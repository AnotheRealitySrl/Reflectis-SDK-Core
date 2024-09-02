
using Reflectis.SDK.Core;

namespace Reflectis.SDK.Diagnostics
{
    public interface IDiagnosticsSystem : ISystem
    {
        void SendExperienceDiagnostic(EExperienceDiagnosticVerb verb, string v);
    }
}
