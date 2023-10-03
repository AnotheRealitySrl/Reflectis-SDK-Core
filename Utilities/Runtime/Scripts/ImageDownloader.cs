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

        public static void DownloadImage(string mediaUrl, Action<Texture2D> onCompletionCallback, Action onFailedCallback = null)
        {
            if (worker == null)
            {
                GameObject go = new GameObject("ImageDownloaderWorker");
                worker = go.AddComponent<Worker>();
            }

            if (userIconCached.TryGetValue(mediaUrl, out Texture2D texture))
            {
                if (texture == null)
                {
                    onCompletionCallback(texture);
                }
                else
                {
                    worker.StartCoroutine(WaitForTextureDownload(mediaUrl, onCompletionCallback, onFailedCallback));
                }
            }
            else
            {
                worker.StartCoroutine(DownloadImageCoroutine(mediaUrl, onCompletionCallback, onFailedCallback));
            }
        }

        public static IEnumerator DownloadImageCoroutine(string mediaUrl, Action<Texture2D> onCompletionCallback, Action onFailedCallback = null)
        {
            userIconCached.Add(mediaUrl, null);
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
                userIconCached[mediaUrl] = ((DownloadHandlerTexture)request.downloadHandler).texture;
            }
        }

        public static IEnumerator WaitForTextureDownload(string mediaUrl, Action<Texture2D> onCompletionCallback, Action onFailedCallback = null)
        {
            Texture2D texture = null;
            yield return new WaitUntil(() => !userIconCached.ContainsKey(mediaUrl) || (userIconCached.TryGetValue(mediaUrl, out texture) && texture != null));
            if (!userIconCached.ContainsKey(mediaUrl))
            {
                onFailedCallback();
            }
            else
            {
                onCompletionCallback(texture);
            }
        }
    }
}
