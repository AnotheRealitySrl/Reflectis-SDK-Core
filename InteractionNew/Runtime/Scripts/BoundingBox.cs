using Reflectis.SDK.Core;
using System.Linq;
using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public class BoundingBox : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider _collider;
        [SerializeField]
        private Renderer _renderer;
        public BoxCollider Collider { get => _collider; set => _collider = value; }
        public Renderer Renderer { get => _renderer; set => _renderer = value; }

        public static BoundingBox GetOrGenerateBoundingBox(GameObject rootObject, BoxCollider collider = null)
        {
            BoundingBox boundingBox = rootObject.GetComponentInChildren<BoundingBox>();
            if (boundingBox == null)
            {
                GameObject boundingBoxGO = new GameObject("BoundingBox");
                boundingBox = boundingBoxGO.AddComponent<BoundingBox>();
                //boundingBoxGO.layer = LayerMask.NameToLayer("BoundingBox");

                boundingBoxGO.transform.parent = rootObject.transform;

                if (collider)
                {
                    boundingBox.Collider = collider;
                }
                else
                {
                    if (collider == null)
                    {
                        boundingBox._collider = boundingBoxGO.AddComponent<BoxCollider>();
                        var renderers = rootObject.GetComponentsInChildren<Renderer>();
                        var bounds = renderers.First().bounds;
                        foreach (var childRenderer in renderers.Skip(1))
                        {
                            bounds.Encapsulate(childRenderer.bounds);
                        }
                        boundingBox._collider.isTrigger = true;

                        Debug.Log($"bounds: {bounds.center}, {bounds.size}");
                        // offset so that the bounding box is centered in zero and apply scale
                        boundingBoxGO.transform.position = bounds.center;
                        boundingBoxGO.transform.localScale = bounds.size;
                        boundingBoxGO.transform.localRotation = Quaternion.identity;
                    }
                    else
                    {
                        boundingBox._collider = boundingBoxGO.AddComponent<BoxCollider>();
                        boundingBoxGO.transform.localPosition = collider.center;
                        boundingBoxGO.transform.localScale = new Vector3(collider.size.x, collider.size.y, collider.size.z);
                        Destroy(boundingBox);
                    }
                    if (boundingBox.Renderer == null)
                    {
                        var boundingBoxGraphic = GameObject.Instantiate(SM.GetSystem<IInteractionSystem>().BoundingBoxGraphicPrefab);
                        boundingBoxGraphic.transform.parent = boundingBoxGO.transform;
                        boundingBoxGraphic.transform.localPosition = new Vector3();
                        boundingBoxGraphic.transform.localRotation = Quaternion.identity;
                        boundingBoxGraphic.transform.localScale = Vector3.one;
                        boundingBox.Renderer = boundingBoxGraphic.GetComponent<Renderer>();
                        boundingBox.Renderer.enabled = false;
                    }
                }
            }
            return boundingBox;
        }
    }
}
