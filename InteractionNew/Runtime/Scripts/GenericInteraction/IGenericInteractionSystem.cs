using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public interface IGenericInteractionSystem : ISystem
    {
        GenericInteractable SetupInteractableBehaviour(GameObject obj);
        Task UnselectCurrentInteractable(GenericInteractable interactableToDisable);
    }
}
