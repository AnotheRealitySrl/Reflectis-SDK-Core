using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;

using UnityEngine;
using UnityEngine.Events;

using static Reflectis.SDK.InteractionNew.IInteractable;

namespace Reflectis.SDK.InteractionNew
{
    public class BaseInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool doAutomaticSetup = false;
        [SerializeField] private List<InteractableBehaviourBase> interactableBehaviours = new();
        [SerializeField] private List<Collider> interactionColliders = new();

        private bool isHovered;

        private EInteractionState interactionState = EInteractionState.Idle;

        public EInteractionState InteractionState
        {
            get => interactionState;
            set
            {
                if (value == EInteractionState.Idle)
                {
                    value = isHovered ? EInteractionState.Hovered : EInteractionState.Idle;
                }
                var oldValue = interactionState;
                interactionState = value;
                if (value == EInteractionState.Idle && oldValue != EInteractionState.Idle)
                {
                    PropagateHoverExit();
                }
                if (value == EInteractionState.Hovered && oldValue != EInteractionState.Hovered)
                {
                    PropagateHoverEnter();
                }

            }
        }
        public List<IInteractableBehaviour> InteractableBehaviours { get; private set; } = new();

        public GameObject GameObjectRef => gameObject;
        public List<Collider> InteractionColliders { get => interactionColliders; set => interactionColliders = value; }

        public UnityEvent OnInteractableSetupComplete { get; } = new();

        [HideInInspector]
        public bool setupCompleted = false;

        private void Awake()
        {
            if (doAutomaticSetup)
            {
                Setup();
            }
        }

        public async Task Setup()
        {
            if (interactableBehaviours.Count == 0)
            {
                interactableBehaviours.AddRange(GetComponentsInChildren<InteractableBehaviourBase>());
            }

            if (interactionColliders == null || interactionColliders.Count == 0)
            {
                interactionColliders = GetComponentsInChildren<Collider>().ToList();
            }
            foreach (var behaviour in interactableBehaviours)
            {
                if (!InteractableBehaviours.Contains(behaviour))
                {
                    InteractableBehaviours.Add(behaviour);
                }
            }

            foreach (var interactable in InteractableBehaviours)
            {
                await interactable.Setup();
            }
            setupCompleted = true;
        }

        public void OnHoverEnter()
        {
            isHovered = true;
            InteractionState = EInteractionState.Hovered;
        }

        public void OnHoverExit()
        {
            isHovered = false;

            if (InteractableBehaviours.TrueForAll(x => (!x.LockHoverDuringInteraction && x is GenericInteractable) || x.IsIdleState))
            {
                InteractionState = EInteractionState.Idle;
            }
        }

        private void PropagateHoverEnter()
        {
            InteractableBehaviours.ForEach(x => x.OnHoverStateEntered());
        }

        private void PropagateHoverExit()
        {
            InteractableBehaviours
                .ForEach(x =>
                {
                    if (x != null && ((!x.LockHoverDuringInteraction && x is GenericInteractable) || x.IsIdleState))
                    {
                        x.OnHoverStateExited();
                    }
                }
            );
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
