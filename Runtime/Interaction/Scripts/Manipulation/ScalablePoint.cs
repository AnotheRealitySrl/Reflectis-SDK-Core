using UnityEngine;

namespace Reflectis.SDK.Core.Interaction
{
    public class ScalablePoint : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

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