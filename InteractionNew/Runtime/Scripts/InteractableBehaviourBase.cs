using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class InteractableBehaviourBase : MonoBehaviour, IInteractableBehaviour
    {
        [SerializeField] private bool lockHoverDuringInteraction = true;

        public IInteractable InteractableRef => GetComponentInParent<IInteractable>(true);

        public abstract bool IsIdleState { get; }

        public bool LockHoverDuringInteraction { get => lockHoverDuringInteraction; set => lockHoverDuringInteraction = value; }

        //bitmask used to know if an interactable is blocked for various reasons
        [System.Flags]
        public enum EBlockedState
        {
            BlockedByOthers = 1, //blocked by player manipolation (like when manipulating with ownership)
            BlockedBySelection = 2, //used in events like pan/unpan and similar --> Never set by ownership
            BlockedByGenerals = 4, //the interactions are blocked --> Set by general scripts. When in this state interaction are stopped and the interactable script is usually set to false
        }

        //set the currentBlockedState to none
        protected EBlockedState currentBlockedState = 0; //set interaction to nothing at the beginning
        public virtual EBlockedState CurrentBlockedState
        {
            get => currentBlockedState;
            set
            {
                currentBlockedState = value;
                OnCurrentBlockedChanged.Invoke(value);
            }
        }

        public UnityEvent<EBlockedState> OnCurrentBlockedChanged { get; set; } = new();

        public abstract Task Setup();

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
