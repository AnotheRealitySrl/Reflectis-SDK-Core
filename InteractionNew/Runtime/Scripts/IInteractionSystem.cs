using Reflectis.SDK.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public interface IInteractionSystem : ISystem
    {
        public bool IsTyping { get; set; }
        public bool IsHoveringUI { get; set; }
        public List<GameObject> InteractableUIHovered { get; set; }
    }
}
