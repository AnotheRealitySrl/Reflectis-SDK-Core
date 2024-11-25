using Reflectis.SDK.Core;

using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine.Events;

namespace Reflectis.SDK.WebSocket
{
    public interface IWebSocketSystem : ISystem
    {
        UnityEvent OnSocketOpen { get; }
        UnityEvent OnSocketClose { get; }
        UnityEvent<string> OnSocketMessage { get; }
        UnityEvent<string> OnSocketError { get; }

        Task ConnectAsync(string url, Dictionary<string, string> queryParams);
        Task DisconnectAsync();
        Task SendMessageAsync(string message);
    }
}
