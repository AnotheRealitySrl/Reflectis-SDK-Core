using Reflectis.SDK.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public interface IInteractionSystem : ISystem
    {
        public enum EHoverInteractionController
        {
            Mouse = 1, //Value used in nova UI input manager
            XRLeftControllerRay = 2,
            XRRightControllerRay = 3,
        }

        public bool IsTyping { get; set; }
        public bool IsHoveringUI { get; set; }
        public bool IsHoveringInteractableUI { get; }

        public Dictionary<EHoverInteractionController, GameObject> ControllersOverInteractableUI { get; }

        public GameObject BoundingBoxGraphicPrefab { get; }

        public InteractableCollider SetupInteractableCollider(GameObject gameObject, InteractableColliderContainer interactableColliderPlaceholder);
    }
}
