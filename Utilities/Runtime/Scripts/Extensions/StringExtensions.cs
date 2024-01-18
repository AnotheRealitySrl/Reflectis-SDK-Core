using System.Text;
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

        /// <summary>
        /// Remove all special characters from a string leaving only letters and numbers and the characters inside the parameter includeCharacters
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string str, string includeCharacters = "")
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || includeCharacters.Contains(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}