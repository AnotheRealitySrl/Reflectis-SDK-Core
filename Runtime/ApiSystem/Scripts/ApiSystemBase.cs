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
        [SerializeField] protected AppConfig appConfig;

        [SerializeField] private bool checkIsAlive;
        [SerializeField] private bool getApiStatus;

        [Header("Untrusted servers")]
        [SerializeField] private bool allowUntrustedServers;

        [SerializeField] protected HttpSystem httpSystem;


        protected HmacCredential credential;
        protected JwtToken jwtToken;

        protected TimeSpan serverTimeOffset;

        public string ApiLabel { get; private set; }

        public override async Task Init()
        {
            httpSystem = httpSystem != null ? httpSystem : SM.GetSystem<HttpSystem>();

            if (string.IsNullOrEmpty(appConfig.AppId))
            {
                throw new ArgumentException($"{this}: Missing appId", nameof(AppConfig.AppId));
            }

            if (string.IsNullOrEmpty(appConfig.AppSecret))
            {
                throw new ArgumentException($"{this}: Missing appSecret", nameof(AppConfig.AppSecret));
            }

            if (string.IsNullOrEmpty(appConfig.ApiBaseUrl))
            {
                throw new ArgumentNullException("Missing apiBaseUrl", nameof(AppConfig.ApiBaseUrl));
            }

            credential = new HmacCredential()
            {
                Id = new Guid(appConfig.AppId),
                Secret = appConfig.AppSecret
            };


            if (checkIsAlive)
            {
                bool isAliveReq = await IsAlive();
                if (isAliveReq)
                {
                    Debug.LogError($"API is not alive");
                }
            }

            if (getApiStatus)
            {
                ApiResponse<ApiServerStatus> apiServerStatusReq = await GetApiServerStatus();
                if (apiServerStatusReq.IsSuccess)
                {
                    ApiServerStatus apiServerStatus = apiServerStatusReq.Content;
                    Debug.Log($"API Server Status: {JsonConvert.SerializeObject(apiServerStatus)}");
                    ApiLabel = apiServerStatus.ApiConfiguration.label;
                }
                else
                {
                    Debug.LogError($"Failed to get API server status: {apiServerStatusReq.StatusCode} {apiServerStatusReq.ReasonPhrase}");
                }
            }
        }

        public async Task Init(AppConfig config)
        {
            appConfig = config ?? throw new ArgumentException($"{this}: Missing AppConfig", nameof(AppConfig));

            await Init();
        }

        protected (string, string) CalculateHmacHeader(HmacCredential credential, DateTime timestamp)
        {
            string appId = credential.Id.ToString().ToUpperInvariant();
            string timestampFormatted = timestamp.ToString("yyyy-MM-ddTHH:mm:ss'z'", CultureInfo.InvariantCulture).ToUpperInvariant();
            byte[] keyBytes = Encoding.UTF8.GetBytes(credential.Secret);
            using HMACSHA256 sha256 = new(keyBytes);
            byte[] hmac = sha256.ComputeHash(Encoding.UTF8.GetBytes($"{appId}:{timestampFormatted}"));

            return (timestampFormatted, Convert.ToBase64String(hmac));
        }

        protected virtual async Task<UnityWebRequest> BuildRequest(
                                                string method,
                                                string endpoint,
                                                Dictionary<string, string> queryParams = null,
                                                HttpSystem.ERequestBodyType requestBodyType = HttpSystem.ERequestBodyType.RawString,
                                                object body = null, // Changed to object);
                                                EAuthentication authentication = EAuthentication.BearerAndHmac,
                                                bool allowEmptyQueryValues = false,
                                                Dictionary<string, string> additionalHeaders = null)
        {
            queryParams ??= new Dictionary<string, string>();
            // Filter null or empty query parameters
            // Assuming string.IsNullOrWhiteSpace is used or an extension method is properly imported
            queryParams = queryParams.Where(x => allowEmptyQueryValues ? x.Value != null : !string.IsNullOrWhiteSpace(x.Value))
                                     .ToDictionary(x => x.Key, x => x.Value);
            queryParams.Add("api-version", appConfig.ApiVersion);

            (string timestamp, string hmac) = CalculateHmacHeader(credential, DateTime.UtcNow - serverTimeOffset);
            Dictionary<string, string> headers = new()
            {
                { "AppId", appConfig.AppId },
                { "Timestamp", timestamp }
            };

            if (additionalHeaders != null)
            {
                headers.AddRange(additionalHeaders);
            }

            // Removed the Content-Type: application/json here, as it's now handled by CreateHttpRequest
            // or explicitly by the caller via headers.
            if (authentication.HasFlag(EAuthentication.Bearer))
            {
                if (jwtToken == null || jwtToken.IsExpired(serverTimeOffset))
                {
                    jwtToken = await SM.GetSystem<IAuthenticationSystem>().RefreshToken(ApiLabel);
                }

                headers.Add("Authorization", $"Bearer {jwtToken.Bearer}");
            }

            if (authentication.HasFlag(EAuthentication.Hmac))
            {
                headers.Add("Hmac", hmac);
            }

            CertificateHandler certificateHandler = allowUntrustedServers ? new AcceptAllCertificates() : default;

            // --- Use the new CreateHttpRequest ---
            UnityWebRequest request = httpSystem.CreateHttpRequest(
                method,
                $"{appConfig.ApiBaseUrl}{endpoint}",
                requestBodyType, // Pass the new enum
                body,            // Pass the object body directly
                queryParams,
                headers,
                certificateHandler);

            // No need for separate header application loop here, as CreateHttpRequest handles it
            // and its internal logic decides if Content-Type should be set/overridden.

            return request;
        }

        public async Task<bool> IsAlive()
        {
            using UnityWebRequest request = await BuildRequest(UnityWebRequest.kHttpVerbGET, "health", authentication: EAuthentication.None);
            await request.SendWebRequest();

            bool success = request.result == UnityWebRequest.Result.Success;

            if (success)
            {
                ApiResponse<DateTime?> serverTimeResponse = new(request.responseCode, request.error, request.downloadHandler.text);
                DateTime? serverTime = serverTimeResponse.Content;

                if (serverTime.HasValue)
                {
                    serverTimeOffset = DateTime.UtcNow - serverTime.Value;
                    Debug.Log($"Server time: {serverTime.Value}, client time offset: {serverTimeOffset}");
                }
                else
                {
                    Debug.LogWarning($"Unable to retrieve server time");
                }
            }

            return success;
        }

        public async Task<ApiResponse<ApiServerStatus>> GetApiServerStatus()
        {
            using UnityWebRequest request = await BuildRequest(UnityWebRequest.kHttpVerbGET, "apiserver/status", authentication: EAuthentication.Hmac);
            await request.SendWebRequest();

            return new ApiResponse<ApiServerStatus>(request.responseCode, request.error, request.downloadHandler.text);
        }
    }
}
