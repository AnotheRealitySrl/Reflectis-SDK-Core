using System;

using UnityEngine;

namespace Reflectis.SDK.Core.ApplicationManagement
{
    [Flags]
    public enum ESupportedPlatform
    {
        VR = 1,
        WebGL = 2
    }

    public interface IPlatformSystem : ISystem
    {
        ESupportedPlatform RuntimePlatform { get; }

        public Sprite GetSprite(ESupportedPlatform platform);
    }
}
