using Reflectis.SDK.CharacterController;
using Reflectis.SDK.Core;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.InteractionNew
{
    public class Manipulable : InteractableBehaviourBase
    {
        public enum EManipulableState
        {
            Idle,
            BeforeManipulating,
            Manipulating
        }

        [Flags]
        public enum EManipulationMode
        {
            Translate = 1,
            Rotate = 2,
            Scale = 4
        }

        [Flags]
        public enum EVRInteraction
        {
            RayInteraction = 1,
            Hands = 2
        }

        [SerializeField] private EManipulationMode manipulationMode = (EManipulationMode)~0;
        [SerializeField] private EVRInteraction vrInteraction = (EVRInteraction)~0;
        [SerializeField] private bool mouseLookAtCamera;
        [SerializeField] private bool nonProportionalScale;

        public EManipulationMode ManipulationMode { get => manipulationMode; set => manipulationMode = value; }
        public EVRInteraction VRInteraction { get => vrInteraction; set => vrInteraction = value; }
        public bool MouseLookAtCamera { get => mouseLookAtCamera; set => mouseLookAtCamera = value; }
        public bool NonProportionalScale { get => nonProportionalScale; set => nonProportionalScale = value; }

        private List<GameObject> scalingCorners;
        private List<GameObject> scalingFaces;
        private GameObject boundingBox;
        public List<GameObject> ScalingCorners
        {
            get
            {
                if (scalingCorners == null)
                {
                    scalingCorners = GetComponent<BaseInteractable>().GameObjectRef.GetComponentsInChildren<GenericHookComponent>().Where(x => x.Id == "ScalableCorner").Select(x => x.gameObject).ToList();
                }
                return scalingCorners;
            }
        }
        public List<GameObject> ScalingFaces
        {
            get
            {
                if (scalingFaces == null)
                {
                    scalingFaces = GetComponent<BaseInteractable>().GameObjectRef.GetComponentsInChildren<GenericHookComponent>().Where(x => x.Id == "ScalableFace").Select(x => x.gameObject).ToList();
                }
                return scalingFaces;
            }
        }
        public GameObject BoundingBox
        {
            get
            {
                if (boundingBox == null)
                {
                    BaseInteractable baseInteractable = GetComponent<BaseInteractable>();
                    if (baseInteractable != null)
                    {
                        boundingBox = baseInteractable.GameObjectRef.GetComponentsInChildren<GenericHookComponent>().FirstOrDefault(x => x.Id == "BoundingBox")?.gameObject;
                    }
                }
                return boundingBox;
            }
        }
        /// <summary>
        /// The overall size of this manipulable item's mesh elements.
        /// </summary>
        public Vector3 ObjectSize
        {
            get
            {
                if (BoundingBox)
                {
                    return Vector3.Scale(BoundingBox.transform.localScale, transform.localScale);
                }
                // If no bounding box is available, looks for a mesh or skinned
                // mesh renderer on this gameobject
                else if (GetComponent<Renderer>() is Renderer localRenderer)
                {
                    return localRenderer.bounds.size;
                }
                // No visual elements found
                else
                {
                    return Vector3.zero;
                }
            }
        }

        public UnityEvent<EManipulableState> OnCurrentStateChange { get; set; } = new();

        public override bool IsIdleState => CurrentInteractionState == EManipulableState.Idle;

        private EManipulableState currentInteractionState;
        public EManipulableState CurrentInteractionState
        {
            get => currentInteractionState;
            set
            {
                currentInteractionState = value;
                OnCurrentStateChange.Invoke(value);

                if (currentInteractionState == EManipulableState.Idle)
                {
                    InteractableRef.InteractionState = IInteractable.EInteractionState.Hovered;
                }
            }
        }

        public bool CanManipulate { get; set; }

        public override void OnHoverStateEntered()
        {
            SM.GetSystem<IManipulationSystem>()?.OnHoverEnterActions.ForEach(x => x.Action(InteractableRef));
        }

        public override void OnHoverStateExited()
        {
            SM.GetSystem<IManipulationSystem>()?.OnHoverExitActions.ForEach(x => x.Action(InteractableRef));
        }

        public void ToggleBoundingBoxCollider(bool state)
        {
            BoundingBox.GetComponent<Collider>().enabled = state;
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(Manipulable))]
    public class ManipulableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Manipulable interactable = (Manipulable)target;

            GUIStyle style = new(EditorStyles.label)
            {
                richText = true
            };

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

            if (Application.isPlaying)
            {
                EditorGUILayout.LabelField($"<b>Current state:</b> {interactable.CurrentInteractionState}", style);
            }
        }
    }

#endif
}