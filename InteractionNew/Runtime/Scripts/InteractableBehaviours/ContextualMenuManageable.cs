using System.Threading.Tasks;

using UnityEditor;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public class ContextualMenuManageable : InteractableBehaviourBase
    {
        public enum EContextualMenuState
        {
            Idle,
            Showing
        }

        [SerializeField] private bool lockTransform = true;
        [SerializeField] private bool resetTransform = true;
        [SerializeField] private bool duplicate = true;
        [SerializeField] private bool delete = true;
        [SerializeField] private bool colorPicker;
        [SerializeField] private bool explodable;

        public bool LockTransform { get => lockTransform; set => lockTransform = value; }
        public bool ResetTransform { get => resetTransform; set => resetTransform = value; }
        public bool Duplicate { get => duplicate; set => duplicate = value; }
        public bool Delete { get => delete; set => delete = value; }
        public bool ColorPicker { get => colorPicker; set => colorPicker = value; }
        public bool Explodable { get => explodable; set => explodable = value; }

        public override bool IsIdleState => CurrentInteractionState == EContextualMenuState.Idle;

        private EContextualMenuState currentInteractionState;
        private EContextualMenuState CurrentInteractionState
        {
            get => currentInteractionState;
            set
            {
                currentInteractionState = value;
                if (currentInteractionState == EContextualMenuState.Idle)
                {
                    InteractableRef.InteractionState = IInteractable.EInteractionState.Hovered;
                }
            }
        }

        public override void OnHoverStateEntered()
        {
            //
        }

        public override void OnHoverStateExited()
        {
            //
        }

        public override async Task EnterInteractionState()
        {
            await base.EnterInteractionState();
            currentInteractionState = EContextualMenuState.Showing;
        }

        public override async Task ExitInteractionState()
        {
            await base.ExitInteractionState();
            currentInteractionState = EContextualMenuState.Idle;
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
