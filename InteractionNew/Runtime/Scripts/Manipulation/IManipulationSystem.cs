using Reflectis.SDK.Core;

using System.Collections.Generic;

namespace Reflectis.SDK.InteractionNew
{
    public interface IManipulationSystem : ISystem
    {
        public List<AwaitableScriptableAction> OnHoverEnterActions { get; }
        public List<AwaitableScriptableAction> OnHoverExitActions { get; }
    }
}
