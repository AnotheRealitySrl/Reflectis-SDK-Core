using Reflectis.SDK.Core;
using System.Threading.Tasks;
using UnityEngine;

public interface ILockObjectSystem : ISystem
{
    Task SetupLockObject(GameObject lockObject, bool isNetworked);
}
