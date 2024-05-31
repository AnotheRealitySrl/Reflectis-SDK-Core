using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.ObjectSpawner
{
    public interface IObjectSpawnerSystem : ISystem
    {
        public Task<GameObject> InstantiateSceneObj(string label, string objectKey, int assetId, Vector3? position, Quaternion? rotation);
    }
}
