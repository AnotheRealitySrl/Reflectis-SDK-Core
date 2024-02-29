using System.Text.RegularExpressions;
using UnityEngine;

namespace Reflectis.SDK.Utilities
{
    public static class TextAssetExtensions
    {
        public static string GetTextClassCompleteName(this TextAsset textAsset)
        {
            string[] firstSplit = textAsset.text?.Split("namespace");
            if (firstSplit.Length > 1)
            {
                string _namespace = Regex.Replace(firstSplit[1].Split('{')[0], @"\s+", "");
                return $"{_namespace}.{textAsset.name}";
            }
            else
            {
                return textAsset.name;
            }
        }
    }
}
