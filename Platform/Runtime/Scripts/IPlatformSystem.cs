using Reflectis.SDK.Core;
using System;

namespace Reflectis.SDK.Platform
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
    }
}
