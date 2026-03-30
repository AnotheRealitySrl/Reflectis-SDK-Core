using Newtonsoft.Json;

using Reflectis.SDK.Core.Authentication;
using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Core.Utilities;
using Reflectis.SDK.Http;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

using static Reflectis.SDK.Core.Authentication.IAuthenticationSystem;

namespace Reflectis.SDK.Core.ApiSystem
{
    public abstract class ApiSystemBase : BaseSystem
    {
        [SerializeField] protected AppIdentification apiConfig;

        [SerializeField] private bool checkIsAlive = true;
        [SerializeField] private bool getApiInfo = true;

        [Header("Untrusted servers")]
        [SerializeField] private bool allowUntrustedServers;

        [SerializeField] protected HttpSystem httpSystem;


        protected TimeSpan serverTimeOffset;

        public JwtToken JwtToken { get; set; }
        public string ApiLabel { get; private set; }

        public override async Task Init()
        {
            httpSystem = httpSystem != null ? httpSystem : SM.GetSystem<HttpSystem>();

            if (string.IsNullOrEmpty(apiConfig.Credential.AppId.ToString()))
            {
                throw new Exception($"{name}: Missing {nameof(HmacCredential.AppId)}");
            }

            if (string.IsNullOrEmpty(apiConfig.Credential.AppSecret))
            {
                throw new Exception($"{name}: Missing {nameof(HmacCredential.AppSecret)}");
            }

            if (string.IsNullOrEmpty(apiConfig.ApiBaseUrl))
            {
                throw new Exception($"{name}: Missing {nameof(AppIdentification.ApiBaseUrl)}");
            }

            if (checkIsAlive)
            {
                if (!await ApiHelper.IsAlive(apiConfig, !allowUntrustedServers))
                {
                    throw new Exception($"{name}: API is not alive");
                }
            }

            if (getApiInfo)
            {
                ApiResponse<ApiInfo> apiInfoReq = await ApiHelper.GetApiInfo(apiConfig, !allowUntrustedServers);
                if (apiInfoReq.IsSuccess)
                {
                    ApiInfo apiInfo = apiInfoReq.Content;
                    Debug.Log($"{name}: API Server Info: {JsonConvert.SerializeObject(apiInfo)}");

                    ApiLabel = apiInfo.Label;
                    serverTimeOffset = DateTime.UtcNow - apiInfo.ServerTime;
                }
                else
                {
                    throw new Exception($"{name}: Failed to get API info: {apiInfoReq.StatusCode} {apiInfoReq.ReasonPhrase}");
                }
            }
        }

        public async Task Init(AppIdentification config)
        {
            apiConfig = config ?? throw new ArgumentException($"{this}: Missing AppConfig", nameof(AppIdentification));

            await Init();
        }

        protected (string, string) CalculateHmacHeader(HmacCredential credential, DateTime timestamp)
        {
            return ApiHelper.CalculateHmacHeader(credential, timestamp);
        }

        protected virtual async Task<UnityWebRequest> BuildRequest(
                                                string method,
                                                string endpoint,
                                                Dictionary<string, string> queryParams = null,
                                                HttpSystem.ERequestBodyType requestBodyType = HttpSystem.ERequestBodyType.RawString,
                                                object body = null,
                                                EAuthentication authentication = EAuthentication.BearerAndHmac,
                                                bool allowEmptyQueryValues = false,
                                                Dictionary<string, string> additionalHeaders = null)
        {
            if (authentication.HasFlag(EAuthentication.Bearer))
            {
                await ValidateJwtToken();
            }

            return ApiHelper.BuildRequest(
                method, endpoint, apiConfig,
                queryParams,
                (HttpHelper.ERequestBodyType)requestBodyType,
                body,
                authentication,
                allowEmptyQueryValues,
                additionalHeaders,
                jwtToken: JwtToken,
                serverTimeOffset: serverTimeOffset,
                allowUntrustedServers: allowUntrustedServers);
        }

        protected virtual Dictionary<string, string> SetDefaultHeaders(params string[] values)
        {
            Dictionary<string, string> headers = new()
            {
                { "AppId", apiConfig.Credential.AppId.ToString() },
                { "Timestamp", values[0] },
            };

            return headers;
        }

        protected virtual async Task ValidateJwtToken()
        {
            IAuthenticationSystem authenticationSystem = SM.GetSystem<IAuthenticationSystem>();

            if (JwtToken == null)
            {
                SetToken();
            }

            if (JwtToken.IsExpired(serverTimeOffset))
            {
                Debug.LogWarning($"[{name}]: JWT token is null or expired. Refreshing token for API label: {ApiLabel}");

                await authenticationSystem.GetTokens();

                SetToken();
            }

            void SetToken()
            {
                try
                {
                    JwtToken = authenticationSystem.FindToken(ApiLabel);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[{name}]: Failed to retrieve JWT token for API label: {ApiLabel}. Exception: {ex.Message}");
                    return;
                }
            }
        }

        public async Task<bool> IsAlive()
        {
            return await ApiHelper.IsAlive(apiConfig, !allowUntrustedServers);
        }

        public async Task<ApiResponse<ApiInfo>> GetApiInfo()
        {
            return await ApiHelper.GetApiInfo(apiConfig, !allowUntrustedServers);
        }
    }
}
