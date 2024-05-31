using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine;

public interface INetworkingSystem : ISystem
{
    Task<GameObject> InstantiateOnNetwork(string objectKey, Vector3? position, Vector3? rotation);
}
