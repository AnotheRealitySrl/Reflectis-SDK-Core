using Reflectis.SDK.Core.SystemFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.Core.CharacterController
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
        void EnableCharacterMovement(bool value, InputSettings settings = null, bool setAsDefaultSettings = false);

        /// <summary>
        /// Enable or disable based on value the jump of the character
        /// </summary>
        /// <param name="value"></param>
        void EnableCharacterJump(bool value, InputSettings settings = null, bool setAsDefaultSettings = false);

        /// <summary>
        /// Enable or disable the rotation of the player camera (Desktop only)
        /// </summary>
        /// <param name="value"></param>
        void EnableCameraRotation(bool value, InputSettings settings = null, bool setAsDefaultSettings = false);

        /// <summary>
        /// Enable or disable the zoom of the player camera (Desktop only)
        /// </summary>
        /// <param name="value"></param>
        void EnableCameraZoom(bool value, InputSettings settings = null, bool setAsDefaultSettings = false);

        /// <summary>
        /// Switches camera to first person view (Desktop only)
        /// </summary>
        void SetFirstPersonCameraMode();

        /// <summary>
        /// Switches camera to third person view (Desktop only)
        /// </summary>
        void SetThirdPersonCameraMode();

        /// <summary>
        /// Sends the character to intract state (Desktop only)
        /// </summary>
        /// <param name="interactingItem"></param>
        public Task GoToInteractState(Transform targetTransform, float maxZoom = 0.0001f, float maxYRotation = 45, float minYRotation = -45, bool cameraInteraction = false);

        /// <summary>
        /// Sends the camera to point (Desktop only)
        /// </summary>
        public Task MoveCameraToPoint(Transform targetTransform, float maxZoom = 0.0001f, float maxYRotation = 45, float minYRotation = -45, bool cameraInteraction = false);

        /// <summary>
        /// Sends the character to movement state (Desktop only)
        /// </summary>
        public Task GoToSetMovementState();

        /// <summary>
        /// Enables character gravity (Desktop only)
        /// </summary>
        public void EnableCharacterGravity(bool enable);

        /// <summary>
        /// Create default settings (Desktop only)
        /// </summary>
        void CreateDefaultSettings(InputSettings settings, bool setDeafultActive = true);

        /// <summary>
        /// Set default settings as active (Desktop only)
        /// </summary>
        void SetDefaultSettingsAsActive();

        /// <summary>
        /// Disable all settings and give new one only to camera (Desktop only)
        /// </summary>
        void DisableAllButCamera(InputSettings settings);

        public static Dictionary<ECameraModes, Type> CameraTypes =
         new Dictionary<ECameraModes, Type>
         {
             {
                 ECameraModes.DefaultCamera,
                 typeof(DefaultCameraType)
             },
             {
                 ECameraModes.StaticCamera,
                 typeof(StaticCameraType)
             },
             {
                 ECameraModes.RotationCamera,
                 typeof(RotationCameraType)
             },
             {
                 ECameraModes.CinemaCamera,
                 typeof(StaticCameraType)
             },
         };

        public InputSettings GetCurrentSettings();
        #endregion
    }
}
