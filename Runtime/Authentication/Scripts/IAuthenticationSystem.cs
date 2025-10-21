using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Core.Utilities;

using System;
using System.Threading.Tasks;

using UnityEngine.Events;

namespace Reflectis.SDK.Core.Authentication
{
    public interface IAuthenticationSystem : ISystem
    {
        [Flags]
        public enum EAuthentication
        {
            None = 0,
            Bearer = 1,
            Hmac = 2,
            BearerAndHmac = Bearer | Hmac
        }

        UnityEvent OnAuthenticated { get; }
        UnityEvent OnUnauthenticated { get; }
        UnityEvent<long, string> OnAuthenticationError { get; }

        JwtToken FindToken(string apiLabel);
        Task GetTokens();
        Task ReloadSession(string sessionHash);
    }
}
