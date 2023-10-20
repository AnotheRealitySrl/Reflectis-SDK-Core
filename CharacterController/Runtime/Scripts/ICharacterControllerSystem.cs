using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.CharacterController
{
    public interface ICharacterControllerSystem
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

        #region Utilities

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
        /// Function to manage a counter of the character interaction
        /// </summary>
        /// <param name="activate"></param>
        int ManageCounterCharacterInteraction(bool activate);
        #endregion
    }
}
