using Reflectis.SDK.Core;

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
        public int CurrentParticipants { get => currentParticipants; set => currentParticipants = value; }
        public int MaxParticipants
        {
            get
            {
                return Math.Clamp(maxParticipants, 0, SM.GetSystem<IClientModelSystem>().MaxShardCapacity);
            }
            set => maxParticipants = value;
        }

        public CMShard(int shardNumber, int eventId, int currentParticipants, int maxParticipants)
        {
            this.shardNumber = shardNumber;
            this.eventId = eventId;
            this.currentParticipants = currentParticipants;
            this.maxParticipants = maxParticipants;
        }
    }
}
