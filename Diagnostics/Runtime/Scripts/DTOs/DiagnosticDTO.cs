using UnityEngine;

namespace Reflectis.SDK.Diagnostics
{
    public class DiagnosticDTO
    {
        [SerializeField] private EDiagnosticVerb verb;
        [SerializeField] private int eventId;

        [SerializeField][SettableField] public AnalyticsDisplayableData customAttributes;

        public EDiagnosticVerb Verb { get => verb; set => verb = value; }
        public int EventId { get => eventId; set => eventId = value; }
    }
}
