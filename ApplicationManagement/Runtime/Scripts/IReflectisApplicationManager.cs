using Reflectis.SDK.Utilities;

namespace Reflectis.SDK.ApplicationManagement
{
    public interface IReflectisApplicationManager : IApplicationManager
    {
        static new IReflectisApplicationManager Instance { get; protected set; }

        void InitializeObject(UnityEngine.Object reflectisObject);
    }

}
