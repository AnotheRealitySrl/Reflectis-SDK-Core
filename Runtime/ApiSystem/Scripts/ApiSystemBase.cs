using Newtonsoft.Json;

using Reflectis.SDK.Core.Authentication;
using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Core.Utilities;
using Reflectis.SDK.Http;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Unity.VisualScripting;

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
                if (!await IsAlive())
                {
                    throw new Exception($"{name}: API is not alive");
                }
            }

            if (getApiInfo)
            {
                ApiResponse<ApiInfo> apiInfoReq = await GetApiInfo();
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
            string appId = credential.AppId.ToString().ToUpperInvariant();
            string timestampFormatted = timestamp.ToString("yyyy-MM-ddTHH:mm:ss'z'", CultureInfo.InvariantCulture).ToUpperInvariant();
            byte[] keyBytes = Encoding.UTF8.GetBytes(credential.AppSecret);
            using HMACSHA256 sha256 = new(keyBytes);
            byte[] hmac = sha256.ComputeHash(Encoding.UTF8.GetBytes($"{appId}:{timestampFormatted}"));

            return (timestampFormatted, Convert.ToBase64String(hmac));
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
            queryParams ??= new Dictionary<string, string>();
            // Filter null or empty query parameters
            // Assuming string.IsNullOrWhiteSpace is used or an extension method is properly imported
            queryParams = queryParams.Where(x => allowEmptyQueryValues ? x.Value != null : !string.IsNullOrWhiteSpace(x.Value))
                                     .ToDictionary(x => x.Key, x => x.Value);
            if (!string.IsNullOrEmpty(apiConfig.ApiVersion))
            {
                queryParams.Add("api-version", apiConfig.ApiVersion);
            }

            (string timestamp, string hmac) = CalculateHmacHeader(apiConfig.Credential, DateTime.UtcNow - serverTimeOffset);

            Dictionary<string, string> headers = SetDefaultHeaders(timestamp);
            if (additionalHeaders != null)
            {
                headers.AddRange(additionalHeaders);
            }

            // Removed the Content-Type: application/json here, as it's now handled by CreateHttpRequest
            // or explicitly by the caller via headers.
            if (authentication.HasFlag(EAuthentication.Bearer))
            {
                await ValidateJwtToken();
                headers.Add("Authorization", $"Bearer {JwtToken.Bearer}");
            }

            if (authentication.HasFlag(EAuthentication.Hmac))
            {
                headers.Add("Hmac", hmac);
            }

            CertificateHandler certificateHandler = allowUntrustedServers ? new AcceptAllCertificates() : default;

            // --- Use the new CreateHttpRequest ---
            UnityWebRequest request = httpSystem.CreateHttpRequest(
                method,
                $"{apiConfig.ApiBaseUrl}/{endpoint}",
                requestBodyType, // Pass the new enum
                body,            // Pass the object body directly
                queryParams,
                headers,
                certificateHandler);

            // No need for separate header application loop here, as CreateHttpRequest handles it
            // and its internal logic decides if Content-Type should be set/overridden.

            return request;
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
            using UnityWebRequest request = await BuildRequest(UnityWebRequest.kHttpVerbGET, "health", authentication: EAuthentication.None);
            await request.SendWebRequest();

            return request.result == UnityWebRequest.Result.Success;
        }


        public async Task<ApiResponse<ApiInfo>> GetApiInfo()
        {
            using UnityWebRequest request = await BuildRequest(UnityWebRequest.kHttpVerbGET, "apiserver/info", authentication: EAuthentication.None);
            await request.SendWebRequest();

            return new ApiResponse<ApiInfo>(request.responseCode, request.error, request.downloadHandler.text);
        }
    }
}
