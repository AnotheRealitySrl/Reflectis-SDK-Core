using System.Threading.Tasks;
using static Reflectis.SDK.Core.Interaction.InteractableBehaviourBase;

namespace Reflectis.SDK.Core.Interaction
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
