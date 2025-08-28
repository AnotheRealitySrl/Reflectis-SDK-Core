using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices; // Necessario per DllImport

using UnityEngine;

namespace Reflectis.SDK.Core.ApplicationManagement.Samples
{
    public class QueryStringParser : MonoBehaviour
    {
        // Importa la funzione GetQueryString dal nostro file .jslib
        [DllImport("__Internal")]
        private static extern string GetQueryString();

        private void Awake()
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

