using Reflectis.SDK.Core.ApplicationManagement;

using System.Collections.Generic;

using UnityEngine;

public class DeepLinkReceiver : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            CheckForDeepLink();
#endif
    }

    private void CheckForDeepLink()
    {
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
            AndroidJavaObject uri = intent.Call<AndroidJavaObject>("getData");

            if (uri != null)
            {
                Debug.Log("App lanciata con Deep Link: " + uri.Call<string>("toString"));

                // Ottieni tutte le query string in un dizionario
                Dictionary<string, string> queryParams = GetAllQueryParameters(uri);

                IApplicationManager.Instance.OnDeepLinkParametersReceived.Invoke(queryParams);

            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Errore nel recuperare il deep link: " + ex.Message);
        }
    }

    private Dictionary<string, string> GetAllQueryParameters(AndroidJavaObject uri)
    {
        var result = new Dictionary<string, string>();
        if (uri == null)
            return result;

        try
        {
            // uri.getQueryParameterNames() -> Set<String>
            AndroidJavaObject namesSet = uri.Call<AndroidJavaObject>("getQueryParameterNames");
            if (namesSet == null) return result;

            AndroidJavaObject iterator = namesSet.Call<AndroidJavaObject>("iterator");
            while (iterator != null && iterator.Call<bool>("hasNext"))
            {
                string name = iterator.Call<string>("next");
                string value = uri.Call<string>("getQueryParameter", name);
                result[name] = value;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Impossibile leggere i parametri della query: " + e.Message);
        }

        return result;
    }
}