using SPACS.Core;
using SPACS.SDK.CharacterController;

using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace SPACS.SDK.Avatars
{
    /// <summary>
    /// System that manages the lyfecicle of my avatar, i.e. the avatar associated to the character controller
    /// </summary>
    [CreateAssetMenu(menuName = "SPACS/SDK-Avatars/AvatarSystem", fileName = "AvatarSystemConfig")]
    public class AvatarSystem : BaseSystem
    {
        #region Inspector variables

        [Header("General")]
        [SerializeField] private bool spawnAvatarOnInit;
        [SerializeField] private bool setupCharacterOnCreation;

        [Header("Avatar Prefab")]
        [SerializeField] private AvatarControllerBase avatarPrefab;

        [Header("Avatar configuration templates")]
        [SerializeField] private ScriptableObject avatarConfigurationTemplates;

        [Header("Layer settings")]
        [SerializeField] private string layerNameHiddenToPlayer = "HiddenToPlayer";

        #endregion

        #region Properties

        public AvatarControllerBase AvatarInstance { get; private set; }
        // TODO: should be removed?
        public ScriptableObject AvatarConfiguratonTemplates => avatarConfigurationTemplates;
        public string LayerNameHiddenToPlayer => layerNameHiddenToPlayer;

        #endregion

        #region Unity events

        public UnityEvent<IAvatarConfig> AvatarConfigChanged { get; } = new();
        public UnityEvent<string> PlayerNickNameChanged { get; } = new();

        #endregion

        #region Private variables

        // The config manager associated to the avatar instance
        private IAvatarConfigManager avatarConfigManager;

        #endregion

        #region System implementation

        public override void Init()
        {
            if (spawnAvatarOnInit)
                Spawn();

            AvatarConfigChanged.AddListener(UpdateAvatarInstanceCustomization);
            PlayerNickNameChanged.AddListener(UpdateAvatarInstanceNickName);
        }

        #endregion

        #region Public API

        /// <summary>
        /// Spawns an avatar. It could be a prefab or an avatar in scene
        /// </summary>
        /// <param name="newAvatar">The avatar to spawn</param>
        public async void Spawn(AvatarControllerBase newAvatar)
        {
            await CreateAvatarInstance(newAvatar);
        }

        /// <summary>
        /// Updates the avatar instance, i.e. the avatar associated with the character controller.
        /// </summary>
        /// <param name="avatar">The current controller</param>
        /// <returns>Task</returns>
        public async Task CreateAvatarInstance(AvatarControllerBase avatar)
        {
            // Destroys the old avatar instance
            if (AvatarInstance)
            {
                await DestroyAvatarInstance();
            }

            CharacterControllerSystem ccs = SM.GetSystem<CharacterControllerSystem>();

            // Checks if the referenced avatar is already in scene
            AvatarInstance = string.IsNullOrEmpty(avatar.gameObject.scene.name)
                ? Instantiate(avatar).GetComponent<AvatarControllerBase>()
                : avatar;
            // Attaches the new avatar instance to the character controller instance
            await AvatarInstance.Setup(ccs.CharacterControllerInstance);

            avatarConfigManager = AvatarInstance.GetComponent<IAvatarConfigManager>();

            if (setupCharacterOnCreation)
            {
                await AvatarInstance.SourceCharacter.Setup();
            }
        }

        /// <summary>
        /// Destroys the current avatar instance
        /// </summary>
        /// <returns>Task</returns>
        public async Task DestroyAvatarInstance()
        {
            await AvatarInstance.Unsetup();

            AvatarInstance = null;
            avatarConfigManager = null;
        }

        /// <summary>
        /// Updates the configuration of the avatar instances
        /// </summary>
        /// <param name="config">The new configuration of the avatar instance</param>
        /// <param name="onBeforeAction">Called before che configuration update takes place</param>
        /// <param name="onAfterAction">Called after che configuration operation has completed</param>
        /// <returns></returns>
        public void UpdateAvatarInstanceCustomization(IAvatarConfig config) => avatarConfigManager?.UpdateAvatarCustomization(config);

        /// <summary>
        /// Shows/hides the meshes of the avatar instance
        /// </summary>
        /// <param name="enable"></param>
        public void UpdateAvatarInstanceNickName(string newName) => avatarConfigManager?.UpdateAvatarNickName(newName);

        /// <summary>
        /// Shows/hides avatar's hands (only for half-body avatars)
        /// </summary>
        /// <param name="enable"></param>
        public void EnableAvatarInstanceMeshes(bool enable) => avatarConfigManager?.EnableAvatarMeshes(enable);

        /// <summary>
        /// Shows/hides a specific hand mesh (only for half-body avatars)
        /// </summary>
        /// <param name="id">The hand to update (0 for left, 1 for right)</param>
        /// <param name="enable"></param>
        public void EnableAvatarInstanceHandMeshes(bool enable) => avatarConfigManager?.EnableHandMeshes(enable);


        /// <summary>
        /// Updates the nickname of the avatar instance (usually shown on top of avatar's head)
        /// </summary>
        /// <param name="newName">The new name</param>
        public void EnableAvatarInstanceHandMesh(int id, bool enable) => avatarConfigManager?.EnableHandMesh(id, enable);

        #endregion

        #region Private methods

        /// <summary>
        /// Called on init, spawns a predefined avatar
        /// </summary>
        private void Spawn() => Spawn(avatarPrefab);

        #endregion
    }
}
