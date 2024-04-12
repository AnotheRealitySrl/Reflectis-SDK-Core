using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class ModelScaler : MonoBehaviour
    {
        public abstract List<GameObject> ScalingCorners
        {
            get;
        }
        public abstract List<GameObject> ScalingFaces
        {
            get;
        }
        public abstract void Setup();
        public abstract void ToggleNonProportionalGizmos(bool enable);
        public abstract void UpdateScalingPointsPosition();
        public abstract void EnableProportionalScaling(bool enable);
        public abstract void EnableNonProportionalScaling();
    }
}
