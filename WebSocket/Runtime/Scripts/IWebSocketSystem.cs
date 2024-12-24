using Reflectis.SDK.Core;

using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine.Events;

namespace Reflectis.SDK.WebSocket
{
    public interface IWebSocketSystem : ISystem
    {
        Task ConnectAsync(string url, Dictionary<string, string> queryParams, IWebSocketListener webSocketListener);
        Task DisconnectAsync(string url);
    }
}
