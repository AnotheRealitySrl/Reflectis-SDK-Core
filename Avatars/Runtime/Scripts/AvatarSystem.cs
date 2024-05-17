
using Reflectis.SDK.CharacterController;
using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.Avatars
{
    [CreateAssetMenu(menuName = "Reflectis/SDK-Avatars/AvatarSystem", fileName = "AvatarSystemConfig")]
    public class AvatarSystem : BaseSystem, IAvatarSystem
    {
        #region Inspector variables

        [Header("Initialization")]
        [SerializeField, Tooltip("Create an avatar instance on system init")]
        private bool createAvatarInstanceOnInit = true;

        [SerializeField, Tooltip("Is the avatar already in scene or should be instantiated from a prefab?")]
        private bool avatarAlreadyInScene;

        [Header("Avatar instantiation")]
        [SerializeField, Tooltip("Reference to the avatar prefab")]
        private AvatarControllerBase avatarPrefab;
        [SerializeField, Tooltip("Reference to the network avatar prefab")]
        private AvatarControllerBase networkAvatarPrefab;

        [Header("General settings")]
        [SerializeField, Tooltip("If true, once a new avatar instance is created, the method Setup of its SourceCharacter is called")]
        private bool setupAvatarInstanceAutomatically;

        [SerializeField, Tooltip("Objects in this layer are hidden to the player (useful for VR avatars)")]
        private string layerNameHiddenToPlayer = "HiddenToPlayer";

        #endregion

        #region Properties

        public AvatarControllerBase AvatarInstance { get; private set; }
        public string LayerNameHiddenToPlayer => layerNameHiddenToPlayer;
        public IAvatarConfigController AvatarInstanceConfigManager { get => avatarInstanceConfigManager; }

        public AvatarControllerBase AvatarPrefab { get => avatarPrefab; }
        public AvatarControllerBase AvatarNetworkPrefab { get => networkAvatarPrefab; }
        #endregion

        #region Unity events

        public UnityEvent<IAvatarConfig> OnPlayerAvatarConfigChanged { get; } = new();
        public UnityEvent<string> PlayerNickNameChanged { get; } = new();

        #endregion

        #region Private variables

        // The config manager associated to the avatar instance
        private IAvatarConfigController avatarInstanceConfigManager;

        private bool cameraDisable;

        private int avatarMeshDisablerCounter;

        #endregion

        #region System implementation

        public override async Task Init()
        {
            if (createAvatarInstanceOnInit)
            {
                CreateAvatarAtInit();
            }
            OnPlayerAvatarConfigChanged.AddListener(UpdateAvatarInstanceCustomization);
            PlayerNickNameChanged.AddListener(UpdateAvatarInstanceNickName);

            await base.Init();
        }

        public override void Finish()
        {
            OnPlayerAvatarConfigChanged.RemoveListener(UpdateAvatarInstanceCustomization);
            PlayerNickNameChanged.RemoveListener(UpdateAvatarInstanceNickName);
        }

        private async void CreateAvatarAtInit()
        {
            if (avatarAlreadyInScene)
            {
                if (FindObjectOfType<AvatarControllerBase>() is AvatarControllerBase avatarController)
                {
                    await CreateAvatarInstance(avatarController);
                }
                else
                {
                    throw new System.Exception("Avatar not found in scene");
                }
            }
            else
            {
                if (avatarPrefab)
                {
                    await CreateAvatarInstance(avatarPrefab);
                }
                else
                {
                    throw new System.Exception("Avatar prefab not specified");
                }
            }
        }

        #endregion

        #region Public API

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

            avatarInstanceConfigManager = AvatarInstance.GetComponent<IAvatarConfigController>();

            //avatarInstanceConfigManager.OnAvatarIstantiated.AddListener(async (_) => await AvatarInstance.Setup(ccs.CharacterControllerInstance));
            if (setupAvatarInstanceAutomatically)
            {
                await AvatarInstance.CharacterReference.Setup();
                ccs.OnCharacterControllerSetupComplete.Invoke(AvatarInstance.CharacterReference);
            }
        }

        public async Task DestroyAvatarInstance()
        {
            if (AvatarInstance)
            {
                await AvatarInstance.Unsetup();
            }

            AvatarInstance = null;
            avatarInstanceConfigManager = null;
        }

        public void UpdateAvatarInstanceCustomization(IAvatarConfig config) => AvatarInstanceConfigManager?.UpdateAvatarCustomization(config);
        public void UpdateAvatarInstanceNickName(string newName) => AvatarInstanceConfigManager?.UpdateAvatarNickName(newName);

        public void EnableAvatarInstance(bool enable)
        {
            if (AvatarInstance)
            {
                AvatarInstance.gameObject.SetActive(enable);
            }
        }

        public void EnableAvatarInstanceMeshes(bool enable, bool fromCamera = false)
        {
            if (fromCamera)
            {
                cameraDisable = !enable;
            }
            else
            {
                if (enable)
                {
                    avatarMeshDisablerCounter--;
                }
                else
                {
                    avatarMeshDisablerCounter++;
                }
            }
            CheckAvatarActivation();
        }

        public void EnableAvatarInstanceLabel(bool enable)
        {

            if (enable)
            {
                AvatarInstanceConfigManager?.EnableAvatarLabel(true);
            }
            else
            {
                AvatarInstanceConfigManager?.EnableAvatarLabel(false);
            }
        }
        public void ResetAvatarMeshDisabler()
        {
            avatarMeshDisablerCounter = 0;
        }

        public void EnableAvatarInstanceHandMeshes(bool enable) => AvatarInstanceConfigManager?.EnableHandMeshes(enable);
        public void EnableAvatarInstanceHandMesh(int id, bool enable) => AvatarInstanceConfigManager?.EnableHandMesh(id, enable);

        internal void CheckAvatarActivation()
        {
            if (avatarMeshDisablerCounter <= 0 && !cameraDisable)
            {
                AvatarInstanceConfigManager?.EnableAvatarMeshes(true);
            }
            if (avatarMeshDisablerCounter >= 1 || cameraDisable)
            {
                AvatarInstanceConfigManager?.EnableAvatarMeshes(false);
            }
        }

        #endregion
    }
}