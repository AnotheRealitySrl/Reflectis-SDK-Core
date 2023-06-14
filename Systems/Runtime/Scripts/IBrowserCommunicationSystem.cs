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

        /// <summary>
        /// Sends a string to the system as if the string was received from web.
        /// Used for DEBUG.
        /// </summary>
        /// <param name="payload">The payload to manage.</param>
        public void fromWeb(string payload);
    }
}
