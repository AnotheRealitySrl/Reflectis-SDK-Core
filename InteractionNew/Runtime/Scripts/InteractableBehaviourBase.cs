using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.InteractionNew
{

    [RequireComponent(typeof(InteractableCollider))]
    public abstract class InteractableBehaviourBase : MonoBehaviour, IInteractableBehaviour
    {
        protected InteractableCollider interactableCollider;

        [SerializeField]
        private bool autoSetupAtStart;

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
        public InteractableCollider InteractableCollider { get => interactableCollider; set => interactableCollider = value; }

        public virtual Task Setup()
        {
            InteractableCollider = GetComponent<InteractableCollider>();

            switch (InteractableCollider.InteractionMode)
            {
                case EInteractionMode.BoundingBox:

                    if (InteractableCollider.BoundingBox == null)
                    {
                        InteractableCollider.BoundingBox = BoundingBox.GetOrGenerateBoundingBox(gameObject);
                    }

                    InteractableCollider.Colliders = new List<Collider>() { InteractableCollider.BoundingBox.Collider };

                    break;
                case EInteractionMode.MultipleColliders:
                    if (InteractableCollider.Colliders == null || InteractableCollider.Colliders.Count == 0)
                    {
                        InteractableCollider.Colliders = GetComponentsInChildren<Collider>().ToList();
                    }
                    break;
            }
            return Task.CompletedTask;
        }

        public abstract void HoverEnter();
        public abstract void HoverExit();

        public virtual void Start()
        {
            if (autoSetupAtStart)
            {
                Setup();
            }
        }

    }
}
