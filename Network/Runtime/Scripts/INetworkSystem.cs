using Reflectis.SDK.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface INetworkSystem : ISystem
{
    Task<GameObject> InstantiateOnNetwork(string objectId, object[] data, Vector3 position, Quaternion rotation);

    Task PreloadObjects(List<string> objectKeys);

}
