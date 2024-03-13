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

        private bool canInteract = true;
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

        protected virtual void Awake()
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
