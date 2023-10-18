using UnityEngine;

namespace Reflectis.SDK.Utilities.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Puts the string into the Clipboard.
        /// </summary>
        public static void CopyToClipboard(this string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }
    }
}