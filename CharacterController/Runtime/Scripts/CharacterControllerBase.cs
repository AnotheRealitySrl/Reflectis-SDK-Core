using UnityEngine;

namespace SPACS.SDK.CharacterController
{
    public class CharacterControllerBase : CharacterBase, ICharacterController
    {
        #region Inspector variables

        [SerializeField] protected Camera cam;
        [SerializeField] protected ICharacterController.InteractionType interactorsType;

        #endregion

        #region Interface implementation

        public Camera Camera => cam;
        public ICharacterController.InteractionType InteractorsType => interactorsType;

        #endregion

    }
}