using Reflectis.SDK.CharacterController;
using Reflectis.SDK.Core;
using UnityEngine;

namespace Reflectis.SDK.Avatars
{
    public class AvatarConfigControllerDesktop : AvatarConfigControllerBase
    {
        #region Inspector variables

        [SerializeField, Tooltip("Default animator for masculine avatars")]
        private RuntimeAnimatorController masculineAnimatorController;

        [SerializeField, Tooltip("Default animator for feminine avatars")]
        private RuntimeAnimatorController feminineAnimatorController;

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
            if (FullBodyAvatarReference != null)
            {
                currentPivotParent = FullBodyAvatarReference.transform.parent;
                prevRef = FullBodyAvatarReference;
            }
            FullBodyAvatarReference = avatar;
            FullBodyAvatarReference.transform.SetParent(currentPivotParent, false);
            FullBodyAvatarReference.transform.SetAsFirstSibling();
            FullBodyAvatarReference.name = "Armature";

            foreach (Transform child in FullBodyAvatarReference.transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("AvatarWelcomeRoom");
            }

            Animator parentAnimator = FullBodyAvatarReference?.transform.parent?.GetComponent<Animator>();
            if (!parentAnimator)
            {
                parentAnimator = FullBodyAvatarReference?.transform.parent?.gameObject.AddComponent<Animator>();
            }


            RuntimeAnimatorController animatorControllerReference;

            if (avatarData.gender == AvatarGender.Feminine)
            {
                animatorControllerReference = feminineAnimatorController;
            }
            else
            {
                animatorControllerReference = masculineAnimatorController;
            }

            if (parentAnimator != null)
            {
                parentAnimator.Rebind();

                parentAnimator.runtimeAnimatorController = animatorControllerReference;

                parentAnimator.avatar = avatarData.animatorAvatar;
            }


            OnAvatarIstantiated?.Invoke(avatar);

            if (GetComponent<AvatarControllerBase>() == avatarSystem.AvatarInstance)
            {
                SM.GetSystem<CharacterControllerSystem>().OnCharacterControllerSetupComplete.Invoke(GetComponent<AvatarControllerBase>().CharacterReference);
            }

            CharacterBase character = GetComponent<CharacterBase>();

            character.PlayerHeight = CalculateCharacterHeight();

            character.Setup();

            if (prevRef != avatar)
            {
                Destroy(prevRef);
            }
        }

        public override void EnableAvatarMeshes(bool enable)
        {
            // This call may be done after run closure. This line prevents the error.
            if (!Application.isPlaying)
                return;
            if (FullBodyAvatarReference != null)
            {
                foreach (Renderer rend in FullBodyAvatarReference?.GetComponentsInChildren<Renderer>())
                {
                    //rend.enabled = enable;
                    rend.gameObject.layer = enable ? LayerMask.NameToLayer("AvatarWelcomeRoom") : LayerMask.NameToLayer("HiddenToPlayer");
                }
            }

            base.EnableAvatarMeshes(enable);

        }

    }
}
