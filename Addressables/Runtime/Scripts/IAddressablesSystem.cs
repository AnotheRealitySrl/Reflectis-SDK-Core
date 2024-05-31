using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine;

public interface IAddressablesSystem : ISystem
{
    public Task<GameObject> InstantiateAsync(string objectKey, Vector3? position, Quaternion? rotation);
}
