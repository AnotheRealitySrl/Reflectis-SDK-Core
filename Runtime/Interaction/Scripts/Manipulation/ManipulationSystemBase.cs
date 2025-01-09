using UnityEngine;

namespace Reflectis.SDK.Core.Interaction
{
    public abstract class ManipulationSystemBase : BaseSystem, IManipulationSystem
    {
        [SerializeField] protected GameObject scalablePointFacePrefab;
        [SerializeField] protected Vector2 targetScaleFaces = new Vector3(0.15f, 0.15f, 0.15f);

        public GameObject ScalablePointFacePrefab => scalablePointFacePrefab;

        public abstract void OnManipulableHoverEnter(IInteractable interactable);
        public abstract void OnManipulableHoverExit(IInteractable interactable);

        public abstract InteractableBehaviourBase SetupInteractableBehaviour(GameObject obj);

    }
}
