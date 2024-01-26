using Reflectis.SDK.Core;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.InteractionNew
{
    [RequireComponent(typeof(BaseInteractable))]
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
        [SerializeField] private bool dynamicAttach;
        [SerializeField] private Transform attachTransform;
        [SerializeField] private bool nonProportionalScale;
        [SerializeField] private bool adjustRotationOnRelease;
        [SerializeField] private bool realignAxisX = true;
        [SerializeField] private bool realignAxisY = false;
        [SerializeField] private bool realignAxisZ = true;
        [SerializeField] private float realignDurationTimeInSeconds = 1f;

        #region Properties

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
        public Transform BoundingBox { get; set; }

        private bool canInteract = true;
        public override bool CanInteract
        {
            get => canInteract;
            set
            {
                canInteract = value;

                BoundingBox.GetComponentInChildren<Renderer>(true).enabled = value && enabled;

                if (manipulationMode.HasFlag(EManipulationMode.Scale))
                    ScalingCorners.ForEach(x => x.SetActive(value && enabled));

                if (nonProportionalScale)
                    ScalingFaces.ForEach(x => x.SetActive(value && enabled));
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
                    return Vector3.Scale(BoundingBox.localScale, transform.localScale);
                }
                // If no bounding box is available, looks for a mesh or skinned
                // mesh renderer on this gameobject
                else if (GetComponentInChildren<Renderer>() is Renderer localRenderer && localRenderer != null)
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

        /// <summary>
        /// The overall center position of this manipulable item's mesh elements.
        /// </summary>
        public Vector3 ObjectCenter
        {
            get
            {
                if (BoundingBox)
                {
                    return BoundingBox.position;
                }
                // If no bounding box is available, looks for a mesh or skinned
                // mesh renderer on this gameobject
                else if (GetComponentInChildren<Renderer>() is Renderer localRenderer && localRenderer != null)
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

        #endregion

        #region Overrides

        public override void Setup() => SM.GetSystem<IManipulationSystem>().SetupManipulable(this);

        public override void OnHoverStateEntered()
        {
            if (!CanInteract)
                return;

            SM.GetSystem<IManipulationSystem>()?.OnHoverEnterActions.ForEach(x => x.Action(InteractableRef));
        }

        public override void OnHoverStateExited()
        {
            if (!CanInteract)
                return;

            SM.GetSystem<IManipulationSystem>()?.OnHoverExitActions.ForEach(x => x.Action(InteractableRef));
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