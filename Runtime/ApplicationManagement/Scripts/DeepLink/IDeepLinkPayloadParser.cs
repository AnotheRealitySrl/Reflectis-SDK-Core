using System.Collections.Generic;

namespace Reflectis.SDK.Core.ApplicationManagement
{
    public interface IDeepLinkPayloadParser
    {
        static IDeepLinkPayloadParser Instance { get; protected set; }
        void ParseDeepLinkPayload(Dictionary<string, string> parameters);
    }
}
