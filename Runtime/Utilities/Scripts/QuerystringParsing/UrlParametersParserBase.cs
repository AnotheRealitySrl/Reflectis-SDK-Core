
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.Core.Utilities
{
    public abstract class UrlParametersParserBase : MonoBehaviour
    {
        public UnityEvent<Dictionary<string, string>> OnUrlParameterdParsed { get; }
        public abstract void ParseUrlParameters();
    }
}
