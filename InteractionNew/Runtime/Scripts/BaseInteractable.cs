using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;
using UnityEngine.Events;

using static Reflectis.SDK.InteractionNew.IInteractable;

namespace Reflectis.SDK.InteractionNew
{
    public class BaseInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<Collider> interactionColliders = new();

        public EInteractionState InteractionState { get; set; } = EInteractionState.Idle;
        public List<IInteractableBehaviour> InteractableBehaviours { get; set; } = new();

        public GameObject GameObjectRef => gameObject;
        public List<Collider> InteractionColliders { get => interactionColliders; set => interactionColliders = value; }

        public UnityEvent OnInteractableSetupComplete { get; } = new();

        private void Awake()
        {
            InteractableBehaviours = GetComponentsInChildren<IInteractableBehaviour>().ToList();

            if (interactionColliders.Count == 0)
            {
                interactionColliders.AddRange(GetComponentsInChildren<Collider>());
            }
        }

        public void OnHoverEnter()
        {
            InteractionState = EInteractionState.Hovered;
            InteractableBehaviours.ForEach(x => x.OnHoverStateEntered());
        }

        public void OnHoverExit()
        {
            InteractionState = EInteractionState.Idle;
            InteractableBehaviours.ForEach(x => x.OnHoverStateExited());
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
