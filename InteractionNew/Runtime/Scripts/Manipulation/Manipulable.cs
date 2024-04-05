using Reflectis.SDK.CharacterController;
using Reflectis.SDK.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.InteractionNew
{
    //[RequireComponent(typeof(BaseInteractable))]
    public abstract class Manipulable : InteractableBehaviourBase
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
        [SerializeField] private bool dynamicAttach;
        [SerializeField] private Transform attachTransform;
        [SerializeField] private bool nonProportionalScale;
        [SerializeField] private bool adjustRotationOnRelease;
        [SerializeField] private bool realignAxisX = true;
        [SerializeField] private bool realignAxisY = false;
        [SerializeField] private bool realignAxisZ = true;
        [SerializeField] private float realignDurationTimeInSeconds = 1f;
        [SerializeField] private EBlockedState exampleInteractionForInspector;

        protected EManipulationInput currentManipulationInput;
        private Renderer boundingBoxRenderer;

        #region Properties
        public override EBlockedState CurrentBlockedState
        {
            get => currentBlockedState;
            set
            {
                currentBlockedState = value;
                exampleInteractionForInspector = value;
                OnCurrentBlockedChanged.Invoke(currentBlockedState);

                if (boundingBoxRenderer)
                {
                    boundingBoxRenderer.enabled = value == 0;
                }

                if (manipulationMode.HasFlag(EManipulationMode.Scale))
                    if (value == 0)
                        ScalingCorners.ForEach(x => x.SetActive(true));
                    else
                        ScalingCorners.ForEach(x => x.SetActive(false));

                if (nonProportionalScale)
                    if (value == 0)
                        ScalingFaces.ForEach(x => x.SetActive(true));
                    else
                        ScalingFaces.ForEach(x => x.SetActive(false));
            }
        }

        public EManipulationMode ManipulationMode { get => manipulationMode; set => manipulationMode = value; }
        public EVRInteraction VRInteraction { get => vrInteraction; set => vrInteraction = value; }
        public bool MouseLookAtCamera { get => mouseLookAtCamera; set => mouseLookAtCamera = value; }
        public bool NonProportionalScale { get => nonProportionalScale; set => nonProportionalScale = value; }
        public bool DynamicAttach { get => dynamicAttach; set => dynamicAttach = value; }
        public Transform AttachTransform { get => attachTransform; set => attachTransform = value; }
        public bool AdjustRotationOnRelease { get => adjustRotationOnRelease; set => adjustRotationOnRelease = value; }
        public bool RealignAxisX { get => realignAxisX; set => realignAxisX = value; }
        public bool RealignAxisY { get => realignAxisY; set => realignAxisY = value; }
        public bool RealignAxisZ { get => realignAxisZ; set => realignAxisZ = value; }
        public float RealignDurationTimeInSeconds { get => realignDurationTimeInSeconds; set => realignDurationTimeInSeconds = value; }

        public List<GameObject> ScalingCorners { get; } = new();
        public List<GameObject> ScalingFaces { get; } = new();
        public Collider BoundingBox { get; set; }


        /// <summary>
        /// Returns true if this manipulable is on a submesh element. Returns false 
        /// if this manipulable is at the root of an interactive object.
        /// </summary>
        public bool IsSubmesh
        {
            get
            {
                // Checks if this manipulable is at the root of the interactable object.
                // If not, this is a submesh
                GameObject rootManipulableObj = ((BaseInteractable)this.InteractableRef).gameObject;
                return rootManipulableObj != this.gameObject;
            }
        }

        /// <summary>
        /// Returns the Manipulable component at the root of the interactive object where this 
        /// component is placed.
        /// This can be used to get a reference to the root Manipulable even from a 
        /// Manipulable object placed on a model's submesh.
        /// </summary>
        public Manipulable RootManipulable
        {
            get
            {
                GameObject rootManipulableObj = ((BaseInteractable)this.InteractableRef).gameObject;
                return rootManipulableObj.GetComponent<Manipulable>();
            }
        }

        /// <summary>
        /// The overall size of this manipulable item's mesh elements.
        /// </summary>
        public Vector3 ObjectSize
        {
            get
            {
                if (!IsSubmesh)
                {
                    // This manipulable is at the root of the interactive object
                    return Vector3.Scale(BoundingBox.transform.localScale, transform.localScale);
                }
                else
                {
                    // This manipulable is on a submesh of the interactive object.
                    // It will now look for a mesh or skinned mesh renderer on this gameobject
                    if (GetComponentInChildren<Renderer>() is Renderer localRenderer && localRenderer != null)
                    {
                        return localRenderer.bounds.size;
                    }
                    // Tries to look for a collider
                    else if (GetComponentInChildren<Collider>() is Collider localCollider && localCollider != null)
                    {
                        return localCollider.bounds.size;
                    }
                    // No visual elements found
                    else
                    {
                        Debug.LogError("Size not available: no visual element has been found");
                        return Vector3.zero;
                    }
                }
            }
        }

        /// <summary>
        /// The overall center position of this manipulable item's mesh elements.
        /// </summary>
        public Vector3 ObjectCenter
        {
            get
            {
                if (!IsSubmesh)
                {
                    // This manipulable is at the root of the interactive object
                    return BoundingBox.transform.position;
                }
                else
                {
                    // This manipulable is on a submesh of the interactive object.
                    // It will now look for a mesh or skinned mesh renderer on this gameobject
                    if (GetComponentInChildren<Renderer>() is Renderer localRenderer && localRenderer != null)
                    {
                        return localRenderer.bounds.center;
                    }
                    // Tries to look for a collider
                    else if (GetComponentInChildren<Collider>() is Collider localCollider && localCollider != null)
                    {
                        return localCollider.bounds.center;
                    }
                    // No visual elements found
                    else
                    {
                        Debug.LogError("Center point not available: no visual element has been found");
                        return Vector3.zero;
                    }
                }
            }
        }

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

        public UnityEvent<EManipulableState> OnCurrentStateChange { get; set; } = new();
        public EManipulationInput CurrentManipulationInput { get => currentManipulationInput; set => currentManipulationInput = value; }

        #endregion

        #region Abstract methods

        public abstract void SetScalingPoints();
        public abstract void UpdateScalingPointsPosition();

        #endregion

        #region Overrides

        public override async Task Setup()
        {
            if (GetComponentsInChildren<GenericHookComponent>(true).FirstOrDefault(x => x.Id == "BoundingBoxVisual") is GenericHookComponent boundingBoxVisualHookComponent)
                boundingBoxRenderer = boundingBoxVisualHookComponent.GetComponent<Renderer>();

            BoundingBox = InteractableRef.InteractionColliders.FirstOrDefault(x => x.GetComponents<GenericHookComponent>().FirstOrDefault(x => x.Id == "BoundingBox"));
            if (BoundingBox == null)
            {
                GameObject boundingBoxGO = new GameObject("BoundingBox");
                BoxCollider collider = boundingBoxGO.AddComponent<BoxCollider>();
                collider.enabled = false;
                boundingBoxGO.transform.parent = transform;
                var renderers = GetComponentsInChildren<Renderer>();
                var bounds = renderers.First().bounds;
                foreach (var renderer in renderers.Skip(1))
                {
                    bounds.Encapsulate(renderer.bounds);
                }


                Debug.Log($"bounds: {bounds.center}, {bounds.size}");
                // offset so that the bounding box is centered in zero and apply scale
                boundingBoxGO.transform.localPosition = Vector3.zero;
                boundingBoxGO.transform.localScale = bounds.size;
                BoundingBox = collider;
                //BoundingBox = InteractableRef.InteractionColliders[0];

            }

        }

        public override void OnHoverStateEntered()
        {
            if (CurrentBlockedState != 0)
                return;

            SM.GetSystem<IManipulationSystem>()?.OnManipulableHoverEnter(InteractableRef);
        }

        public override void OnHoverStateExited()
        {
            if (CurrentBlockedState != 0)
                return;

            SM.GetSystem<IManipulationSystem>()?.OnManipulableHoverExit(InteractableRef);
        }

        #endregion

        #region Public methods

        public void ToggleBoundingBoxCollider(bool state)
        {
            BoundingBox.GetComponent<Collider>().enabled = state;
        }

        #endregion
    }

}