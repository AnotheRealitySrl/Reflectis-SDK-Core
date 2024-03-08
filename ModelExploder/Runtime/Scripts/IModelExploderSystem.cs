using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine;

public interface IModelExploderSystem : ISystem
{
    Task AssignModelExploder(GameObject obj);

    void AssignSavedExplosionToModelExploder(float explosionValue, int explosionType, GameObject obj);
}
