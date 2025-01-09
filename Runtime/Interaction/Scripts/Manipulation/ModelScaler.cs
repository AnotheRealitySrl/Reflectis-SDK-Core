using UnityEngine;

namespace Reflectis.SDK.Core.Interaction
{
    public abstract class ModelScaler : MonoBehaviour
    {
        public abstract void Setup();
        public abstract void ToggleNonProportionalGizmos(bool enable);
    }
}
