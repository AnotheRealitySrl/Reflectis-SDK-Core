using Reflectis.SDK.Core;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public interface IManipulationSystem : ISystem
    {
        public Manipulable SetupInteractableBehaviour(GameObject obj);

        public void OnManipulableHoverEnter(GameObject boundingBox);

        public void OnManipulableHoverExit(GameObject boundingBox);
    }
}
