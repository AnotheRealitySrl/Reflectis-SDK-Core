using SPACS.Core;
using SPACS.SDK.CharacterController;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SPACS.SDK.Avatars
{
    public class VRAvatarController : ParentConstraintAvatarController
    {
        [Tooltip("List of names of all the objects that have to be hidden from the player camera")]
        [SerializeField]
        private List<string> hideToPlayer = new List<string>
        {
            "Renderer_EyeLeft",
            "Renderer_EyeRight",
            "Renderer_Head",
            "Renderer_Teeth",
            "Renderer_low",
            "Renderer_Hair",
            "Renderer_Glasses",
            "Renderer_Beard",
            "Renderer_Headwear",
        };

        public override async Task Setup(CharacterControllerBase sourceController)
        {
            await base.Setup(sourceController);

            HideAvatarHeadToPlayer(gameObject);

            GetComponent<AvatarConfigControllerBase>().OnAvatarIstantiated.AddListener(HideAvatarHeadToPlayer);
        }

        #region Private Methods
        private void HideAvatarHeadToPlayer(GameObject avatarInstance)
        {
            Debug.Log("Hide head");
            string layerHiddenToPlayer = SM.GetSystem<AvatarSystem>().LayerNameHiddenToPlayer;

            foreach (Transform transform in avatarInstance.GetComponentsInChildren<Transform>())
            {
                if (hideToPlayer.Contains(transform.gameObject.name))
                {
                    transform.gameObject.layer = LayerMask.NameToLayer(layerHiddenToPlayer);
                }
            }
        }

        #endregion
    }
}
