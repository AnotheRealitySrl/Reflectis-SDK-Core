using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Reflectis.SDK.Utilities
{
    public static class ImageDownloader
    {
        public static Dictionary<string, Texture2D> userIconCached = new Dictionary<string, Texture2D>();

        private static Worker worker;

        public static void DownloadImage(string mediaUrl, Action<Texture2D> onCompletionCallback, Action onFailedCallback = null, string key = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = mediaUrl;
            }
            if (worker == null)
            {
                GameObject go = new GameObject("ImageDownloaderWorker");
                worker = go.AddComponent<Worker>();
            }

            if (userIconCached.TryGetValue(key, out Texture2D texture))
            {
                if (texture != null)
                {
                    onCompletionCallback(texture);
                }
                else
                {
                    worker.StartCoroutine(WaitForTextureDownload(mediaUrl, onCompletionCallback, onFailedCallback, key));
                }
            }
            else
            {
                worker.StartCoroutine(DownloadImageCoroutine(mediaUrl, onCompletionCallback, onFailedCallback, key));
            }
        }

        public static IEnumerator DownloadImageCoroutine(string mediaUrl, Action<Texture2D> onCompletionCallback, Action onFailedCallback, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = mediaUrl;
            }

            userIconCached.Add(key, null);
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
                userIconCached.Remove(mediaUrl);
                onFailedCallback();
            }
            else
            {
                onCompletionCallback(((DownloadHandlerTexture)request.downloadHandler).texture);
                userIconCached[key] = ((DownloadHandlerTexture)request.downloadHandler).texture;
            }
        }

        public static IEnumerator WaitForTextureDownload(string mediaUrl, Action<Texture2D> onCompletionCallback, Action onFailedCallback, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = mediaUrl;
            }
            Texture2D texture = null;
            yield return new WaitUntil(() => !userIconCached.ContainsKey(key) || (userIconCached.TryGetValue(key, out texture) && texture != null));
            if (!userIconCached.ContainsKey(key))
            {
                if (onFailedCallback != null)
                {
                    onFailedCallback();
                }
            }
            else
            {
                onCompletionCallback(texture);
            }
        }
    }
}
