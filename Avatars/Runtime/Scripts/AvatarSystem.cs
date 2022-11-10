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

        [Header("Avatar Prefab")]
        [SerializeField] private AvatarControllerBase avatarPrefab;

        [Header("Avatar configuration templates")]
        [SerializeField] private ScriptableObject avatarConfigurationTemplates;

        [Header("Layer settings")]
        [SerializeField] private string layerNameHiddenToPlayer = "HiddenToPlayer";

        #endregion

        #region Properties

        // Non serve?
        public AvatarControllerBase AvatarInstance { get; private set; }
        public ScriptableObject AvatarInstanceConfigTemplates => avatarConfigurationTemplates;
        public IAvatarConfig AvatarInstanceConfig => avatarConfigManager?.AvatarConfig;
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

        public override async void Init()
        {
            await CreateAvatarInstance(avatarPrefab);

            AvatarConfigChanged.AddListener(UpdateAvatarInstanceCustomization);
            PlayerNickNameChanged.AddListener(UpdateAvatarInstanceNickName);
        }

        #endregion

        #region Public API

        public async Task CreateAvatarInstance(AvatarControllerBase avatar)
        {
            if (AvatarInstance)
            {
                await DestroyAvatarInstance();
            }

            CharacterControllerSystem ccs = SM.GetSystem<CharacterControllerSystem>();

            AvatarInstance = string.IsNullOrEmpty(avatar.gameObject.scene.name)
                ? Instantiate(avatar).GetComponent<AvatarControllerBase>()
                : avatar;
            await AvatarInstance.Setup(ccs.CharacterControllerInstance);

            avatarConfigManager = AvatarInstance.GetComponent<IAvatarConfigManager>();

            ccs.OnCharacterControllerSetupComplete.Invoke(AvatarInstance);
        }

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
    }
}
