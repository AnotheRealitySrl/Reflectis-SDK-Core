using UnityEngine;

namespace Reflectis.SDK.Diagnostics
{
    public class ExperienceDiagnosticDTO
    {
        [SerializeField] private EExperienceDiagnosticVerb verb;
        [SerializeField] private string detail;
        [SerializeField] private int eventId;
        [SerializeField] private int shardId;
        [SerializeField] private int userId;

        public ExperienceDiagnosticDTO(EExperienceDiagnosticVerb verb, string detail, int eventId, int shardId, int userId)
        {
            this.verb = verb;
            this.detail = detail;
            this.eventId = eventId;
            this.shardId = shardId;
            this.userId = userId;
        }

        public EExperienceDiagnosticVerb Verb { get => verb; }
        public string Detail { get => detail; }
        public int EventId { get => eventId; }
        public int ShardId { get => shardId; }
        public int UserId { get => userId; }
    }
}
