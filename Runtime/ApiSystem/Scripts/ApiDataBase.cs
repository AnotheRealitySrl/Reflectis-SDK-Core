using Reflectis.SDK.Core.Utilities;

using System;

using UnityEngine;

namespace Reflectis.SDK.Core.ApiSystem
{
    /// <summary>
    /// Base ScriptableObject for all API data companions.
    /// Stores configuration and runtime state for static API classes.
    /// </summary>
    public abstract class ApiDataBase : ScriptableObject
    {
        [SerializeField] protected AppIdentification apiConfig;

        [Header("Init settings")]
        [SerializeField] private bool checkIsAlive = true;
        [SerializeField] private bool getApiInfo = true;

        [Header("Untrusted servers")]
        [SerializeField] private bool allowUntrustedServers;

        // Runtime state (not serialized, populated by the static API class)
        [NonSerialized] private JwtToken jwtToken;
        [NonSerialized] private string apiLabel;
        [NonSerialized] private TimeSpan serverTimeOffset;

        public AppIdentification ApiConfig { get => apiConfig; set => apiConfig = value; }
        public bool CheckIsAlive => checkIsAlive;
        public bool GetApiInfo => getApiInfo;
        public bool AllowUntrustedServers => allowUntrustedServers;

        public JwtToken JwtToken { get => jwtToken; set => jwtToken = value; }
        public string ApiLabel { get => apiLabel; set => apiLabel = value; }
        public TimeSpan ServerTimeOffset { get => serverTimeOffset; set => serverTimeOffset = value; }
    }
}
