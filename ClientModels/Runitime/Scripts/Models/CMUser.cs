using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMUser
    {
        [SerializeField] private int id;
        [SerializeField] private string displayName;
        [SerializeField] private List<CMTag> tags;
        [SerializeField] private string playerImageUrl;
        [SerializeField] private CMUserPreference preferences;

        public int ID { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
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
    }

}
