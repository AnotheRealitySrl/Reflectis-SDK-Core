using System.Threading.Tasks;

namespace Reflectis.SDK.Utilities
{
    public interface IApplicationManager
    {
        static IApplicationManager Instance { get; protected set; }

        void QuitApplication();
        void ErasePlayerSessionData();
        Task<bool> CheckInternetConnection();
    }
}
