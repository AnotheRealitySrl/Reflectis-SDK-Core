using System;

using Unity.Properties;

using UnityEngine;

namespace Reflectis.SDK.Core.Utilities
{
    [Serializable, Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class HmacCredential
    {
        [SerializeField] private string appId;
        [SerializeField] private string appSecret;

        [CreateProperty] public Guid AppId { get => new(appId); set => appId = value.ToString(); }
        [CreateProperty] public string AppSecret { get => appSecret; set => appSecret = value; }
    }
}