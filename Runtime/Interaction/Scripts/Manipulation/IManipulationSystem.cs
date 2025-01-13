using UnityEngine;

namespace Reflectis.SDK.Core.Interaction
{
    public interface IManipulationSystem : ISystem
    {
        public InteractableBehaviourBase SetupInteractableBehaviour(GameObject obj);

        public void OnManipulableHoverEnter(IInteractable interactable);

        public void OnManipulableHoverExit(IInteractable interactable);
    }
}
