using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
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

        public string Language { get => language; set => language = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public AvatarConfigCTO? AvatarConfig { get => avatarConfig; set => avatarConfig = value; }
        public string Social1 { get => social1; set => social1 = value; }
        public string Social2 { get => social2; set => social2 = value; }
        public string Social3 { get => social3; set => social3 = value; }
        public string Bio { get => bio; set => bio = value; }
    }

}
