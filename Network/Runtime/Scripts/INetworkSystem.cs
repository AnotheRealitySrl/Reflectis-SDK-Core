using Reflectis.SDK.Core;
using System.Threading.Tasks;
using UnityEngine;

public interface INetworkSystem : ISystem
{
    Task<GameObject> InstantiateOnNetwork(string objectKey, Vector3 position, Vector3 eulerAngles);
}
