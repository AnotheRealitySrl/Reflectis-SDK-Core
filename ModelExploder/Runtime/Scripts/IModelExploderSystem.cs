using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine;

public interface IModelExploderSystem : ISystem
{
    Task AssignModelExploder(GameObject obj, bool networkedContext = true);

    void AssignSavedExplosionToModelExploder(float explosionValue, int explosionType, GameObject obj);
}
