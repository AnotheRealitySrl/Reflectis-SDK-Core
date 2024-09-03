
using Reflectis.SDK.Core;

namespace Reflectis.SDK.Diagnostics
{
    public interface IDiagnosticsSystem : ISystem
    {

        string GetUniqueSessionID();

        string GetUniqueNetworkID();

        void SendExperienceDiagnostic(EExperienceDiagnosticVerb verb, string v);
    }
}
