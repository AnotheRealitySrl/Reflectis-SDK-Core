using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMOnlinePresence
    {
        [SerializeField] private string id;
        [SerializeField] private string displayName; // nickname
        [SerializeField] private string avatarPng;
        [SerializeField] private int shard;
        [SerializeField] private int eventId;

        public string Id { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string AvatarPng { get => avatarPng; set => avatarPng = value; }
        public int Shard { get => shard; set => shard = value; }
        public int EventId { get => eventId; set => eventId = value; }
    }
}
