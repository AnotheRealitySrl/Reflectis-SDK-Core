using System.Threading.Tasks;

namespace Reflectis.SDK.InteractionNew
{
    public interface IInteractableBehaviour
    {
        public IInteractable InteractableRef { get; }
        public bool IsIdleState { get; }

        void OnHoverStateEntered();
        void OnHoverStateExited();

        Task EnterInteractionState();
        Task ExitInteractionState();
    }
}
