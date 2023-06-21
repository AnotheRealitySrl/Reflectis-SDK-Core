using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.CharacterController
{
    public class CharacterControllerBase : CharacterBase, ICharacterController
    {
        #region Inspector variables

        [SerializeField] protected Camera cam;
        [SerializeField] protected ICharacterController.EInteractionType interactorsType;

        #endregion

        #region Interface implementation

        public Camera Camera { get => cam; set => cam = value; }
        public ICharacterController.EInteractionType InteractorsType => interactorsType;

        #endregion
    }
}