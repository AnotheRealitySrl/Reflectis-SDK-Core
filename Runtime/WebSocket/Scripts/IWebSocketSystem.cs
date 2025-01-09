using Reflectis.SDK.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine.Events;

namespace Reflectis.SDK.Core.WebSocket
{
    public interface IWebSocketSystem : ISystem
    {
        /// <summary>
        /// Connect to a webSocket
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryParams"></param>
        /// <param name="onWebSocketOpen"></param>
        /// <param name="onWebSocketOpenError">if there was an error while opening the websocket call the action with the error message param</param>
        /// <returns></returns>
        Task ConnectAsync(string url, Dictionary<string, string> queryParams, Action onWebSocketOpen = null, Action<string> onWebSocketOpenError = null);

        /// <summary>
        /// Add listener to the webSocket
        /// </summary>
        /// <param name="url"></param>
        /// <param name="webSocketListener"></param>
        void AddListener(string url, IWebSocketListener webSocketListener);

        /// <summary>
        /// Disconnect this listener from the socket, this will NOT call the listener onClose
        /// </summary>
        /// <param name="url"></param>
        /// <param name="webSocketListener"></param>
        /// <returns></returns>
        void RemoveListener(string url, IWebSocketListener webSocketListener);

        /// <summary>
        /// Hard disconnect from the socket
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task DisconnectAsync(string url);

        /// <summary>
        /// Send message to a websocket we are connected to. 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendMessageAsync(string url, string message);

    }
}
