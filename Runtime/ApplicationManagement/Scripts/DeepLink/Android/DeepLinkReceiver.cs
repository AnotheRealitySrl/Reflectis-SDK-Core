#if UNITY_ANDROID && !UNITY_EDITOR
using Reflectis.SDK.Core.ApplicationManagement;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using UnityEngine;

public class DeepLinkReceiver : MonoBehaviour
{
    public void CheckForDeepLink()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            AndroidJavaClass unityPlayer = new("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
            AndroidJavaObject uri = intent.Call<AndroidJavaObject>("getData");

            if (uri != null)
            {
                string deepLinkUrl = uri.Call<string>("toString");
                Debug.Log("App opened with deep linkg: " + deepLinkUrl);

                Dictionary<string, string> queryParams = ParseQueryString(deepLinkUrl);

                IDeepLinkPayloadParser.Instance.ParseDeepLinkPayload(queryParams);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Errore nel recuperare il deep link: " + ex.Message);
        }
#endif
    }

    /// <summary>
    /// Esegue il parsing della querystring da un URL usando le librerie standard .NET.
    /// È più robusto e leggibile rispetto all'iterazione tramite AndroidJavaObject.
    /// </summary>
    /// <param name="url">L'URL completo da cui estrarre i parametri.</param>
    /// <returns>Un dizionario con i parametri della querystring.</returns>
    private Dictionary<string, string> ParseQueryString(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return new Dictionary<string, string>();
        }

        try
        {
            // La classe Uri di .NET è perfetta per scomporre un URL
            var uri = new Uri(url);

            // HttpUtility.ParseQueryString gestisce automaticamente la decodifica
            // di caratteri speciali (es. %20 per lo spazio).
            var queryCollection = HttpUtility.ParseQueryString(uri.Query);

            // Converte il risultato (NameValueCollection) in un dizionario
            return queryCollection.AllKeys.ToDictionary(key => key, key => queryCollection[key]);
        }
        catch (Exception ex)
        {
            Debug.LogError("Impossibile parsare i parametri della querystring: " + ex.Message);
            return new Dictionary<string, string>();
        }
    }
}