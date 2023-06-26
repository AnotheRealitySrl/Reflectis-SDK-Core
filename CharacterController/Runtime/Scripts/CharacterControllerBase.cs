using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.CharacterController
{
    /// <summary>
    /// Base implementation of <see cref="ICharacterController"/>.
    /// In addition of the properties provided by <see cref="CharacterBase"/>, 
    /// it provides a reference to the main camera and the type of interaction used in VR (controllers, hands).
    /// </summary>
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