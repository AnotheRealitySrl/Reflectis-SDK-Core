using System.Threading.Tasks;

namespace Reflectis.SDK.InteractionNew
{
    public interface IInteractableBehaviour
    {
        IInteractable InteractableRef { get; }
        bool IsIdleState { get; }
        bool LockHoverDuringInteraction { get; }

        Task Setup();

        void OnHoverStateEntered();
        void OnHoverStateExited();

        Task EnterInteractionState();
        Task ExitInteractionState();
    }
}
