using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.WebSocket
{
    public class BaseWebSocketListener : IWebSocketListener
    {
        public Action onClose;

        public Action onOpen;

        public Action<string> onMessageReceived;

        public Action<string> onError;

        public Action<string> onOpeningError;

        public void OnWebSocketClose()
        {
            onClose?.Invoke();
        }

        public void OnWebSocketError(string error)
        {
            onError?.Invoke(error);
        }

        public void OnWebSocketMessageReceived(string data)
        {
            onMessageReceived?.Invoke(data);
        }

        public void OnWebSocketOpen()
        {
            onOpen?.Invoke();
        }

        public void OnWebSocketOpeningError(string error)
        {
            onOpeningError?.Invoke(error);
        }
    }
}
