using Newtonsoft.Json;
using Sirenix.Utilities;
using System;

using UnityEngine;

namespace Reflectis.SDK.Utilities.API
{
    [Serializable, JsonObject(MemberSerialization.Fields)]
    public class ApiResponse
    {
        [SerializeField] private int statusCode;
        [SerializeField] private string reasonPhrase;
        [SerializeField] int content;

        public bool IsSuccess { get => (statusCode >= 200) && (statusCode <= 299); }
        public long StatusCode { get => statusCode; private set => statusCode = (int)value; }
        public string ReasonPhrase { get => reasonPhrase; private set => reasonPhrase = value; }
        public int Content { get => content; set => content = value; }

        public ApiResponse(long statusCode, string reasonPhrase, string content)
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
            Content = IsSuccess ? int.Parse(content) : -1;
        }
    }

    [Serializable]
    public class ApiResponse<T> where T : class
    {
        [SerializeField] private int statusCode;
        [SerializeField] private string reasonPhrase;
        [SerializeField] T content;

        public bool IsSuccess { get => (statusCode >= 200) && (statusCode <= 299); }
        public long StatusCode { get => statusCode; private set => statusCode = (int)value; }
        public string ReasonPhrase { get => reasonPhrase; private set => reasonPhrase = value; }
        public T Content { get => content; set => content = value; }

        public ApiResponse(long statusCode, string reasonPhrase, string content)
        {
            StatusCode = statusCode;
            if (!((statusCode >= 200) && (statusCode <= 299)) && !content.IsNullOrWhitespace())
            {
                JsonConvert.DeserializeObject<ApiResponseError>(content).DisplayError();
            }
            ReasonPhrase = reasonPhrase;
            if (typeof(T) == typeof(string))
            {
                Content = IsSuccess ? (T)(object)content : null;
            }
            else
            {
                Content = IsSuccess ? JsonConvert.DeserializeObject<T>(content) : null;
            }
        }
    }

    [Serializable]
    public class ApiResponseArray<T> where T : class
    {
        [SerializeField] private int statusCode;
        [SerializeField] private string reasonPhrase;
        [SerializeField] T[] content;

        public bool IsSuccess { get => (statusCode >= 200) && (statusCode <= 299); }
        public long StatusCode { get => statusCode; private set => statusCode = (int)value; }
        public string ReasonPhrase { get => reasonPhrase; private set => reasonPhrase = value; }
        public T[] Content { get => content; set => content = value; }

        public ApiResponseArray(long statusCode, string reasonPhrase, string content)
        {
            StatusCode = statusCode;
            if (!((statusCode >= 200) && (statusCode <= 299)) && !content.IsNullOrWhitespace())
            {
                JsonConvert.DeserializeObject<ApiResponseError>(content).DisplayError();
            }
            ReasonPhrase = reasonPhrase;
            Content = IsSuccess ? JsonArrayHelper.FromJson<T>(content) : null;
        }
    }

    [Serializable]
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class ApiResponseError
    {
        private string type;
        private string title;
        private int status;
        private string traceId;

        public string Type { get => type; set => type = value; }
        public string Title { get => title; set => title = value; }
        public int Status { get => status; set => status = value; }
        public string TraceId { get => traceId; set => traceId = value; }

        public void DisplayError()
        {
            Debug.LogError(Title);
        }
    }
}