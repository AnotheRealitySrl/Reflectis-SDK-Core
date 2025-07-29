using System;

namespace Reflectis.SDK.Core.Utilities
{
    [Serializable, Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class HmacCredential
    {
        private string id;
        private string secret;

        public Guid Id { get => new(id); set => id = value.ToString(); }
        public string Secret { get => secret; set => secret = value; }
    }
}