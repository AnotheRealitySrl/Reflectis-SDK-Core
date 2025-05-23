using System;
using UnityEngine;

namespace Reflectis.SDK.Core.Avatars
{
    public enum AvatarGender
    {
        Feminine,
        Masculine,
        Other
    }

    public enum AvatarBodyType
    {
        HalfBody = 0,
        FullBody = 1,
    }
    [Serializable]
    public struct AvatarData
    {
        public GameObject avatarPrefab;

        public AvatarGender gender;

        public AvatarBodyType bodyType;

        public Avatar animatorAvatar;

        public Material skinMaterial;
    }
}
