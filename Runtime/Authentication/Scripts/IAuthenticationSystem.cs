using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Core.Utilities;

using System;
using System.Threading.Tasks;

using UnityEngine.Events;

namespace Reflectis.SDK.Core.Authentication
{
    public interface IAuthenticationSystem : ISystem
    {
        public enum EAuthStatus
        {
            LoadingSession,
            NoSession,
            NewSession,
            NewSessionPolling,
            Authenticated
        }

        [Flags]
        public enum EAuthentication
        {
            None = 0,
            Bearer = 1,
            Hmac = 2,
            BearerAndHmac = Bearer | Hmac
        }

        UnityEvent<EAuthStatus> OnAuthStatusChange { get; }

        JwtToken FindToken(string apiLabel);
        Task GetTokens();
        Task ReloadSession(string sessionHash);
    }
}
