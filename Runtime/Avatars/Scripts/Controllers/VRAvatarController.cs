using Reflectis.SDK.Core.CharacterController;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Core.Avatars
{
    public class VRAvatarController : ParentConstraintAvatarController
    {
        //[Tooltip("List of names of all the objects that have to be hidden from the player camera")]
        //[Serializefield]

        #region Constants
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
        #endregion

        #region Inspector fields
        [SerializeField] private bool onlyHandsOnFullAvatar;
        #endregion

        #region Private variables
        private AvatarConfigControllerVR avatarConfigControllerVR;

        #endregion

        #region Public Methods
        public override async Task Setup(CharacterControllerBase sourceController)
        {
            await base.Setup(sourceController);

            AvatarSystem avatarSystem = SM.GetSystem<AvatarSystem>();

            if (avatarSystem.AvatarInstance == this)
            {
                SetupAvatar(gameObject);

                avatarConfigControllerVR.OnAvatarIstantiated.AddListener(SetupAvatar);
            }
        }
        #endregion 

        #region Private Methods
        private void SetupAvatar(GameObject avatarInstance)
        {
            avatarConfigControllerVR = GetComponent<AvatarConfigControllerVR>();

            HideHeadToPlayer();

            if (onlyHandsOnFullAvatar)
            {
                avatarConfigControllerVR.EnableFullBodyAvatar(false);
                avatarConfigControllerVR.EnableHandMeshes(true);
            }
        }

        private void HideHeadToPlayer()
        {
            int layerHiddenToPlayer = LayerMask.NameToLayer(SM.GetSystem<AvatarSystem>().LayerNameHiddenToPlayer);

            foreach (Transform transform in avatarConfigControllerVR.FullBodyAvatarReference.GetComponentsInChildren<Transform>())
            {
                if (hideToPlayer.Contains(transform.gameObject.name))
                {
                    transform.gameObject.layer = layerHiddenToPlayer;
                }
            }

            foreach (Transform transform in avatarConfigControllerVR.HalfBodyAvatarReference.GetComponentsInChildren<Transform>())
            {
                if (hideToPlayer.Contains(transform.gameObject.name))
                {
                    transform.gameObject.layer = layerHiddenToPlayer;
                }
            }

            if (CharacterReference.LabelReference)
            {
                CharacterReference.LabelReference.gameObject.layer = layerHiddenToPlayer;
            }
        }

        #endregion
    }
}
