using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SPACS.SDK.CharacterController
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
        void CreateCharacterControllerInstance();

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

        #endregion
    }
}
