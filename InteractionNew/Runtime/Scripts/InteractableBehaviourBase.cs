using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class InteractableBehaviourBase : MonoBehaviour, IInteractableBehaviour
    {
        [SerializeField] private bool lockHoverDuringInteraction = true;

        public IInteractable InteractableRef => GetComponentInParent<IInteractable>();

        public abstract bool IsIdleState { get; }

        protected bool canInteract = true;
        public virtual bool CanInteract
        {
            get => canInteract && enabled;
            set
            {
                canInteract = value;
                OnInteractionEnabledChange.Invoke(value);
            }
        }

        public UnityEvent<bool> OnInteractionEnabledChange { get; set; } = new();

        public bool LockHoverDuringInteraction => lockHoverDuringInteraction;

        [System.Flags]
        public enum EInteractionState
        {
            Idle = 1,
            Selected = 2, //used in events like pan/unpan and similar --> Never set by ownership
            Blocked = 4, //the interaction are blocked --> Set by general scripts and also by Ownership
        }

        protected EInteractionState currentInteractionBehaviourState;
        public virtual EInteractionState CurrentInteractionBehaviourState
        {
            get => currentInteractionBehaviourState;
            set
            {
                Debug.LogError("Setting interaction behaviour state");
                currentInteractionBehaviourState = value;
                OnCurrentInteractionStateChange.Invoke(value);
            }
        }

        public UnityEvent<EInteractionState> OnCurrentInteractionStateChange { get; set; } = new();

        private void Awake()
        {
            if (!InteractableRef.InteractableBehaviours.Contains(this))
                InteractableRef.InteractableBehaviours.Add(this);
        }

        public abstract void Setup();

        public abstract void OnHoverStateEntered();
        public abstract void OnHoverStateExited();

        public virtual Task EnterInteractionState()
        {
            InteractableRef.InteractionState = IInteractable.EInteractionState.Interaction;
            return Task.CompletedTask;
        }

        public virtual Task ExitInteractionState()
        {
            InteractableRef.InteractionState = IInteractable.EInteractionState.Hovered;
            return Task.CompletedTask;
        }
    }
}
