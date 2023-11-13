using UnityEngine; 

namespace Reflectis.SDK.InteractionNew
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
            meshRenderer.enabled = true;
        }

        public void OnHoverExit()
        { 
            meshRenderer.enabled = false;
        }
    }
}