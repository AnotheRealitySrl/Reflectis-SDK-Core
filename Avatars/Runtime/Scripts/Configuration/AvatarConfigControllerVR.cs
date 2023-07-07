
using Reflectis.SDK.CharacterController;
using Reflectis.SDK.Core;
using System.Linq;
using UnityEngine;

namespace Reflectis.SDK.Avatars
{
    public class AvatarConfigControllerVR : AvatarConfigControllerBase
    {
        #region Inspector variablexs
        [SerializeField] private GameObject halfBodyAvatarReference;

        [SerializeField, Tooltip("Default height for feminine players")]
        private float defaultFeminineHeight = 1.60f;

        [SerializeField, Tooltip("Default height for anonymous players")]
        private float defaultHeight = 1.65f;

        [SerializeField, Tooltip("Default height for masculine players")]
        private float defaultMasculineHeight = 1.70f;
        #endregion


        public override void OnAvatarLoadCompletion(GameObject avatar, AvatarData avatarData)
        {
            Transform currentPivotParent = null;
            GameObject prevRef = null;
            CharacterBase character = GetComponent<CharacterBase>();
            switch (avatarData.bodyType)
            {
                case AvatarBodyType.FullBody:
                    if (fullBodyAvatarReference != null)
                    {
                        currentPivotParent = fullBodyAvatarReference.transform.parent;
                        prevRef = fullBodyAvatarReference;
                    }
                    fullBodyAvatarReference = avatar;

                    fullBodyAvatarReference.transform.SetParent(currentPivotParent);
                    fullBodyAvatarReference.transform.SetAsFirstSibling();
                    fullBodyAvatarReference.name = "Avatar";
                    halfBodyAvatarReference.SetActive(false);
                    foreach (Renderer hand in handsMeshes)
                    {
                        hand.enabled = false;
                    }

                    if (AvatarConfig != null && AvatarConfig.PlayerHeight != null)
                        character.playerHeight = AvatarConfig.PlayerHeight ?? defaultHeight;
                    else
                        character.playerHeight = (avatarData.gender == AvatarGender.Masculine ? defaultMasculineHeight :
                        avatarData.gender == AvatarGender.Feminine ? defaultFeminineHeight :
                            defaultHeight);
                    break;
                case AvatarBodyType.HalfBody:
                    if (halfBodyAvatarReference != null)
                    {
                        currentPivotParent = halfBodyAvatarReference.transform.parent;
                        prevRef = halfBodyAvatarReference;
                    }
                    halfBodyAvatarReference = avatar;
                    halfBodyAvatarReference.transform.SetParent(currentPivotParent);
                    halfBodyAvatarReference.transform.SetAsFirstSibling();
                    halfBodyAvatarReference.transform.localPosition = Vector3.zero;
                    halfBodyAvatarReference.transform.localRotation = Quaternion.identity;

                    Material skinMaterial = avatarData.skinMaterial;

                    foreach (Renderer hand in handsMeshes)
                    {
                        hand.enabled = true;
                        hand.material = skinMaterial;
                    }
                    fullBodyAvatarReference.SetActive(false);
                    break;
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

        public override void EnableAvatarMeshes(bool enable)
        {
            foreach (Renderer rend in handsMeshes.Concat(halfBodyAvatarReference.GetComponentsInChildren<Renderer>()))
            {
                rend.enabled = enable;
            }

            base.EnableAvatarMeshes(enable);

        }


    }
}
