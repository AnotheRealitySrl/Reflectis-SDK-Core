using System;
using SPACS.Core;

namespace SPACS.SDK.Systems
{
    public interface IBrowserCommunicationSystem : ISystem
    {
        public event Action<string> OnWebMessageReceived;

        /// <summary>
        /// Sends a string to JS.
        /// </summary>
        /// <param name="payload">The payload to send.</param>
        public void SendMessageToWeb(string payload);

        /// <summary>
        /// Sends a raw message to JS.
        /// </summary>
        /// <param name="payload">The payload to send.</param>
        public void SendRawToWeb(string payload);
    }
}
