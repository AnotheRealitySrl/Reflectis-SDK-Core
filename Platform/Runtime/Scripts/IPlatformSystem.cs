using Reflectis.SDK.Core;
using UnityEngine;

namespace Reflectis.SDK.Platform
{
    public interface IPlatformSystem : ISystem
    {
        RuntimePlatform RuntimePlatform { get; }
    }
}
