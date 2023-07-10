using UnityEngine;

namespace Reflectis.SDK.CharacterController
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
        [SerializeField] private bool isInRangeToInteract = false;
        [SerializeField] private Canvas canvasInteraction;

        #endregion

        #region Interface implementation

        public Camera Camera { get => cam; set => cam = value; }
        public ICharacterController.EInteractionType InteractorsType => interactorsType;
        public bool IsInRangeToInteract { get => isInRangeToInteract; set => isInRangeToInteract = value; }
        public Canvas CanvasInteraction => canvasInteraction;

        #endregion
    }
}