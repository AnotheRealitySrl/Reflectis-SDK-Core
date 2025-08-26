using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine.Events;

namespace Reflectis.SDK.Core.ApplicationManagement
{
    public interface IApplicationManager
    {
        UnityEvent<Dictionary<string, string>> OnDeepLinkParametersReceived { get; }

        static IApplicationManager Instance { get; protected set; }
        void QuitApplication();
        void ErasePlayerSessionData();
        Task<bool> CheckInternetConnection();
        string GetCurrentDevice();
    }
}
