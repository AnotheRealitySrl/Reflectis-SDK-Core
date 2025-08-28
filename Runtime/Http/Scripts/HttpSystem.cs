using Reflectis.SDK.Core.SystemFramework;

using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Reflectis/SDK-Http/HttpSystemConfig", fileName = "HttpSystemConfig")]
public class HttpSystem : BaseSystem
{
    [SerializeField] private bool secureConnection = true; // Use HTTPS by default

    public enum ERequestBodyType
    {
        /// <summary>No body for the request (e.g., GET, HEAD, DELETE).</summary>
        None,
        /// <summary>Request body as a string (e.g., JSON, XML, text).</summary>
        RawString,
        /// <summary>Request body as a byte array.</summary>
        RawBytes,
        /// <summary>Request body in multipart/form-data format.</summary>
        MultipartFormData
    }

    public static readonly string GET = UnityWebRequest.kHttpVerbGET;
    public static readonly string POST = UnityWebRequest.kHttpVerbPOST;
    public static readonly string PUT = UnityWebRequest.kHttpVerbPUT;
    public static readonly string DELETE = UnityWebRequest.kHttpVerbDELETE;
    public static readonly string CREATE = UnityWebRequest.kHttpVerbCREATE;
    public static readonly string HEAD = UnityWebRequest.kHttpVerbHEAD;

    /// <summary>
    /// Creates a UnityWebRequest for performing an HTTP request.
    /// The type of the request body is determined by 'requestBodyType'.
    /// </summary>
    /// <param name="method">The HTTP method (GET, POST, PUT, etc.).</param>
    /// <param name="uri">The URI for the request.</param>
    /// <param name="requestBodyType">The type of body the request will contain.</param>
    /// <param name="body">The data for the request body. Its type must match 'requestBodyType'.</param>
    /// <param name="queryParams">Query parameters to append to the URI.</param>
    /// <param name="headers">Custom HTTP headers.</param>
    /// <param name="certificateHandler">Handler for SSL certificate validation.</param>
    /// <returns>The configured UnityWebRequest.</returns>
    public UnityWebRequest CreateHttpRequest(string method,
                                            string uri,
                                            ERequestBodyType requestBodyType = ERequestBodyType.None,
                                            object body = null,
                                            Dictionary<string, string> queryParams = null,
                                            Dictionary<string, string> headers = null,
                                            CertificateHandler certificateHandler = default)
    {
        if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri parsedUri) ||
            (parsedUri.Scheme != "http" && parsedUri.Scheme != "https"))
        {
            uri = $"http{(secureConnection ? "s" : string.Empty)}://{uri}";
        }

        if (queryParams != null)
        {
            uri += CreateQueryString(queryParams);
        }

        string inferredContentType = null; // To keep track of the deduced Content-Type
        body ??= string.Empty; // Default to empty string if no body is provided

        UnityWebRequest request;
        // --- Deducing and handling the body type based on the enum ---
        switch (requestBodyType)
        {
            case ERequestBodyType.None:
                request = new UnityWebRequest(uri, method);
                break;

            case ERequestBodyType.RawString:
                if (body is string stringBody)
                {
                    request = new UnityWebRequest(uri, method)
                    {
                        uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(stringBody))
                    };
                    inferredContentType = "application/json"; // Default, caller can override via headers
                }
                else
                {
                    Debug.LogError($"Body type for ERequestBodyType.RawString must be a string. Got {body.GetType()} type on request {uri}!");
                    return null;
                }
                break;

            case ERequestBodyType.RawBytes:
                if (body is byte[] byteBody)
                {
                    request = new UnityWebRequest(uri, method)
                    {
                        uploadHandler = new UploadHandlerRaw(byteBody)
                    };
                    inferredContentType = "application/octet-stream"; // Default, caller can override
                }
                else
                {
                    Debug.LogError("Body type for ERequestBodyType.RawBytes must be a byte array.");
                    return null;
                }
                break;

            case ERequestBodyType.MultipartFormData:
                if (body is List<IMultipartFormSection> multipartSections)
                {
                    if (multipartSections.Count > 0)
                    {
                        // IMPORTANT: For multipart/form-data, it's best to use UnityWebRequest.Post (or .Put)
                        // as they internally handle the UploadHandlerMultipart and Content-Type with boundary.
                        // We will then override the method if it's not POST.
                        request = UnityWebRequest.Post(uri, multipartSections);

                        // If the actual method is not POST, override it.
                        // This effectively uses Unity's multipart setup and then changes the verb.
                        if (method != POST)
                        {
                            request.method = method; // Override the method
                        }

                        // UnityWebRequest.Post already sets the Content-Type header with the boundary.
                        // We don't need to manually set inferredContentType here for the general logic below,
                        // as it's already handled within the request itself.
                    }
                    else
                    {
                        Debug.LogWarning("ERequestBodyType.MultipartFormData specified but the list of IMultipartFormSection is empty. Treating as no body.");
                        request = new UnityWebRequest(uri, method); // Fallback to no body
                    }
                }
                else
                {
                    Debug.LogError("Body type for ERequestBodyType.MultipartFormData must be List<IMultipartFormSection>.");
                    return null;
                }
                break;

            default:
                Debug.LogError($"Unsupported request body type: {requestBodyType}");
                return null;
        }

        // Set common properties
        request.downloadHandler = new DownloadHandlerBuffer();
        request.certificateHandler = certificateHandler;

        // --- Applying headers ---
        if (headers != null)
        {
            foreach (var header in headers)
            {
                // For MultipartFormData, UnityWebRequest.Post already sets Content-Type.
                // Do not overwrite it with user-provided Content-Type.
                if (header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                {
                    if (requestBodyType == ERequestBodyType.MultipartFormData)
                    {
                        Debug.LogWarning("You've manually specified a Content-Type header for a multipart/form-data request. Unity handles this automatically. Your custom Content-Type will be ignored.");
                        continue; // Skip this header
                    }
                    // For RawString/RawBytes, if a Content-Type is provided, it takes precedence.
                    request.SetRequestHeader(header.Key, header.Value);
                    inferredContentType = null; // No longer need to apply inferred if explicitly set
                }
                else
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }
        }

        // If we have an inferred Content-Type (from RawString/RawBytes) and it hasn't been set by explicit headers, apply it.
        // Also ensure a Content-Type is set if there's a raw body and no header was explicitly given.
        if (requestBodyType != ERequestBodyType.None && requestBodyType != ERequestBodyType.MultipartFormData) // Only for Raw types
        {
            if (request.GetRequestHeader("Content-Type") == null) // If no explicit Content-Type was given
            {
                if (!string.IsNullOrEmpty(inferredContentType))
                {
                    request.SetRequestHeader("Content-Type", inferredContentType);
                }
                else // Fallback if no specific inferred type
                {
                    request.SetRequestHeader("Content-Type", "application/octet-stream");
                }
            }
        }

        return request;
    }

    public string CreateQueryString(Dictionary<string, string> queryParams)
    {
        string queryString = string.Empty;
        bool isFirst = true;
        foreach (KeyValuePair<string, string> item in queryParams)
        {
            queryString += (isFirst ? "?" : "&") + UnityWebRequest.EscapeURL(item.Key) + "=" + UnityWebRequest.EscapeURL(item.Value);
            isFirst = false;
        }
        return queryString;
    }
}