using Reflectis.SDK.Core;

using System.Collections.Generic;

namespace Reflectis.SDK.InteractionNew
{
    public interface IManipulationSystem : ISystem
    {
        public void SetupManipulable(Manipulable manipulable);
        public void SetScalingPoints(Manipulable manipulable);
        public void UpdateScalingPointsPosition(Manipulable manipulable);

        public List<AwaitableScriptableAction> OnHoverEnterActions { get; }
        public List<AwaitableScriptableAction> OnHoverExitActions { get; }
    }
}
