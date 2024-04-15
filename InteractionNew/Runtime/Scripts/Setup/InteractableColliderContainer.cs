using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public class InteractableColliderContainer : MonoBehaviour
    {
        [Header("Desktop Interaction Modes")]
        [SerializeField]
        private EInteractionMode desktopInteractionMode;

        [SerializeField]
        //[DrawIf(nameof(desktopInteractionMode), EInteractionMode.BoundingBox)]
        private BoxCollider desktopBoundingBox;

        [SerializeField]
        //[DrawIf(nameof(desktopInteractionMode), EInteractionMode.MultipleColliders)]
        private List<Collider> desktopColliders;

        [Space(10)]

        [Header("VR Interaction Modes")]
        [SerializeField]
        private EInteractionMode vrInteractionMode;

        [SerializeField]
        //[DrawIf(nameof(vrInteractionMode), EInteractionMode.BoundingBox)]
        private BoxCollider vrBoundingBox;

        [SerializeField]
        //[DrawIf(nameof(vrInteractionMode), EInteractionMode.MultipleColliders)]
        private List<Collider> vrColliders;

        public EInteractionMode DesktopInteractionMode { get => desktopInteractionMode; set => desktopInteractionMode = value; }
        public BoxCollider DesktopBoundingBox { get => desktopBoundingBox; set => desktopBoundingBox = value; }
        public List<Collider> DesktopColliders { get => desktopColliders; set => desktopColliders = value; }
        public EInteractionMode VrInteractionMode { get => vrInteractionMode; set => vrInteractionMode = value; }
        public BoxCollider VrBoundingBox { get => vrBoundingBox; set => vrBoundingBox = value; }
        public List<Collider> VrColliders { get => vrColliders; set => vrColliders = value; }

    }
}
