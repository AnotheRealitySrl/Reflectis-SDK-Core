using UnityEngine;

namespace Reflectis.SDK.Diagnostics
{
    public class DiagnosticDTO
    {
        [SerializeField] private EDiagnosticVerb verb;
        [SerializeField] private string detail;
        [SerializeField] private int eventId;
        [SerializeField] private int shardId;
        [SerializeField] private int userId;

        public DiagnosticDTO(EDiagnosticVerb verb, string detail, int eventId, int shardId, int userId)
        {
            this.verb = verb;
            this.detail = detail;
            this.eventId = eventId;
            this.shardId = shardId;
            this.userId = userId;
        }

        public EDiagnosticVerb Verb { get => verb; }
        public string Detail { get => detail; }
        public int EventId { get => eventId; }
        public int ShardId { get => shardId; }
        public int UserId { get => userId; }
    }
}
