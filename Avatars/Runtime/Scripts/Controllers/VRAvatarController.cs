using SPACS.Core;
using SPACS.SDK.CharacterController;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UnityEngine;

namespace SPACS.SDK.Avatars
{
    public class VRAvatarController : ParentConstraintAvatarController
    {
        //[Tooltip("List of names of all the objects that have to be hidden from the player camera")]
        //[Serializefield]
        private static readonly IList<string> hideToPlayer = new ReadOnlyCollection<string>
                (new List<string> {
                    //fullbodyRPM
                    "Renderer_EyeLeft",
                    "Renderer_EyeRight",
                    "Renderer_Head",
                    "Renderer_Teeth",
                    "Renderer_low",
                    "Renderer_Hair",
                    "Renderer_Glasses",
                    "Renderer_Beard",
                    "Renderer_Headwear",
                    //halfBodyRPM
                    "EyeLeft",
                    "EyeRight",
                    "Wolf3D_Facewear",
                    "Wolf3D_Hair",
                    "Wolf3D_Head",
                    "Wolf3D_Shirt",
                    "Wolf3D_Teeth",
                    "Wolf3D_Beard",
                    "Wolf3D_Glasses"
                }
                );

        public override async Task Setup(CharacterControllerBase sourceController)
        {
            await base.Setup(sourceController);

            HideAvatarHeadToPlayer(gameObject);

            GetComponent<AvatarConfigControllerBase>().OnAvatarIstantiated.AddListener(HideAvatarHeadToPlayer);
        }

        #region Private Methods
        private void HideAvatarHeadToPlayer(GameObject avatarInstance)
        {

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
