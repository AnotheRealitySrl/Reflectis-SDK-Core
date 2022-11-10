using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.SDK.Avatars
{
    public interface IAvatarConfigManager
    {
        IAvatarConfig AvatarConfig { get; }

        void UpdateAvatarCustomization(IAvatarConfig config);
        void EnableAvatarMeshes(bool enable);
        void EnableHandMeshes(bool enable);
        void EnableHandMesh(int id, bool enable);
        void UpdateAvatarNickName(string newName);

    }
}
