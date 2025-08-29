using System.Collections.Generic;
using System.Net;
#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

using UnityEngine;

namespace Reflectis.SDK.Core.ApplicationManagement.Samples
{
    public class QueryStringParser : MonoBehaviour
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string GetQueryString();
#endif

        public void ParseQueryString()
        {
            Dictionary<string, string> queryParams = new();
            string queryString = "";

#if UNITY_WEBGL && !UNITY_EDITOR
            // Chiama la funzione JS per ottenere la querystring
            queryString = GetQueryString();
#endif

            // Rimuovi il '?' iniziale, se esiste
            if (!string.IsNullOrEmpty(queryString) && queryString.StartsWith("?"))
            {
                queryString = queryString.Substring(1);
            }

            if (!string.IsNullOrEmpty(queryString))
            {
                // La logica di parsing rimane la stessa
                string[] parameters = queryString.Split('&');
                foreach (string param in parameters)
                {
                    string[] pair = param.Split('=');
                    if (pair.Length == 2)
                    {
                        string key = pair[0];
                        string value = WebUtility.UrlDecode(pair[1]);
                        queryParams[key] = value;
                    }
                }
            }

            IDeepLinkPayloadParser.Instance.ParseDeepLinkPayload(queryParams);
        }
    }
}

