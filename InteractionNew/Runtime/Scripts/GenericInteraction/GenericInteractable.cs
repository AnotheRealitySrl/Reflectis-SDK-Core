using Reflectis.SDK.Core;
using Reflectis.SDK.Platform;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEditor;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public class GenericInteractable : InteractableBehaviourBase, IInteractableBehaviour
    {
        [Flags]
        public enum EGenericInteractableState
        {
            Idle = 0,
            SelectEntering = 1,
            Selected = 2,
            Interacting = 3,
            SelectExiting = 4,
        }

        [Flags]
        public enum EAllowedGenericInteractableState
        {
            Selected = 1,
            Interacting = 2
        }

        [SerializeField] private List<AwaitableScriptableAction> onHoverEnterActions = new();
        [SerializeField] private List<AwaitableScriptableAction> onHoverExitActions = new();
        [SerializeField] private List<AwaitableScriptableAction> onSelectingActions = new();
        [SerializeField] private List<AwaitableScriptableAction> onSelectedActions = new();
        [SerializeField] private List<AwaitableScriptableAction> onDeselectingActions = new();
        [SerializeField] private List<AwaitableScriptableAction> onDeselectedActions = new();
        [SerializeField] private List<AwaitableScriptableAction> onInteractActions = new();
        [SerializeField] private List<AwaitableScriptableAction> onInteractFinishActions = new();


        [Header("Allowed states")]
        [SerializeField] private EAllowedGenericInteractableState desktopAllowedStates = EAllowedGenericInteractableState.Selected | EAllowedGenericInteractableState.Interacting;
        [SerializeField] private EAllowedGenericInteractableState vrAllowedStates = EAllowedGenericInteractableState.Selected | EAllowedGenericInteractableState.Interacting;


        public List<AwaitableScriptableAction> OnHoverEnterActions { get => onHoverEnterActions; set => onHoverEnterActions = value; }
        public List<AwaitableScriptableAction> OnHoverExitActions { get => onHoverExitActions; set => onHoverExitActions = value; }
        public List<AwaitableScriptableAction> OnSelectingActions { get => onSelectingActions; set => onSelectingActions = value; }
        public List<AwaitableScriptableAction> OnSelectedActions { get => onSelectedActions; set => onSelectedActions = value; }
        public List<AwaitableScriptableAction> OnDeselectingActions { get => onDeselectingActions; set => onDeselectingActions = value; }
        public List<AwaitableScriptableAction> OnDeselectedActions { get => onDeselectedActions; set => onDeselectedActions = value; }
        public List<AwaitableScriptableAction> OnInteractActions { get => onInteractActions; set => onInteractActions = value; }
        public List<AwaitableScriptableAction> OnInteractFinishActions { get => onInteractFinishActions; set => onInteractFinishActions = value; }

        public EAllowedGenericInteractableState DesktopAllowedStates { get => desktopAllowedStates; set => desktopAllowedStates = value; }
        public EAllowedGenericInteractableState VRAllowedStates { get => vrAllowedStates; set => vrAllowedStates = value; }


        public override bool IsIdleState => CurrentInteractionState == EGenericInteractableState.Idle;

        private EGenericInteractableState currentInteractionState;
        private EGenericInteractableState CurrentInteractionState
        {
            get => currentInteractionState;
            set
            {
                currentInteractionState = value;
                if (currentInteractionState == EGenericInteractableState.Idle)
                {
                    InteractableRef.InteractionState = IInteractable.EInteractionState.Hovered;
                }
            }
        }

        private bool skipSelectState = false;
        private bool hasInteractState = false;


        private void Awake()
        {
            switch (SM.GetSystem<IPlatformSystem>().RuntimePlatform)
            {
                case RuntimePlatform.WebGLPlayer:
                    skipSelectState = !DesktopAllowedStates.HasFlag(EAllowedGenericInteractableState.Selected);
                    hasInteractState = DesktopAllowedStates.HasFlag(EAllowedGenericInteractableState.Interacting);
                    break;
                case RuntimePlatform.Android:
                    skipSelectState = !VRAllowedStates.HasFlag(EAllowedGenericInteractableState.Selected);
                    hasInteractState = VRAllowedStates.HasFlag(EAllowedGenericInteractableState.Interacting);
                    break;
            }
        }

        private void OnDestroy()
        {
            if (!IsIdleState)
            {
                onDeselectingActions.ForEach(x => x.Action());
                onDeselectedActions.ForEach(x => x.Action());
            }
        }

        public override void OnHoverStateEntered()
        {
            onHoverEnterActions.ForEach(a => a.Action(InteractableRef));
        }

        public override void OnHoverStateExited()
        {
            onHoverExitActions.ForEach(a => a.Action(InteractableRef));
        }

        public override async Task EnterInteractionState()
        {
            CurrentInteractionState = EGenericInteractableState.SelectEntering;
            foreach (var action in OnSelectingActions)
            {
                await action.Action(InteractableRef);
            }

            CurrentInteractionState = EGenericInteractableState.Selected;
            foreach (var action in onSelectedActions)
            {
                await action.Action(InteractableRef);
            }

            if (skipSelectState)
            {
                await Interact();
            }
        }

        public override async Task ExitInteractionState()
        {
            CurrentInteractionState = EGenericInteractableState.SelectExiting;
            foreach (var action in onDeselectingActions)
            {
                await action.Action(InteractableRef);
            }

            CurrentInteractionState = EGenericInteractableState.Idle;
            foreach (var action in OnDeselectedActions)
            {
                await action.Action(InteractableRef);
            }
        }

        public async Task Interact()
        {
            if (CurrentInteractionState != EGenericInteractableState.Selected && hasInteractState)
                return;

            CurrentInteractionState = EGenericInteractableState.Interacting;
            foreach (var action in onInteractActions)
            {
                await action.Action(InteractableRef);
            }

            CurrentInteractionState = EGenericInteractableState.Selected;
            foreach (var action in onInteractFinishActions)
            {
                await action.Action(InteractableRef);
            }

            if (skipSelectState)
            {
                await ExitInteractionState();
            }
        }


#if UNITY_EDITOR

        [CustomEditor(typeof(GenericInteractable))]
        public class GenericInteractableEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                GenericInteractable interactable = (GenericInteractable)target;

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
}
