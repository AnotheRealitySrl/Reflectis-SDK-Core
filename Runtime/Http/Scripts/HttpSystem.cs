using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Http;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Reflectis/SDK-Http/HttpSystemConfig", fileName = "HttpSystemConfig")]
public class HttpSystem : BaseSystem
{
    [SerializeField] private bool secureConnection = true;

    // Backward compatibility enum - mirrors HttpHelper.ERequestBodyType
    public enum ERequestBodyType
    {
        None = 0,
        RawString = 1,
        RawBytes = 2,
        MultipartFormData = 3
    }

    public static readonly string GET = UnityWebRequest.kHttpVerbGET;
    public static readonly string POST = UnityWebRequest.kHttpVerbPOST;
    public static readonly string PUT = UnityWebRequest.kHttpVerbPUT;
    public static readonly string DELETE = UnityWebRequest.kHttpVerbDELETE;
    public static readonly string CREATE = UnityWebRequest.kHttpVerbCREATE;
    public static readonly string HEAD = UnityWebRequest.kHttpVerbHEAD;

    /// <summary>
    /// Creates a UnityWebRequest. Delegates to HttpHelper.
    /// </summary>
    public UnityWebRequest CreateHttpRequest(string method,
                                            string uri,
                                            ERequestBodyType requestBodyType = ERequestBodyType.None,
                                            object body = null,
                                            Dictionary<string, string> queryParams = null,
                                            Dictionary<string, string> headers = null,
                                            CertificateHandler certificateHandler = default)
    {
        return HttpHelper.CreateHttpRequest(
            method, uri,
            (HttpHelper.ERequestBodyType)requestBodyType,
            body, queryParams, headers, certificateHandler, secureConnection);
    }

    public string CreateQueryString(Dictionary<string, string> queryParams)
    {
        return HttpHelper.CreateQueryString(queryParams);
    }
}
