using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMShard
    {
        [SerializeField] private int shardNumber;
        [SerializeField] private int eventId;
        [SerializeField] private int currentParticipants;
        [SerializeField] private int maxParticipants;

        public int ShardNumber => shardNumber;
        public int EventId => eventId;
        public int CurrentUsers => currentParticipants;
        public int MaxParticipants => maxParticipants;

        public CMShard(int shardNumber, int eventId, int currentParticipants, int maxParticipants)
        {
            this.shardNumber = shardNumber;
            this.eventId = eventId;
            this.currentParticipants = currentParticipants;
            this.maxParticipants = maxParticipants;
        }
    }
}
