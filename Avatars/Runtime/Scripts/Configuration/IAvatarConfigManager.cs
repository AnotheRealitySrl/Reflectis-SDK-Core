using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.Avatars
{
    public interface IAvatarConfigManager
    {
        public ScriptableObject AvatarConfigTemplates { get; }
        IAvatarConfig AvatarConfig { get; }

        Task UpdateAvatarCustomization(IAvatarConfig config);
        void EnableAvatarMeshes(bool enable);
        void EnableHandMeshes(bool enable);
        void EnableHandMesh(int id, bool enable);
        void UpdateAvatarNickName(string newName);

    }
}
