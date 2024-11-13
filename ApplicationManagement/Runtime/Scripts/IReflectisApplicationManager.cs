using Reflectis.SDK.ClientModels;
using Reflectis.SDK.Utilities;
using System.Threading.Tasks;
using UnityEngine;

namespace Reflectis.SDK.ApplicationManagement
{
    public interface IReflectisApplicationManager : IApplicationManager
    {
        static new IReflectisApplicationManager Instance { get; protected set; }
        Task InitializeObject(GameObject gameObject, bool initializeChildren = false);
        Task LoadEvent(CMEvent ev, CMShard shard = null, bool updateHistory = true, bool recoverFromDisconnection = false);
    }

}
