
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

        public override void OnAvatarLoadCompletion(GameObject avatar, AvatarData avatarData)
        {
            Transform currentPivotParent = null;
            GameObject prevRef = null;
            if (fullBodyAvatarReference != null)
            {
                currentPivotParent = fullBodyAvatarReference.transform.parent;
                prevRef = fullBodyAvatarReference;
            }
            fullBodyAvatarReference = avatar;
            fullBodyAvatarReference.transform.SetParent(currentPivotParent, false);
            fullBodyAvatarReference.transform.SetAsFirstSibling();
            fullBodyAvatarReference.name = "Avatar";
            fullBodyAvatarReference.layer = LayerMask.NameToLayer("AvatarWelcomeRoom");


            Animator parentAnimator = fullBodyAvatarReference.transform.parent.GetComponent<Animator>();
            if (!parentAnimator)
            {
                parentAnimator = fullBodyAvatarReference.transform.parent.gameObject.AddComponent<Animator>();
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

            parentAnimator.runtimeAnimatorController = animatorControllerReference;

            parentAnimator.avatar = avatarData.animatorAvatar;

            OnAvatarIstantiated?.Invoke(avatar);

            if (GetComponent<AvatarControllerBase>() == avatarSystem.AvatarInstance)
            {
                SM.GetSystem<CharacterControllerSystem>().OnCharacterControllerSetupComplete.Invoke(GetComponent<AvatarControllerBase>().CharacterReference);
                
            }

            GetComponent<CharacterBase>().Setup();

            if (prevRef != avatar)
            {
                Destroy(prevRef);
            }
            
            //onAfterAction?.Invoke();
        }

        public override void EnableAvatarMeshes(bool enable)
        {
            foreach (Renderer rend in fullBodyAvatarReference.GetComponentsInChildren<Renderer>())
            {
                rend.enabled = enable;
            }

            base.EnableAvatarMeshes(enable);

        }

    }
}
