using Reflectis.SDK.Core;

namespace Reflectis.SDK.InteractionNew
{
    public interface IInteractionSystem : ISystem
    {
        public bool IsTyping { get; set; }
        public bool IsHoveringUI { get; set; }
    }
}
