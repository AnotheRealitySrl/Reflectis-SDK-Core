using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.CharacterController
{
    public interface ICharacterControllerSystem : ISystem
    {
        #region Properties

        /// <summary>
        /// Charactrer controller instance
        /// </summary>
        public CharacterControllerBase CharacterControllerInstance { get; }

        #endregion

        #region Unity Events

        /// <summary>
        /// Invoked when the character controller setup is completed
        /// </summary>
        public UnityEvent<CharacterBase> OnCharacterControllerSetupComplete { get; }

        #endregion

        #region Character controller lifecycle

        /// <summary>
        /// Creates the character controller instance. 
        /// A character controller can be referenced as a prefab through the serialized field `characterControllerPrefab`, or can be already in scene
        /// </summary>
        void CreateCharacterControllerInstance(CharacterControllerBase characterController);

        /// <summary>
        /// Destroys the character controller instance
        /// </summary>
        void DestroyCharacterControllerInstance();

        #endregion

        #region Public API

        /// <summary>
        /// Moves the character controller to a destination position and rotation
        /// </summary>
        /// <param name="newPose">The destination pose of the character</param>
        void MoveCharacter(Pose newPose);

        /// <summary>
        /// Sets the character controller to "reaction" state and activates one of the 
        /// reaction animations
        /// </summary>
        /// <param name="reactionName">Name of the reaction animation to activate</param>
        void ActivateReactionAnimation(string reactionName);

        /// <summary>
        /// Enable or disable based on value the movement of the character
        /// </summary>
        /// <param name="enable"></param>
        void EnableCharacterMovement(bool value);

        /// <summary>
        /// Enable or disable based on value the jump of the character
        /// </summary>
        /// <param name="value"></param>
        void EnableCharacterJump(bool value);

        /// <summary>
        /// Enable or disable the rotation of the player camera (Desktop only)
        /// </summary>
        /// <param name="value"></param>
        void EnableCameraRotation(bool value);

        /// <summary>
        /// Enable or disable the zoom of the player camera (Desktop only)
        /// </summary>
        /// <param name="value"></param>
        void EnableCameraZoom(bool value);

        /// <summary>
        /// Sends the character to intract state (Desktop only)
        /// </summary>
        /// <param name="interactingItem"></param>
        public Task GoToInteractState(Transform targetTransform);

        /// <summary>
        /// Sends the character to movement state (Desktop only)
        /// </summary>
        public Task GoToSetMovementState();

        #endregion
    }
}
