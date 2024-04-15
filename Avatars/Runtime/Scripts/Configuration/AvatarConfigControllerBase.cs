using Reflectis.SDK.CharacterController;
using Reflectis.SDK.Core;
using Reflectis.SDK.Utilities.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.Avatars
{
    public abstract class AvatarConfigControllerBase : MonoBehaviour, IAvatarConfigController
    {
        #region Inspector variables

        [Header("Avatar elements")]

        [SerializeField, Tooltip("Sorted list of avatar loaders")]
        private AvatarLoadersController avatarLoadersController;

        [SerializeField] private GameObject fullBodyAvatarReference;

        #endregion

        #region Protected variables

        protected AvatarSystem avatarSystem;
        protected ICharacter character;

        protected readonly List<Renderer> handsMeshes = new();
        protected TMP_Text avatarNameText;
        private AvatarLoaderBase avatarLoader;

        protected int avatarCounterEnable = 0;

        protected Bounds combinedBounds;
        #endregion

        #region Properties
        public IAvatarConfig AvatarConfig { get; private set; }

        public AvatarLoaderBase AvatarLoader { get => avatarLoader; set => avatarLoader = value; }

        public GameObject FullBodyAvatarReference { get => fullBodyAvatarReference; protected set => fullBodyAvatarReference = value; }
        #endregion

        #region Unity events

        public UnityEvent OnBeforeInstantiation { get; } = new();
        public UnityEvent<GameObject> OnAvatarIstantiated { get; } = new();

        protected AvatarLoadersController AvatarLoadersController { get => avatarLoadersController; set => avatarLoadersController = value; }

        #endregion

        #region Unity callbacks

        protected virtual void Awake()
        {
            avatarSystem = SM.GetSystem<AvatarSystem>();
            character = GetComponent<ICharacter>();

            AddToHandMeshes(character.LeftInteractorReference);
            AddToHandMeshes(character.RightInteractorReference);

            if (character.LabelReference)
            {
                avatarNameText = character.LabelReference.GetComponentInChildren<TMP_Text>();
            }
        }

        #endregion

        #region Public methods

        public async virtual Task UpdateAvatarCustomization(IAvatarConfig config, Action onBeforeAction = null, Action onAfterAction = null)
        {
            AvatarConfig = config;

            onBeforeAction?.Invoke();

            OnBeforeInstantiation?.Invoke();

            AvatarLoader = Instantiate(AvatarLoadersController.GetAvatarLoader(config));

            AvatarLoader.onLoadingAvatarComplete.AddListenerOnce(OnAvatarLoadCompletion);

            AvatarLoader.onLoadingAvatarComplete.AddListenerOnce((_, _) => { onAfterAction?.Invoke(); });

            await AvatarLoader.LoadAvatar(config);

            float labelPositionY;
            labelPositionY = GetBounds();
            character.LabelReference.transform.localPosition = new Vector3(character.LabelReference.transform.localPosition.x, labelPositionY, character.LabelReference.transform.localPosition.z);

            SM.GetSystem<AvatarSystem>().CheckAvatarActivation();

            onAfterAction?.Invoke();

        }

        public abstract void OnAvatarLoadCompletion(GameObject avatar, AvatarData avatarData);

        public virtual void EnableAvatarMeshes(bool enable)
        {
            EnableAvatarTag(enable);
        }

        public virtual void EnableAvatarTag(bool enable)
        {
            if (character.LabelReference)
            {
                character.LabelReference.gameObject.SetActive(enable);
            }
        }

        public virtual void EnableHandMeshes(bool enable)
        {
            EnableHandMesh(0, enable);
            EnableHandMesh(1, enable);
        }

        public virtual void EnableHandMesh(int id, bool enable)
        {
            handsMeshes[id].enabled = enable;
        }


        public virtual void UpdateAvatarNickName(string newName)
        {
            if (avatarNameText)
            {
                avatarNameText.text = newName;
                avatarNameText.gameObject.SetActive(false);
                StartCoroutine(UpdateTextTransform());

                IEnumerator UpdateTextTransform()
                {
                    yield return new WaitForEndOfFrame();
                    avatarNameText.gameObject.SetActive(true);
                    avatarNameText.GetComponent<RectTransform>().ForceUpdateRectTransforms();
                }
            }
        }

        public virtual int ManageCounterAvatarMeshEnable(bool activate)
        {
            if (!activate)
            {
                avatarCounterEnable++;
            }
            else
            {
                avatarCounterEnable--;
            }

            return avatarCounterEnable;
        }
        #endregion

        #region Protected Methods

        protected void AddToHandMeshes(Transform transform)
        {
            if (transform != null)
            {
                Renderer renderer = transform.GetComponentInChildren<Renderer>();
                if (renderer != null)
                {
                    handsMeshes.Add(renderer);
                    //Debug.Log(renderer.gameObject.name + " renderer ", renderer.gameObject);
                }

            }
        }

        protected float GetBounds()
        {
            Renderer[] childObjects = GetComponentsInChildren<Renderer>();
            combinedBounds = new Bounds();

            if (childObjects.Length == 0)
            {
                return 2f;
            }

            foreach (Renderer renderer in childObjects)
            {
                Bounds bound = renderer.localBounds;
                combinedBounds.Encapsulate(bound);
            }

            float labelPositionY = (float)((combinedBounds.extents.y * 2) - character.LabelOffsetFromBounds);
            return labelPositionY;
        }

        #endregion
    }
}
