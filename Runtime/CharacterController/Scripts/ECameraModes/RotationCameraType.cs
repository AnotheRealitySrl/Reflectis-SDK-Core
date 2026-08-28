using System;
using UnityEngine;

namespace Virtuademy.SDK.Core
{
    [Serializable]
    public class RotationCameraType
    {
        //[SettableField(isRequired = true)]
        public bool rotationY;
        public bool rotationX;
        public bool constrainedRotation;
        public bool leftButtonToRotate;
        //public bool mouseWheelZoom;
        //public bool invertX;
        //public bool invertY;
    }
}
