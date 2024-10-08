using Reflectis.SDK.Utilities;
using UnityEngine;

namespace Reflectis.SDK.Diagnostics
{
    public class DiagnosticDTO
    {
        [SerializeField] private EDiagnosticVerb verb;
        [SerializeField] private int eventId;
        [SerializeField] private CustomType[] customAttributes;

        public EDiagnosticVerb Verb { get => verb; set => verb = value; }
        public int EventId { get => eventId; set => eventId = value; }
        public CustomType[] CustomAttributes { get => customAttributes; set => customAttributes = value; }
    }
}
