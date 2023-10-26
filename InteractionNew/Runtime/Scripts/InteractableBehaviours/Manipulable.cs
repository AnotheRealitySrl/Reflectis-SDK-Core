using Reflectis.SDK.Core;

using System;

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
            Translate,
            Rotate,
            Scale
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

        public async override void OnHoverStateEntered()
        {
            foreach (var action in SM.GetSystem<IManipulationSystem>()?.OnHoverEnterActions)
            {
                await action.Action(InteractableRef);
            }
        }

        public async override void OnHoverStateExited()
        {
            foreach (var action in SM.GetSystem<IManipulationSystem>()?.OnHoverExitActions)
            {
                await action.Action(InteractableRef);
            }
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
