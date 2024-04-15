using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public class InteractableCollider : MonoBehaviour
    {
        [SerializeField]
        private EInteractionMode interactionMode;
        [SerializeField]
        private BoundingBox boundingBox;
        [SerializeField]
        private List<Collider> colliders;

        public EInteractionMode InteractionMode { get => interactionMode; set => interactionMode = value; }
        public BoundingBox BoundingBox { get => boundingBox; set => boundingBox = value; }
        public List<Collider> Colliders { get => colliders; set => colliders = value; }
    }
}
