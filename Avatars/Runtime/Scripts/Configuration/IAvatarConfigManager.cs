using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SPACS.SDK.Avatars
{
    /// <summary>
    /// Manages the configuration of an avatar (body, skin, hair, ecc...) 
    /// </summary>
    public interface IAvatarConfigManager
    {
        #region Properties

        /// <summary>
        /// The templates of the configurations of avatars
        /// </summary>
        public ScriptableObject AvatarConfigTemplates { get; }

        /// <summary>
        /// Current avatar configuration
        /// </summary>
        IAvatarConfig AvatarConfig { get; }

        /// <summary>
        /// Instantiating a new avatar is an asynchronous operation. 
        /// CurrentAvatarSetupOperation tracks the completion of the setup operation.
        /// </summary>
        public AsyncOperationHandle<GameObject> CurrentAvatarSetupOperation { get; }

        #endregion

        #region Public API

        /// <summary>
        /// Updates an avatar configuration
        /// </summary>
        /// <param name="config">The new configuration</param>
        /// <param name="onBeforeAction">Called before che configuration update takes place</param>
        /// <param name="onAfterAction">Called after che configuration operation has completed</param>
        /// <returns></returns>
        Task UpdateAvatarCustomization(IAvatarConfig config, Action onBeforeAction = null, Action onAfterAction = null);

        /// <summary>
        /// Shows/hides avatar meshes
        /// </summary>
        /// <param name="enable"></param>
        void EnableAvatarMeshes(bool enable);

        /// <summary>
        /// Shows/hides avatar's hands (only for half-body avatars)
        /// </summary>
        /// <param name="enable"></param>
        void EnableHandMeshes(bool enable);

        /// <summary>
        /// Shows/hides a specific hand mesh (only for half-body avatars)
        /// </summary>
        /// <param name="id">The hand to update (0 for left, 1 for right)</param>
        /// <param name="enable"></param>
        void EnableHandMesh(int id, bool enable);

        /// <summary>
        /// Updates avatar nickname (usually shown on top of avatar's head)
        /// </summary>
        /// <param name="newName">The new name</param>
        void UpdateAvatarNickName(string newName);

        #endregion
    }
}
