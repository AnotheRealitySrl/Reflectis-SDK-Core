using Reflectis.SDK.Utilities;

namespace Reflectis.SDK.ApplicationManagement
{
    public interface IReflectisApplicationManager : IApplicationManager
    {
        static new IReflectisApplicationManager Instance { get; protected set; }
        //TO DO: Move this logic inside network system
        bool IsNetworkMaster { get; }

        void InitializeObject(UnityEngine.Object reflectisObject);
    }

}
