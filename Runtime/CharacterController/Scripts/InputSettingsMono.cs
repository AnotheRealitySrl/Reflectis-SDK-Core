using System;
using UnityEngine;

namespace Reflectis.SDK.Core
{
    public class InputSettingsMono : MonoBehaviour
    {
        public InputSettings settings;
    }

    [Serializable]
    public class InputSettings
    {        
        //TODO FARE TUTTO IN EDITOR SCRIPTING FATTO BENE!!!!
        [Header("Movement")]
        public bool EnableWASDInteraction;
        public bool EnableArrowInteraction;
        public bool EnableNavmeshMovement;
        public bool EnableJoystickInteraction;

        [Header("Camera")]
        public bool EnableMouseDraggingInteraction;
        public bool EnableInputCameraInteraction;
        public bool ThirdPerson = true;
        public bool ConstrainedRotation = false;

        [Header("Zoom")]
        public bool EnableMouseWheelZoom;
        public bool EnablePinchZoom;

        [Header("Jump")]
        public bool EnableJumpWithKey;
        public bool EnableJumpWithButton;

        [Header("Sit")]
        public bool EnableSittingInteraction;

        [Header("Walk")]
        public bool EnableWalkInteraction;

        public InputSettings(bool enableWASDInteraction, bool enableArrowInteraction, bool enableNavmeshMovement, bool enableJoystickInteraction, bool enableMouseDraggingInteraction, bool enableInputCameraInteraction, bool thirdPerson, bool enableMouseWheelZoom, bool enablePinchZoom, bool enableJumpWithKey, bool enableJumpWithButton, bool enableSittingInteraction, bool enableWalkInteraction)
        {
            EnableWASDInteraction = enableWASDInteraction;
            EnableArrowInteraction = enableArrowInteraction;
            EnableNavmeshMovement = enableNavmeshMovement;
            EnableJoystickInteraction = enableJoystickInteraction;
            EnableMouseDraggingInteraction = enableMouseDraggingInteraction;
            EnableInputCameraInteraction = enableInputCameraInteraction;
            ThirdPerson = thirdPerson;
            EnableMouseWheelZoom = enableMouseWheelZoom;
            EnablePinchZoom = enablePinchZoom;
            EnableJumpWithKey = enableJumpWithKey;
            EnableJumpWithButton = enableJumpWithButton;
            EnableSittingInteraction = enableSittingInteraction;
            EnableWalkInteraction = enableWalkInteraction;
        }

        public InputSettings(bool _mouseDragging, bool _cameraInteraction, bool _thirdPerson, bool _enablemouseWheel, bool _constrainedRotation)
        {
            EnableWASDInteraction = false;
            EnableArrowInteraction = false;
            EnableNavmeshMovement = false;
            EnableJoystickInteraction = false;

            EnableMouseDraggingInteraction = _mouseDragging;
            EnableInputCameraInteraction = _cameraInteraction;
            ThirdPerson = _thirdPerson;
            EnableMouseWheelZoom = _enablemouseWheel;
            ConstrainedRotation = _constrainedRotation;

            EnablePinchZoom = false;
            EnableJumpWithKey = false;
            EnableJumpWithButton = false;
            EnableSittingInteraction = false;
            EnableWalkInteraction = false;
        }
    }
}
