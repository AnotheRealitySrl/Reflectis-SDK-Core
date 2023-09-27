using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMOnlinePresence
    {
        [SerializeField] private int id;
        [SerializeField] private string displayName;
        [SerializeField] private string avatarPng;
        [SerializeField] private int shard;
        [SerializeField] private int eventId;

        public int Id { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string AvatarPng { get => avatarPng; set => avatarPng = value; }
        public int Shard { get => shard; set => shard = value; }
        public int EventId { get => eventId; set => eventId = value; }
    }
}
