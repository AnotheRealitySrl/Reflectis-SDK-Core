using System;

using System.Threading.Tasks;

namespace Reflectis.SDK.WebSocket
{
    public interface IWebSocketListener
    {
        public void OnWebSocketOpen();
        public void OnWebSocketError(string error);
        public void OnWebSocketMessageReceived(string data);
        public void OnWebSocketClose();
    }
}
