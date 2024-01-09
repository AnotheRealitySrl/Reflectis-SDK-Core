using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class InteractableBehaviourBase : MonoBehaviour, IInteractableBehaviour
    {
        public IInteractable InteractableRef => GetComponentInParent<IInteractable>();

        public abstract bool IsIdleState { get; }

        public bool CanInteract { get; set; } = true;

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
