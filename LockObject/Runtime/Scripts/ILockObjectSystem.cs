using Reflectis.SDK.Core;
using System.Threading.Tasks;
using UnityEngine;

public interface ILockObjectSystem : ISystem
{
    void AssignSavedLockState(GameObject go, bool v);
    Task SetupLockObject(GameObject lockObject, bool isNetworked);
}
