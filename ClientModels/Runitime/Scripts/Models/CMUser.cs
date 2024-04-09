using System;
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMUser
    {
        [SerializeField] private int id;
        [SerializeField] private string nickname;
        [SerializeField] private int code;
        [SerializeField] private string email;
        [SerializeField] private List<CMTag> tags;
        [SerializeField] private string playerImageUrl;
        [SerializeField] private CMUserPreference preferences;

        public int ID { get => id; set => id = value; }
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
        public List<CMTag> Tags { get => tags; set => tags = value; }
        public CMUserPreference Preferences { get => preferences; set => preferences = value; }
        public string PlayerImageUrl
        {
            get
            {
                if (string.IsNullOrEmpty(playerImageUrl))
                {
                    return preferences?.AvatarConfig?.AvatarPng;
                }
                else
                {
                    return playerImageUrl;
                }
            }
            set => playerImageUrl = value;
        }

        public string DisplayName { get; set; } // Use this property for user interfaces!
    }

}
