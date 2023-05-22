using SPACS.Core;
using SPACS.SDK.CharacterController;

using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace SPACS.SDK.Avatars
{
    [CreateAssetMenu(menuName = "AnotheReality/Systems/Avatars/AvatarSystem", fileName = "AvatarSystemConfig")]
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

        public async void Spawn(AvatarControllerBase newAvatar)
        {
            await CreateAvatarInstance(newAvatar);

            //AvatarConfigChanged.AddListener(UpdateAvatarInstanceCustomization);
            //PlayerNickNameChanged.AddListener(UpdateAvatarInstanceNickName);
        }

        /// <summary>
        /// 
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

        public void UpdateAvatarInstanceCustomization(IAvatarConfig config) => avatarConfigManager?.UpdateAvatarCustomization(config);
        public void UpdateAvatarInstanceNickName(string newName) => avatarConfigManager?.UpdateAvatarNickName(newName);
        public void EnableAvatarInstanceMeshes(bool enable) => avatarConfigManager?.EnableAvatarMeshes(enable);
        public void EnableAvatarInstanceHandMeshes(bool enable) => avatarConfigManager?.EnableHandMeshes(enable);
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
