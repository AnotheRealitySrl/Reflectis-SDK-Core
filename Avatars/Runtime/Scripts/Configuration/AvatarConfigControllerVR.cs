
using Reflectis.SDK.CharacterController;
using Reflectis.SDK.Core;
using System.Linq;
using UnityEngine;

namespace Reflectis.SDK.Avatars
{
    public class AvatarConfigControllerVR : AvatarConfigControllerBase
    {
        

        #region Inspector variables

        [SerializeField] private GameObject halfBodyAvatarReference;

        [SerializeField, Tooltip("Default height for feminine players")]
        private float defaultFeminineHeight = 1.60f;

        [SerializeField, Tooltip("Default height for anonymous players")]
        private float defaultHeight = 1.65f;

        [SerializeField, Tooltip("Default height for masculine players")]
        private float defaultMasculineHeight = 1.70f;

        #endregion

        #region Properties

        public GameObject HalfBodyAvatarReference { get => halfBodyAvatarReference; }

        #endregion


        public override void OnAvatarLoadCompletion(GameObject avatar, AvatarData avatarData)
        {
            Transform currentPivotParent = null;
            GameObject prevRef = null;
            CharacterBase character = GetComponent<CharacterBase>();
            switch (avatarData.bodyType)
            {
                case AvatarBodyType.FullBody:
                    if (FullBodyAvatarReference != null)
                    {
                        currentPivotParent = FullBodyAvatarReference.transform.parent;
                        prevRef = FullBodyAvatarReference;
                    }
                    FullBodyAvatarReference = avatar;

                    FullBodyAvatarReference.transform.SetParent(currentPivotParent);
                    FullBodyAvatarReference.transform.SetAsFirstSibling();
                    FullBodyAvatarReference.name = "Avatar";
                    HalfBodyAvatarReference.SetActive(false);
                    foreach (Renderer hand in handsMeshes)
                    {
                        hand.enabled = false;
                    }

                    if (AvatarConfig != null && AvatarConfig.PlayerHeight != null)
                        character.PlayerHeight = AvatarConfig.PlayerHeight ?? defaultHeight;
                    else
                        character.PlayerHeight = (avatarData.gender == AvatarGender.Masculine ? defaultMasculineHeight :
                        avatarData.gender == AvatarGender.Feminine ? defaultFeminineHeight :
                            defaultHeight);
                    break;
                case AvatarBodyType.HalfBody:
                    if (HalfBodyAvatarReference != null)
                    {
                        currentPivotParent = HalfBodyAvatarReference.transform.parent;
                        prevRef = HalfBodyAvatarReference;
                    }
                    halfBodyAvatarReference = avatar;
                    HalfBodyAvatarReference.transform.SetParent(currentPivotParent);
                    HalfBodyAvatarReference.transform.SetAsFirstSibling();
                    HalfBodyAvatarReference.transform.localPosition = Vector3.zero;
                    HalfBodyAvatarReference.transform.localRotation = Quaternion.identity;

                    foreach (Renderer hand in handsMeshes)
                    {
                        hand.enabled = true;
                    }
                    FullBodyAvatarReference.SetActive(false);
                    break;
            }
            Material skinMaterial = avatarData.skinMaterial;

            foreach (Renderer hand in handsMeshes)
            {
                hand.material = skinMaterial;
            }

            if (GetComponent<AvatarControllerBase>() == avatarSystem.AvatarInstance)
            {
                //HideMeshesToPlayer();
                SM.GetSystem<CharacterControllerSystem>().OnCharacterControllerSetupComplete?.Invoke(GetComponent<AvatarControllerBase>().CharacterReference);

            }


            character.Setup();

            OnAvatarIstantiated?.Invoke(avatar);


            if (prevRef != avatar)
            {
                Debug.Log("previous ref" + prevRef, prevRef);
                Destroy(prevRef);
            }

        }


        public void EnableFullBodyAvatar(bool enable)
        {
            string layerHidden = SM.GetSystem<AvatarSystem>().LayerNameHiddenToPlayer;

            foreach(Renderer renderer in FullBodyAvatarReference.GetComponentsInChildren<Renderer>())
            {
                if (enable)
                    renderer.gameObject.layer = LayerMask.NameToLayer("Default");
                else
                    renderer.gameObject.layer = LayerMask.NameToLayer(layerHidden);
            }
        }


        public override void EnableAvatarMeshes(bool enable)
        {
            foreach (Renderer rend in handsMeshes.Concat(HalfBodyAvatarReference.GetComponentsInChildren<Renderer>()))
            {
                rend.enabled = enable;
            }

            base.EnableAvatarMeshes(enable);

        }


    }
}
