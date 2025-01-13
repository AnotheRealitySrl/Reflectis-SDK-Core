using Reflectis.SDK.Core.Avatars;

using System.Threading.Tasks;

using UnityEngine.Events;

namespace Reflectis.SDK.Core.Avatars
{
    /// <summary>
    /// The avatar system manages the lifecycle of the avatar instance, i.e. the avatar associated to the character controller
    /// </summary>
    public interface IAvatarSystem : ISystem
    {
        #region Properties

        /// <summary>
        /// Reference to the avatar associated with the character controller (local 
        /// player's avatar).
        /// </summary>
        AvatarControllerBase AvatarInstance { get; }
        public IAvatarConfigController AvatarInstanceConfigManager
        {
            get;
        }
        #endregion

        #region Unity events

        UnityEvent<IAvatarConfig> OnPlayerAvatarConfigChanged { get; }
        UnityEvent<string> PlayerNickNameChanged { get; }

        #endregion

        #region Avatar lifecycle

        /// <summary>
        /// Updates the avatar instance, i.e. the avatar associated with the character controller.
        /// </summary>
        /// <param name="avatar">The current controller</param>
        /// <returns>Task</returns>
        Task CreateAvatarInstance(AvatarControllerBase avatar);

        /// <summary>
        /// Destroys the current avatar instance
        /// </summary>
        /// <returns>Task</returns>
        Task DestroyAvatarInstance();

        #endregion

        #region Avatar instance customization

        /// <summary>
        /// Updates the configuration of the avatar instances
        /// </summary>
        /// <param name="config">The new configuration of the avatar instance</param>
        /// <param name="onBeforeAction">Called before che configuration update takes place</param>
        /// <param name="onAfterAction">Called after che configuration operation has completed</param>
        void UpdateAvatarInstanceCustomization(IAvatarConfig config);

        /// <summary>
        /// Shows/hides the meshes of the avatar instance
        /// </summary>
        /// <param name="enable"></param>
        void UpdateAvatarInstanceNickName(string newName);

        #endregion

        #region Utility methods

        void EnableAvatarInstance(bool enable);

        /// <summary>
        /// Shows/hides avatar's hands (only for half-body avatars)
        /// </summary>
        /// <param name="enable"></param>
        void EnableAvatarInstanceMeshes(bool enable, bool fromCamera = false);

        /// <summary>
        /// Show/hides avatars that aren't controlled by local player.
        /// </summary>
        /// <param name="enable"></param>
        public void EnableOtherAvatarsMeshes(bool enable);

        /// <summary>
        /// Shows/hides avatar's hands (only for half-body avatars)
        /// </summary>
        /// <param name="enable"></param>
        void EnableAvatarInstanceLabel(bool enable);

        /// <summary>
        /// Reset avatar mesh disabler counter
        /// </summary>
        /// <param name="enable"></param>
        void ResetAvatarMeshDisabler();

        /// <summary>
        /// Reset mesh disabler counter used for other players' avatars
        /// </summary>
        /// <param name="enable"></param>
        void ResetOtherAvatarsMeshDisabler();

        /// <summary>
        /// Adds a entry to the list of istantiated avatars that are not related to 
        /// the local player.
        /// </summary>
        /// <param name="creatorNumber">Actor number of the avatar's controller</param>
        /// <param name="controller">Reference to the IAvatarConfigController component 
        /// on the avatar</param>
        public void AddOtherAvatarReference(int creatorNumber, IAvatarConfigController controller);

        /// <summary>
        /// Removes as entry from the list of istantiated avatars that are not related 
        /// to the local player.
        /// </summary>
        /// <param name="creatorNumber">Actor number of the avatar's controller</param>
        public void RemoveOtherAvatarReference(int creatorNumber);

        /// <summary>
        /// Updates the visibility of the non-local-player avatar.
        /// </summary>
        /// <param name="avatarConfigController"></param>
        public void CheckOtherAvatarActivation(IAvatarConfigController avatarConfigController);

        /// <summary>
        /// Shows/hides a specific hand mesh (only for half-body avatars)
        /// </summary>
        /// <param name="id">The hand to update (0 for left, 1 for right)</param>
        /// <param name="enable"></param>
        public void EnableAvatarInstanceHandMeshes(bool enable);

        /// <summary>
        /// Updates the nickname of the avatar instance (usually shown on top of avatar's head)
        /// </summary>
        /// <param name="newName">The new name</param>
        public void EnableAvatarInstanceHandMesh(int id, bool enable);

        #endregion
    }
}
