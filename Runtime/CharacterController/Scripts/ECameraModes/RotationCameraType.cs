using System;
using UnityEngine;

namespace Reflectis.SDK.Core
{
    [Serializable]
    public class RotationCameraType
    {
        //[SettableField(isRequired = true)]
        public bool rotationY;
        public bool rotationX;
        public bool invertX;
        public bool invertY;
        public bool lockRotation;
        public bool mouseWheelZoom;
    }
}
