using System.Threading.Tasks;
using static Reflectis.SDK.InteractionNew.InteractableBehaviourBase;

namespace Reflectis.SDK.InteractionNew
{
    public interface IInteractableBehaviour
    {
        IInteractable InteractableRef { get; }
        bool IsIdleState { get; }
        bool LockHoverDuringInteraction { get; }
        Task Setup();
        public EBlockedState CurrentBlockedState { get; set; }

        void OnHoverStateEntered();
        void OnHoverStateExited();

        Task EnterInteractionState();
        Task ExitInteractionState();
    }
}
