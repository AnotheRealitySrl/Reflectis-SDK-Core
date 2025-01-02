using UnityEngine;

namespace Reflectis.SDK.Avatars
{
    public class AvatarConfigBase : IAvatarConfig
    {
        [SerializeField] private string avatarId;
        [SerializeField] private string avatarPNG;
        [SerializeField] private float? playerHeight;

        public virtual string AvatarId { get => avatarId; set => avatarId = value; }
        public virtual float? PlayerHeight { get => playerHeight; set => playerHeight = value; }
        public virtual string AvatarPNG { get => avatarPNG; set => avatarPNG = value; }
    }
}
