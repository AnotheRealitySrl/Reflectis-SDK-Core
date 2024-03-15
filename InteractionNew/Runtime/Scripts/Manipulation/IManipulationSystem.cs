using Reflectis.SDK.Core;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public interface IManipulationSystem : ISystem
    {
        public InteractableBehaviourBase SetupInteractableBehaviour(GameObject obj);

        public void OnManipulableHoverEnter(IInteractable interactable);

        public void OnManipulableHoverExit(IInteractable interactable);
    }
}
