using Reflectis.SDK.Core;
using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class ManipulationSystemBase : BaseSystem, IManipulationSystem
    {
        [SerializeField] protected GameObject scalablePointFacePrefab;
        [SerializeField] protected Vector2 targetScaleFaces = new Vector3(0.15f, 0.15f, 0.15f);

        public GameObject ScalablePointFacePrefab => scalablePointFacePrefab;

        public virtual void OnManipulableHoverEnter(GameObject boundingBox)
        {
            if (boundingBox)
            {
                var meshRenderer = boundingBox.GetComponentInChildren<MeshRenderer>(true);
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = true;
                }
            }
        }

        public virtual void OnManipulableHoverExit(GameObject boundingBox)
        {
            if (boundingBox)
            {
                var meshRenderer = boundingBox.GetComponentInChildren<MeshRenderer>(true);
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }
            }
        }

        public abstract Manipulable SetupInteractableBehaviour(GameObject obj);

    }
}
