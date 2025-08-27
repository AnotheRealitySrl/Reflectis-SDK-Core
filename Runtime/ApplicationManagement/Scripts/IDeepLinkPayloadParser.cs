using System.Collections.Generic;

namespace Reflectis.SDK.Core.ApplicationManagement.Samples
{
    public interface IDeepLinkPayloadParser
    {
        void ParseDeepLinkPayload(Dictionary<string, string> parameters);
    }
}
