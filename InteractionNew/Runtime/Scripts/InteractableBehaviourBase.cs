using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class InteractableBehaviourBase : MonoBehaviour, IInteractableBehaviour
    {
        [SerializeField] private bool lockHoverDuringInteraction = true;

        public IInteractable InteractableRef => GetComponentInParent<IInteractable>();

        public abstract bool IsIdleState { get; }

        public virtual bool CanInteract { get; set; } = true;

        public bool LockHoverDuringInteraction => lockHoverDuringInteraction;

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
