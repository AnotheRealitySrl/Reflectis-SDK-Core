using Reflectis.SDK.Core.SystemFramework;

using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.Core.Interaction
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

    }
}
