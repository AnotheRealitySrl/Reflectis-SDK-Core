using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.Core.Interaction
{
    public interface IGenericInteractionSystem : ISystem
    {
        InteractableBehaviourBase SetupInteractableBehaviour(GameObject obj);
        Task UnselectCurrentInteractable(GenericInteractable interactableToDisable);
        public UnityEvent<GenericInteractable> OnSelectedInteractableChange { get; set; }
    }
}
