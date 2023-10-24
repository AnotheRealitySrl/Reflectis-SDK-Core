using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    public enum HandPreference
    {
        LeftHanded,
        RightHanded
    }

    [Serializable]
    public class CMUserPreference
    {
        [Serializable]
        public class AvatarConfigCTO
        {
            [SerializeField] private string avatarId;
            [SerializeField] private string avatarPng;

            public string AvatarId { get => avatarId; set => avatarId = value; }
            public string AvatarPng { get => avatarPng; set => avatarPng = value; }
        }

        [SerializeField] private string language;
        [SerializeField] private string nickname;
        [SerializeField] private AvatarConfigCTO? avatarConfig;
        [SerializeField] private string social1;
        [SerializeField] private string social2;
        [SerializeField] private string social3;
        [SerializeField] private string bio;
        [SerializeField] private string handPreference;
        [SerializeField] private bool isHand;
        [SerializeField] private Dictionary<string, string> mascotteNames;

        public HandPreference HandPreference
        {
            get => Enum.TryParse(handPreference, out HandPreference _) ? (HandPreference)Enum.Parse(typeof(HandPreference), handPreference) : HandPreference.RightHanded;
            set => handPreference = value.ToString();
        }
        public Dictionary<string, string> MascotteNames { get => mascotteNames; set => mascotteNames = value; }

        public string Language { get => language; set => language = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public AvatarConfigCTO? AvatarConfig { get => avatarConfig; set => avatarConfig = value; }
        public string Social1 { get => social1; set => social1 = value; }
        public string Social2 { get => social2; set => social2 = value; }
        public string Social3 { get => social3; set => social3 = value; }
        public string Bio { get => bio; set => bio = value; }
    }

}
