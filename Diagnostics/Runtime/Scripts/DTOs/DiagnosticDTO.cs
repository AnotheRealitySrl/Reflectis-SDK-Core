using UnityEngine;

namespace Reflectis.SDK.Diagnostics
{
    public class DiagnosticDTO
    {
        [SerializeField] private EDiagnosticVerb verb;
        [SerializeField] private int eventId;
        [SerializeField] private string customAttributes;

        public EDiagnosticVerb Verb { get => verb; set => verb = value; }
        public int EventId { get => eventId; set => eventId = value; }
        public string CustomAttributes { get => customAttributes; set => customAttributes = value; }
    }
}
