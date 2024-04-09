using System.Linq;
using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public class BoundingBox : MonoBehaviour
    {
        [SerializeField]
        private Collider _collider;
        public Collider Collider { get => _collider; set => _collider = value; }

        public static BoundingBox GetOrGenerateBoundingBox(GameObject rootObject)
        {
            BoundingBox boundingBox = rootObject.GetComponentInChildren<BoundingBox>();
            if (boundingBox == null)
            {
                GameObject boundingBoxGO = new GameObject("BoundingBox");
                //boundingBoxGO.layer = LayerMask.NameToLayer("BoundingBox");
                boundingBox = boundingBoxGO.AddComponent<BoundingBox>();
                boundingBox._collider = rootObject.GetComponentInChildren<Collider>();
                if (boundingBox._collider == null)
                {
                    boundingBox._collider = boundingBoxGO.AddComponent<BoxCollider>();
                }
                boundingBoxGO.transform.parent = rootObject.transform;
                var renderers = rootObject.GetComponentsInChildren<Renderer>();
                var bounds = renderers.First().bounds;
                foreach (var renderer in renderers.Skip(1))
                {
                    bounds.Encapsulate(renderer.bounds);
                }


                Debug.Log($"bounds: {bounds.center}, {bounds.size}");
                // offset so that the bounding box is centered in zero and apply scale
                boundingBoxGO.transform.localPosition = Vector3.zero;
                boundingBoxGO.transform.localScale = bounds.size;
            }
            return boundingBox;
        }
    }
}
