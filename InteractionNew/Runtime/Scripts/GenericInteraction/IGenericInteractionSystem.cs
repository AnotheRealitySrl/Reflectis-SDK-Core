using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.InteractionNew
{
    public interface IGenericInteractionSystem : ISystem
    {
        InteractableBehaviourBase SetupInteractableBehaviour(GameObject obj);
        Task UnselectCurrentInteractable(GenericInteractable interactableToDisable);
        public UnityEvent<GenericInteractable> OnSelectedInteractableChange { get; set; }
    }
}
