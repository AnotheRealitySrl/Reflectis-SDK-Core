using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public class ScalablePoint : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        private ModelScaler scaler;

        private void Start()
        {
            if (!meshRenderer)
            {
                meshRenderer = GetComponent<MeshRenderer>();
            }
        }

        public void OnHoverEnter()
        {
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
        }

        public void OnHoverExit()
        {
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
        }
    }
}