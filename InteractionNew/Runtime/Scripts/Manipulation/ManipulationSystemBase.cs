using Reflectis.SDK.Core;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class ManipulationSystemBase : BaseSystem, IManipulationSystem
    {
        [SerializeField] protected GameObject scalablePointFacePrefab;
        [SerializeField] protected Vector2 targetScaleFaces = new Vector3(0.15f, 0.15f, 0.15f);

        public GameObject ScalablePointFacePrefab => scalablePointFacePrefab;

        public abstract void OnManipulableHoverEnter(GameObject boundingBox);
        public abstract void OnManipulableHoverExit(GameObject boundingBox);

        public abstract Manipulable SetupInteractableBehaviour(GameObject obj);

    }
}
