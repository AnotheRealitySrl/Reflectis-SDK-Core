using System.Threading.Tasks;

namespace Reflectis.SDK.InteractionNew
{
    public interface IInteractableBehaviour
    {
        IInteractable InteractableRef { get; }
        bool IsIdleState { get; }
        bool CanInteract { get; set; }
        bool LockHoverDuringInteraction { get; }

        void Setup();

        void OnHoverStateEntered();
        void OnHoverStateExited();

        Task EnterInteractionState();
        Task ExitInteractionState();
    }
}
