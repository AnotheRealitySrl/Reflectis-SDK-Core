using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.Core.Popup
{
    public interface IPopupSystem : ISystem
    {
        public enum PopupLocation
        {
            Floating,
            InsideInvoker
        }

        public enum EPopUpGravity
        {
            Replaceable = 1,
            MinorError = 5,
            MajorError = 10,
            BlockAccess = 15,
        }

        // Instantiate a popup directly with texts
        public void Instantiate(string message, string button1Text, UnityAction button1Callback, PopupLocation whereToDisplay, EPopUpGravity popUpGravity, Transform popupParent = null, string header = "", string button2Text = "", UnityAction button2Callback = null);

        // Use this if insted of a text you need an enum-based data structure to access the texts
        public void Instantiate(int popupMessageId, UnityAction button1Callback, PopupLocation whereToDisplay, EPopUpGravity popUpGravity, Transform popupParent = null, UnityAction button2Callback = null);
    }
}
