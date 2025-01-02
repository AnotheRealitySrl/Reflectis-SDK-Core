using Reflectis.SDK.Core;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEditor;

using UnityEngine;
using UnityEngine.Events;

using static IPopupSystem;

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
            None = 0,
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
            EContextualMenuOption.None;

        [SerializeField]
        private bool isNetworked = true;

        public EContextualMenuOption ContextualMenuOptions
        {
            get
            {
                return contextualMenuOptions;
            }
            set => contextualMenuOptions = value;
        }

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

        public Dictionary<EContextualMenuOption, UnityAction> OnContextualMenuButtonSelected { get; } = new()
        {
            { EContextualMenuOption.LockTransform, null },
            { EContextualMenuOption.ResetTransform, null },
            { EContextualMenuOption.Duplicate, null },
            { EContextualMenuOption.Delete, null },
            { EContextualMenuOption.ColorPicker, null },
            { EContextualMenuOption.Explodable, null },
            { EContextualMenuOption.NonProportionalScale, null },
        };

        public UnityAction DoDestroy { get; set; }
        public bool IsNetworked { get => isNetworked; set => isNetworked = value; }


        public UnityEvent OnEnterInteractionState = new();
        public UnityEvent OnExitInteractionState = new();


        private void Awake()
        {
            DoDestroy ??= LocalDestroy;
        }


        private async void OnDestroy()
        {
            var contextualMenuSystem = SM.GetSystem<ContextualMenuSystem>();
            if (contextualMenuSystem?.SelectedInteractable == this)
            {
                await contextualMenuSystem.HideContextualMenu();
            }
        }

        public override async Task Setup()
        {
            OnContextualMenuButtonSelected[EContextualMenuOption.Delete] = AskForDelete;

            if (ContextualMenuOptions.HasFlag(EContextualMenuOption.ColorPicker))
            {
                if (TryGetComponent(out IColorPicker colorPicker))
                {
                    await colorPicker.Setup();
                }
                else
                {
                    await SM.GetSystem<IColorPickerSystem>().AssignColorPicker(gameObject, IsNetworked);
                }
            }

            if (ContextualMenuOptions.HasFlag(EContextualMenuOption.Explodable))
            {

                if (TryGetComponent(out IModelExploder modelExploder))
                {
                    await modelExploder.Setup();
                }
                else
                {
                    await SM.GetSystem<IModelExploderSystem>().AssignModelExploder(gameObject, IsNetworked);
                }
            }

            if (ContextualMenuOptions.HasFlag(EContextualMenuOption.LockTransform))
            {
                if (TryGetComponent(out ILockObject objectLocker))
                {
                    objectLocker.Setup();
                    if (TryGetComponent(out ILockObjectNetwork networkLock))
                    {
                        await networkLock.Setup();
                    }
                }
                else
                {
                    await SM.GetSystem<ILockObjectSystem>().SetupLockObject(gameObject, IsNetworked);
                }
            }
        }


        public override void OnHoverStateEntered()
        {
            if (CurrentBlockedState != 0)
                return;
        }

        public override void OnHoverStateExited()
        {
            if (CurrentBlockedState != 0)
                return;
        }

        public override async Task EnterInteractionState()
        {
            if (CurrentBlockedState != 0)
                return;

            await base.EnterInteractionState();

            OnEnterInteractionState?.Invoke();
            CurrentInteractionState = EContextualMenuInteractableState.Showing;
        }

        public override async Task ExitInteractionState()
        {
            if (CurrentBlockedState != 0)
                return;

            await base.ExitInteractionState();

            OnExitInteractionState?.Invoke();
            CurrentInteractionState = EContextualMenuInteractableState.Idle;
        }

        public void OnContextualMenuButtonClicked(EContextualMenuOption option)
        {
            OnContextualMenuButtonSelected[option]?.Invoke();
        }

        public void AskForDelete()
        {
            SM.GetSystem<IPopupSystem>().Instantiate(
                // Used for asset deletion
                13,
                popupParent: transform,
                whereToDisplay: PopupLocation.Floating,
                button1Callback: DoDestroy,
                button2Callback: () => { },
                popUpGravity: EPopUpGravity.Replaceable);
        }

        public void LocalDestroy()
        {
            Destroy(gameObject);
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
