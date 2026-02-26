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
        public bool InvertX = false;
        public bool InvertY = false;
        public bool LeftButton = true;
        public bool RightButton = false;
        public bool ThirdPerson = true;

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

        public InputSettings(bool enableWASDInteraction, bool enableArrowInteraction, bool enableNavmeshMovement, bool enableJoystickInteraction, bool enableMouseDraggingInteraction, bool enableInputCameraInteraction, bool invertX, bool invertY, bool leftButton, bool rightButton, bool thirdPerson, bool enableMouseWheelZoom, bool enablePinchZoom, bool enableJumpWithKey, bool enableJumpWithButton, bool enableSittingInteraction, bool enableWalkInteraction)
        {
            EnableWASDInteraction = enableWASDInteraction;
            EnableArrowInteraction = enableArrowInteraction;
            EnableNavmeshMovement = enableNavmeshMovement;
            EnableJoystickInteraction = enableJoystickInteraction;
            EnableMouseDraggingInteraction = enableMouseDraggingInteraction;
            EnableInputCameraInteraction = enableInputCameraInteraction;
            InvertX = invertX;
            InvertY = invertY;
            LeftButton = leftButton;
            RightButton = rightButton;
            ThirdPerson = thirdPerson;
            EnableMouseWheelZoom = enableMouseWheelZoom;
            EnablePinchZoom = enablePinchZoom;
            EnableJumpWithKey = enableJumpWithKey;
            EnableJumpWithButton = enableJumpWithButton;
            EnableSittingInteraction = enableSittingInteraction;
            EnableWalkInteraction = enableWalkInteraction;
        }
    }
}
