using UnityEngine;

namespace Reflectis.SDK.Core
{
    public class InputSettings : MonoBehaviour
    {
        //TODO FARE TUTTO IN EDITOR SCRIPTING FATTO BENE!!!!
        [Header("Movement")]
        public bool EnableWASDInteraction, EnableArrowInteraction, EnableNavmeshMovement, EnableJoystickInteraction;

        [Header("Camera")]
        public bool EnableMouseDraggingInteraction, EnableInputCameraInteraction;
        public bool InvertX = false;
        public bool InvertY = false;
        public bool LeftButton = true;
        public bool RightButton = false;

        [Header("Zoom")]
        public bool EnableMouseWheelZoom, EnablePinchZoom;

        [Header("Jump")]
        public bool EnableJumpWithKey, EnableJumpWithButton;

        [Header("Sit")]
        public bool EnableSittingInteraction;

        [Header("Walk")]
        public bool EnableWalkInteraction;

        public InputSettings(bool enableWASDInteraction, bool enableArrowInteraction, bool enableNavmeshMovement, bool enableJoystickInteraction, bool enableMouseDraggingInteraction, bool enableInputCameraInteraction, bool invertX, bool invertY, bool leftButton, bool rightButton, bool enableMouseWheelZoom, bool enablePinchZoom, bool enableJumpWithKey, bool enableJumpWithButton, bool enableSittingInteraction, bool enableWalkInteraction)
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
            EnableMouseWheelZoom = enableMouseWheelZoom;
            EnablePinchZoom = enablePinchZoom;
            EnableJumpWithKey = enableJumpWithKey;
            EnableJumpWithButton = enableJumpWithButton;
            EnableSittingInteraction = enableSittingInteraction;
            EnableWalkInteraction = enableWalkInteraction;
        }
    }
}
