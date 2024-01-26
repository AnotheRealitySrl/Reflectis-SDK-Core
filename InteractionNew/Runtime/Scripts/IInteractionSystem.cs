using Reflectis.SDK.Core;
using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public interface IInteractionSystem : ISystem
    {
        public bool IsTyping { get; set; }
        public bool IsHoveringUI { get; set; }
        public GameObject InteractableUIHovered { get; set; }
    }
}
