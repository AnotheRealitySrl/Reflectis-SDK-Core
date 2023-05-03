using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SPACS.SDK.Avatars
{
    public interface IAvatarConfigManager
    {
        public ScriptableObject AvatarConfigTemplates { get; }
        IAvatarConfig AvatarConfig { get; }
        public AsyncOperationHandle<GameObject> CurrentAvatarSetupOperation { get; }

        Task UpdateAvatarCustomization(IAvatarConfig config, Action onBeforeAction = null, Action onAfterAction = null);
        void EnableAvatarMeshes(bool enable);
        void EnableHandMeshes(bool enable);
        void EnableHandMesh(int id, bool enable);
        void UpdateAvatarNickName(string newName);

    }
}
