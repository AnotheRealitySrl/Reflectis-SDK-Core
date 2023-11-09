using Reflectis.SDK.Core;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEditor;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.InteractionNew
{
    [Serializable]
    public class ContextualMenuManageable : InteractableBehaviourBase
    {
        public enum EContextualMenuInteractableState
        {
            Idle,
            Showing
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
        }

        [SerializeField]
        private EContextualMenuOption contextualMenuOptions =
            EContextualMenuOption.LockTransform |
            EContextualMenuOption.ResetTransform |
            EContextualMenuOption.Duplicate |
            EContextualMenuOption.Delete;

        public EContextualMenuOption ContextualMenuOptions { get => contextualMenuOptions; set => contextualMenuOptions = value; }

        public override bool IsIdleState => CurrentInteractionState == EContextualMenuInteractableState.Idle;

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
            }
        }

        public Dictionary<EContextualMenuOption, UnityEvent> OnContextualMenuButtonSelected { get; } = new()
        {
            { EContextualMenuOption.LockTransform, new UnityEvent() },
            { EContextualMenuOption.ResetTransform, new UnityEvent() },
            { EContextualMenuOption.Duplicate, new UnityEvent() },
            { EContextualMenuOption.Delete, new UnityEvent() },
            { EContextualMenuOption.ColorPicker, new UnityEvent() },
            { EContextualMenuOption.Explodable, new UnityEvent() }
        };

        public override void OnHoverStateEntered()
        {
            SM.GetSystem<ContextualMenuSystem>()?.OnHoverEnterActions.ForEach(x => x.Action(InteractableRef));
        }

        public override void OnHoverStateExited()
        {
            SM.GetSystem<ContextualMenuSystem>()?.OnHoverExitActions.ForEach(x => x.Action(InteractableRef));
        }

        public override async Task EnterInteractionState()
        {
            await base.EnterInteractionState();
            CurrentInteractionState = EContextualMenuInteractableState.Showing;
        }

        public override async Task ExitInteractionState()
        {
            await base.ExitInteractionState();
            CurrentInteractionState = EContextualMenuInteractableState.Idle;
        }

        public void OnContextualMenuButtonClicked(EContextualMenuOption option)
        {
            OnContextualMenuButtonSelected[option].Invoke();
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
