using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMOnlinePresence
    {
        public enum Platform
        {
            None,
            WebGL,
            VR
        }

        [SerializeField] private int id;
        [SerializeField] private string nickname;
        [SerializeField] private int code;
        [SerializeField] private string email;
        [SerializeField] private Platform currentPlatform;
        [SerializeField] private string avatarPng;
        [SerializeField] private int shard;
        [SerializeField] private int eventId;
        [SerializeField] private List<CMTag> tags;

        public int Id { get => id; set => id = value; }
        public string Nickname
        {
            get => nickname; set
            {
                nickname = value;
                if (string.IsNullOrEmpty(DisplayName))
                {
                    DisplayName = value;
                }
            }
        }
        public int Code { get => code; set => code = value; }
        public string Email { get => email; set => email = value; }
        public Platform CurrentPlatform { get => currentPlatform; set => currentPlatform = value; }
        public string AvatarPng { get => avatarPng; set => avatarPng = value; }
        public int Shard { get => shard; set => shard = value; }
        public int EventId { get => eventId; set => eventId = value; }
        public List<CMTag> Tags { get => tags; set => tags = value; }

        public string DisplayName { get; set; } // Use this property for user interfaces!
    }
}
