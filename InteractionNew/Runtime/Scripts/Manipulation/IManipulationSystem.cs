using Reflectis.SDK.Core;

using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public interface IManipulationSystem : ISystem
    {
        public InteractableBehaviourBase SetupInteractableBehaviour(GameObject obj);

        public List<AwaitableScriptableAction> OnHoverEnterActions { get; }
        public List<AwaitableScriptableAction> OnHoverExitActions { get; }
    }
}
