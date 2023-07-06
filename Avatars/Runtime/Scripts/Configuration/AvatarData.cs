using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.Avatars
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

    public struct AvatarData
    {
        public AvatarGender gender;

        public AvatarBodyType bodyType;

        public Avatar animatorAvatar;
    }
}
