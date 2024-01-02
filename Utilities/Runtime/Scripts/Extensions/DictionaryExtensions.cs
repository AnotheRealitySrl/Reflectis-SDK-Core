using System.Collections.Generic;
using System.Linq;

namespace Reflectis.SDK.Utilities
{
    public static class DictionaryExtensions
    {
        public static string CustomToString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
        }
    }
}
