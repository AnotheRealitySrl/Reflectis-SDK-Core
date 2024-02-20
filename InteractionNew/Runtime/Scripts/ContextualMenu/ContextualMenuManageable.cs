using Reflectis.SDK.Core;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEditor;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.InteractionNew
{
    [Serializable, RequireComponent(typeof(BaseInteractable))]
    public abstract class ContextualMenuManageable : InteractableBehaviourBase
    {
        public enum EContextualMenuInteractableState
        {
            Idle,
            Showing
        }

        public enum EContextualMenuType
        {
            Default,
            VideoPlayerConttroller,
            PresentationPlayerController
        }

        [Flags]
        public enum EContextualMenuOption
        {
            LockTransform = 1,
            ResetTransform = 2,
            Duplicate = 4,
            Delete = 8,
            ColorPicker = 16,
            Explodable = 32,
            NonProportionalScale = 64,
        }

        [SerializeField]
        private EContextualMenuOption contextualMenuOptions =
            EContextualMenuOption.LockTransform |
            EContextualMenuOption.ResetTransform |
            EContextualMenuOption.Duplicate |
            EContextualMenuOption.Delete;

        public EContextualMenuOption ContextualMenuOptions { get => contextualMenuOptions; set => contextualMenuOptions = value; }

        public EContextualMenuType ContextualMenuType = EContextualMenuType.Default;

        public override bool IsIdleState => CurrentInteractionState == EContextualMenuInteractableState.Idle || CurrentInteractionState == EContextualMenuInteractableState.Showing;

        private EContextualMenuInteractableState currentInteractionState;
        private EContextualMenuInteractableState CurrentInteractionState
        {
            get => currentInteractionState;
            set
            {
                currentInteractionState = value;
                if (currentInteractionState == EContextualMenuInteractableState.Idle)
                {
                    InteractableRef.OnHoverExit();
                }
                if (currentInteractionState == EContextualMenuInteractableState.Showing)
                {
                    InteractableRef.InteractionState = IInteractable.EInteractionState.Hovered;
                }
            }
        }

        public Dictionary<EContextualMenuOption, UnityEvent> OnContextualMenuButtonSelected { get; } = new()
        {
            { EContextualMenuOption.LockTransform, new UnityEvent() },
            { EContextualMenuOption.ResetTransform, new UnityEvent() },
            { EContextualMenuOption.Duplicate, new UnityEvent() },
            { EContextualMenuOption.Delete, new UnityEvent() },
            { EContextualMenuOption.ColorPicker, new UnityEvent() },
            { EContextualMenuOption.Explodable, new UnityEvent() },
            { EContextualMenuOption.NonProportionalScale, new UnityEvent() },
        };

        public UnityEvent OnEnterInteractionState = new();
        public UnityEvent OnExitInteractionState = new();

        public override void Setup()
        {
            if (ContextualMenuOptions.HasFlag(EContextualMenuOption.ColorPicker))
            {
                SM.GetSystem<IColorPickerSystem>().AssignColorPicker(gameObject);
            }

            if (ContextualMenuOptions.HasFlag(EContextualMenuOption.Explodable))
            {
                SM.GetSystem<IModelExploderSystem>().AssignModelExploder(gameObject);
            }
        }

        public override void OnHoverStateEntered()
        {
            //if (!CanInteract)
            if(CurrentBlockedState != 0)
                return;

            SM.GetSystem<ContextualMenuSystem>()?.OnHoverEnterActions.ForEach(x => x.Action(InteractableRef));
        }

        public override void OnHoverStateExited()
        {
            //if (!CanInteract)
            if (CurrentBlockedState != 0)
                return;

            SM.GetSystem<ContextualMenuSystem>()?.OnHoverExitActions.ForEach(x => x.Action(InteractableRef));
        }

        public override async Task EnterInteractionState()
        {
            //if (!CanInteract)
            if (CurrentBlockedState != 0)
                return;
            
            await base.EnterInteractionState();

            OnEnterInteractionState?.Invoke();
            CurrentInteractionState = EContextualMenuInteractableState.Showing;
        }

        public override async Task ExitInteractionState()
        {
            //if (!CanInteract)
            if (CurrentBlockedState != 0)
                return;

            await base.ExitInteractionState();

            OnExitInteractionState?.Invoke();
            CurrentInteractionState = EContextualMenuInteractableState.Idle;
        }

        public void OnContextualMenuButtonClicked(EContextualMenuOption option)
        {
            OnContextualMenuButtonSelected[option].Invoke();
        }


        private async void OnDestroy()
        {
            var contextualMenuSystem = SM.GetSystem<ContextualMenuSystem>();
            if (contextualMenuSystem?.SelectedInteractable == this)
            {
                await contextualMenuSystem.HideContextualMenu();
            }
        }

#if UNITY_EDITOR

        [CustomEditor(typeof(ContextualMenuManageable))]
        public class ContextualMenuManageableEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                ContextualMenuManageable interactable = (ContextualMenuManageable)target;

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
