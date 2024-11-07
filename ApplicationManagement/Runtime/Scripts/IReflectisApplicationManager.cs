using Reflectis.SDK.ClientModels;
using Reflectis.SDK.Utilities;
using System.Threading.Tasks;
using UnityEngine;

namespace Reflectis.SDK.ApplicationManagement
{
    public interface IReflectisApplicationManager : IApplicationManager
    {
        static new IReflectisApplicationManager Instance { get; protected set; }
        //TO DO: Move this logic inside network system
        bool IsNetworkMaster { get; }

        void InitializeObject(GameObject gameObject, bool initializeChildrens = false);
        Task LoadEvent(CMEvent ev, CMShard shard = null, bool updateHistory = true, bool recoverFromDisconnection = false);
    }

}
