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

        #endregion

        #region Properties

        public GameObject HalfBodyAvatarReference { get => halfBodyAvatarReference; }

        #endregion


        public override void OnAvatarLoadCompletion(AvatarData avatarData)
        {
            //the avatar loading took too long, now the controller has been destroyed (the player has left the room)
            if (this == null || this.gameObject == null)
            {
                return;
            }
            base.OnAvatarLoadCompletion(avatarData);

            GameObject avatar = Instantiate(avatarData.avatarPrefab);
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
                    FullBodyAvatarReference.transform.localPosition = Vector3.zero;
                    FullBodyAvatarReference.transform.localRotation = Quaternion.identity;
                    if (HalfBodyAvatarReference != null)
                    {
                        HalfBodyAvatarReference.SetActive(false);
                    }
                    foreach (Renderer hand in handsMeshes)
                    {
                        hand.enabled = false;
                    }

                    character.PlayerHeight = CalculateCharacterHeight();

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
                    if (FullBodyAvatarReference != null)
                    {
                        FullBodyAvatarReference.SetActive(false);
                    }
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

            foreach (Renderer renderer in FullBodyAvatarReference.GetComponentsInChildren<Renderer>())
            {
                if (enable)
                    renderer.gameObject.layer = LayerMask.NameToLayer("Default");
                else
                    renderer.gameObject.layer = LayerMask.NameToLayer(layerHidden);
            }
        }


        public override void EnableAvatarMeshes(bool enable)
        {
            // If this avatar is the one controlled by the local player, updates hands
            // visibility. Else, it updates visibility of full body elements.
            if (this.gameObject == avatarSystem.AvatarInstance.gameObject)
            {
                foreach (Renderer rend in handsMeshes.Concat(HalfBodyAvatarReference.GetComponentsInChildren<Renderer>()))
                {
                    rend.enabled = enable;
                }
            }
            else
            {
                if (FullBodyAvatarReference != null)
                {
                    foreach (Renderer rend in FullBodyAvatarReference?.GetComponentsInChildren<Renderer>())
                    {
                        //rend.enabled = enable;
                        rend.gameObject.layer = enable ? LayerMask.NameToLayer("AvatarWelcomeRoom") : LayerMask.NameToLayer("HiddenToPlayer");
                    }
                }
            }

            base.EnableAvatarMeshes(enable);

        }


    }
}
