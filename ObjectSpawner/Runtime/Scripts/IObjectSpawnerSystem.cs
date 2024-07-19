using Reflectis.SDK.Core;
using System.Threading.Tasks;
using UnityEngine;

public interface IObjectSpawnerSystem : ISystem
{
    public Task<GameObject> CheckEntireFovAndSpawn(GameObject prefab);
}
