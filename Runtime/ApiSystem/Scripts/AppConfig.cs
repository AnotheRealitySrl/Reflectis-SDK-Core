using Reflectis.SDK.Core.Utilities;

using System;

using Unity.Properties;

using UnityEngine;

namespace Reflectis.SDK.Core.ApiSystem
{
    [Serializable, Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class AppConfig
    {
        [SerializeField] private HmacCredential credential;
        [SerializeField] private string apiBaseUrl;
        [SerializeField] private string apiVersion;

        [CreateProperty] public HmacCredential Credential => credential;
        [CreateProperty] public string ApiBaseUrl => apiBaseUrl;
        [CreateProperty] public string ApiVersion => apiVersion ??= "1";

        public AppConfig(HmacCredential credential, string apiBaseUrl, string apiVersion = "1")
        {
            this.credential = credential;
            this.apiBaseUrl = apiBaseUrl;
            this.apiVersion = apiVersion;
        }

        public AppConfig()
        {
            // Default constructor for serialization
        }
    }

}

