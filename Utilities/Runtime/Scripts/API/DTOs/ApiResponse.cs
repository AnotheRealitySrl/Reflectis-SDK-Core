using System;
using Newtonsoft.Json;
using UnityEngine;

namespace SPACS.SDK.Utilities.API
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
            ReasonPhrase = reasonPhrase;
            Content = IsSuccess ? JsonConvert.DeserializeObject<T>(content) : null;
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
            ReasonPhrase = reasonPhrase;
            Content = IsSuccess ? JsonArrayHelper.FromJson<T>(content) : null;
        }
    }
}