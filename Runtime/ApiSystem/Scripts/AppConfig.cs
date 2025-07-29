using System;

using Unity.Properties;

using UnityEngine;

namespace Reflectis.SDK.Core.ApiSystem
{
    [Serializable, Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class AppConfig
    {
        [SerializeField] private string appId;
        [SerializeField] private string appSecret;
        [SerializeField] private string apiBaseUrl;
        [SerializeField] private string apiVersion;

        [CreateProperty] public string AppId => appId;
        [CreateProperty] public string AppSecret => appSecret;
        [CreateProperty] public string ApiBaseUrl => apiBaseUrl;
        [CreateProperty] public string ApiVersion => apiVersion ??= "1";

        public AppConfig(string appId, string appSecret, string apiBaseUrl, string apiVersion = "1")
        {
            this.appId = appId;
            this.appSecret = appSecret;
            this.apiBaseUrl = apiBaseUrl;
            this.apiVersion = apiVersion;
        }

        public AppConfig()
        {
            // Default constructor for serialization
        }
    }

}

