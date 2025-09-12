
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.Core.Utilities
{
    public abstract class UrlParametersParserBase : MonoBehaviour
    {
        public abstract Dictionary<string, string> ParseUrlParameters();
    }
}
