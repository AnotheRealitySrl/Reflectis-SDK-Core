using Reflectis.SDK.Core;
using System.Threading.Tasks;
using UnityEngine;

public interface ILockObjectSystem : ISystem
{
    Task LockObject(GameObject obj, bool networkedContext = true);

}
