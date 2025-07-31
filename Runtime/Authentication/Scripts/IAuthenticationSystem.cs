using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Core.Utilities;

using System;
using System.Threading.Tasks;

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

        JwtToken GetToken(string apiLabel);
        Task<JwtToken> RefreshToken(string apiLabel);
    }
}
