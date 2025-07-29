using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Reflectis.SDK.Http
{
    [Serializable, Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class ApiServerStatus
    {
        [SerializeField] private string startTimestamp;
        [SerializeField] private string environment;
        [SerializeField] private string apiServerBuild;
        [SerializeField] private List<string> versionInfo;
        [SerializeField] private ApiServerConfiguration apiConfiguration;

        public DateTime StartTimestamp => DateTime.Parse(startTimestamp.Replace("T", " "));
        public string Environment => environment;
        public string ApiServerBuild => apiServerBuild;
        public IEnumerable<string> VersionInfo => versionInfo.ToList();
        public ApiServerConfiguration ApiConfiguration => apiConfiguration;
    }

    [Serializable, Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class ApiServerConfiguration
    {
        public string label;

        public string Label => label;
    }
}