using Reflectis.SDK.CharacterController;
using Reflectis.SDK.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;

using UnityEngine;
using UnityEngine.Events;

using static Reflectis.SDK.InteractionNew.IInteractable;

namespace Reflectis.SDK.InteractionNew
{
    public class BaseInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool doAutomaticSetup = false;
        [SerializeField] private List<InteractableBehaviourBase> interactableBehaviours = new();
        [SerializeField] private List<Collider> interactionColliders = new();

        private bool isHovered;

        private EInteractionState interactionState = EInteractionState.Idle;

        public EInteractionState InteractionState
        {
            get => interactionState;
            set
            {
                if (value == EInteractionState.Idle)
                {
                    value = isHovered ? EInteractionState.Hovered : EInteractionState.Idle;
                }
                var oldValue = interactionState;
                interactionState = value;
                if (value == EInteractionState.Idle && oldValue != EInteractionState.Idle)
                {
                    PropagateHoverExit();
                }
                if (value == EInteractionState.Hovered && oldValue != EInteractionState.Hovered)
                {
                    PropagateHoverEnter();
                }

            }
        }
        public List<IInteractableBehaviour> InteractableBehaviours { get; private set; } = new();

        public GameObject GameObjectRef => gameObject;
        public List<Collider> InteractionColliders { get => interactionColliders; set => interactionColliders = value; }

        public UnityEvent OnInteractableSetupComplete { get; } = new();

        [HideInInspector]
        public bool setupCompleted = false;

        private void Awake()
        {
            if (doAutomaticSetup)
            {
                Setup();
            }
        }

        public async Task Setup()
        {
            if (interactableBehaviours.Count == 0)
            {
                interactableBehaviours.AddRange(GetComponentsInChildren<InteractableBehaviourBase>());
            }

            if (interactionColliders == null || interactionColliders.Count == 0)
            {
                interactionColliders = GetComponentsInChildren<Collider>().ToList();
            }
            foreach (var behaviour in interactableBehaviours)
            {
                if (!InteractableBehaviours.Contains(behaviour))
                {
                    InteractableBehaviours.Add(behaviour);
                }
            }

            foreach (var interactable in InteractableBehaviours)
            {
                await interactable.Setup();
            }
            setupCompleted = true;
        }

        public void OnHoverEnter()
        {
            isHovered = true;
            InteractionState = EInteractionState.Hovered;
        }

        public void OnHoverExit()
        {
            isHovered = false;

            if (InteractableBehaviours.TrueForAll(x => (!x.LockHoverDuringInteraction && x is GenericInteractable) || x.IsIdleState))
            {
                InteractionState = EInteractionState.Idle;
            }
        }

        private void PropagateHoverEnter()
        {
            InteractableBehaviours.ForEach(x => x.OnHoverStateEntered());
        }

        private void PropagateHoverExit()
        {
            InteractableBehaviours
                .ForEach(x =>
                {
                    if (x != null && ((!x.LockHoverDuringInteraction && x is GenericInteractable) || x.IsIdleState))
                    {
                        x.OnHoverStateExited();
                    }
                }
            );
        }

        public async Task<List<InteractableSubmesh>> SetupSubmeshes(int polygonThreshold)
        {
            List<InteractableSubmesh> submeshes = new List<InteractableSubmesh>();

            List<MeshRenderer> rendererList = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>(true));

            List<GameObject> objectsToExclude = new List<GameObject>();
            // Excludes bounding box and scaling gizmos
            Manipulable modelManipulable = this.GetComponentInactive<Manipulable>();
            if (modelManipulable != null)
            {
                objectsToExclude.AddRange(modelManipulable.BoundingBox.gameObject.transform.GetComponentsInChildren<Transform>(true).Select(x => x.gameObject));

                foreach (GameObject gObject in modelManipulable.ScalingCorners)
                {
                    objectsToExclude.Add(gObject);
                }
                foreach (GameObject gObject in modelManipulable.ScalingFaces)
                {
                    objectsToExclude.Add(gObject);
                }
            }

            // Excludes placeholder object (which is used during asset download)
            GameObject placeholder = GetComponentsInChildren<GenericHookComponent>().FirstOrDefault(x => x.Id == "Placeholder")?.gameObject;
            if (placeholder != null)
            {
                objectsToExclude.Add(placeholder);
            }

            // Cycle to find objects that should be excluded
            foreach (var objectToExclude in objectsToExclude)
            {
                rendererList = rendererList.Where(x => x.gameObject != objectToExclude).ToList();
            }
            foreach (MeshRenderer renderer in rendererList)
            {
                //check if there is already an interactable submesh
                if (!renderer.TryGetComponent<InteractableSubmesh>(out var interactableSubmesh))
                {
                    if (!renderer.TryGetComponent(out Collider existingCollider))
                    {
                        var colliders = await SplitMeshAndCreateColliders(renderer, polygonThreshold);
                        interactableSubmesh.Colliders = colliders;
                        interactableSubmesh.Renderer = renderer;
                        submeshes.Add(interactableSubmesh);
                        interactableSubmesh.Colliders = colliders;
                    }
                    else
                    {
                        interactableSubmesh.Colliders = new List<Collider>() { existingCollider };
                    }
                    interactableSubmesh.Renderer = renderer;
                }
                submeshes.Add(interactableSubmesh);
            }
            return submeshes;
        }


        // Function that splits the mesh and generates the colliders for each submesh
        private async Task<List<Collider>> SplitMeshAndCreateColliders(MeshRenderer meshRenderer, int maxTrianglesPerSubmesh)
        {
            Mesh originalMesh = meshRenderer.GetComponent<MeshFilter>().sharedMesh;
            int[] triangles = originalMesh.triangles;
            Vector3[] vertices = originalMesh.vertices;
            Vector3[] normals = originalMesh.normals;
            Vector2[] uvs = originalMesh.uv;

            int triangleCount = triangles.Length / 3; // Total number of triangles in the mesh

            // List to hold all submeshes
            var subMeshes = new List<Mesh>();

            // Temporary variables to construct the submeshes
            List<int> submeshTriangles = new List<int>();
            List<Vector3> submeshVertices = new List<Vector3>();
            List<Vector3> submeshNormals = new List<Vector3>();
            List<Vector2> submeshUVs = new List<Vector2>();

            int currentTriangleIndex = 0;

            List<Collider> colliders = new List<Collider>();

            while (currentTriangleIndex < triangleCount)
            {
                submeshTriangles.Clear();
                submeshVertices.Clear();
                submeshNormals.Clear();
                submeshUVs.Clear();

                // Construct the submesh until the triangle threshold is reached
                int trianglesAdded = 0;
                while (trianglesAdded < maxTrianglesPerSubmesh && currentTriangleIndex < triangleCount)
                {
                    for (int j = 0; j < 3; j++) // Adds the 3 indices of the current triangle
                    {
                        int triangleVertexIndex = triangles[currentTriangleIndex * 3 + j];
                        submeshTriangles.Add(submeshVertices.Count); // New index for the submesh
                        submeshVertices.Add(vertices[triangleVertexIndex]);
                        submeshNormals.Add(normals[triangleVertexIndex]);
                        submeshUVs.Add(uvs[triangleVertexIndex]);
                    }

                    trianglesAdded++;
                    currentTriangleIndex++;
                }

                // Create the mesh for the submesh
                Mesh submesh = new Mesh();
                submesh.SetVertices(submeshVertices);
                submesh.SetTriangles(submeshTriangles, 0);
                submesh.SetNormals(submeshNormals);
                submesh.SetUVs(0, submeshUVs);

                // Add the submesh to the list
                subMeshes.Add(submesh);

                // Create the collider for the submesh (back to the main thread to interact with Unity)
                MeshCollider submeshCollider = meshRenderer.gameObject.AddComponent<MeshCollider>();
                submeshCollider.convex = false;  // If required, it can be set to true to simplify collisions
                submeshCollider.sharedMesh = submesh;
                colliders.Add(submeshCollider);
                await Task.Yield();

            }

            Debug.Log($"Mesh divided into {subMeshes.Count} submeshes.");

            return colliders;
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(BaseInteractable))]
    public class BaseInteractableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            BaseInteractable interactable = (BaseInteractable)target;

            GUIStyle style = new(EditorStyles.label)
            {
                richText = true
            };

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

            if (Application.isPlaying)
            {
                EditorGUILayout.LabelField($"<b>Current state:</b> {interactable.InteractionState}", style);
            }
        }
    }

#endif

}
