using Reflectis.SDK.Utilities.Extensions;

using System.Threading.Tasks;

using UnityEngine.Networking;
#if !UNITY_WEBGL
using UnityEngine;
#endif
namespace Reflectis.SDK.Utilities
{
    public static class NetworkUtilities
    {

        private static int PING_MAX_SECONDS_DELAY = 2;

        public static async Task<bool> CheckUserInternetConnection()
        {
            using var request = UnityWebRequest.Head("8.8.8.8");
            request.timeout = PING_MAX_SECONDS_DELAY;
            await request.SendWebRequest();

            return request.result == UnityWebRequest.Result.Success;
        }

    }
}
