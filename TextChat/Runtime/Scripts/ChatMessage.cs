using UnityEngine;

using Reflectis.SDK.TextChat.Enums;

namespace Reflectis.SDK.TextChat
{
    public class ChatMessage : MonoBehaviour
    {
        /// <summary>
        /// The message ID
        /// </summary>
        public string MsgId { get; internal set; }
        
        /// <summary>
        /// The ID of the conversation to which the message belongs.
        /// </summary>
        public string ConversationId { get; internal set; }
        
        /// <summary>
        /// The use ID of the message sender.
        /// </summary>
        public string From { get; internal set; }
        
        /// <summary>
        /// The user ID of the message recipient.
        /// </summary>
        public string To { get; internal set; }
        
        /// <summary>
        /// The message type.
        ///
        /// - 'Chat': The one-to-one chat message.
        /// - 'Room': The chat room message.
        /// </summary>
        public EChatMessageType ChatMessageType { get; internal set; }
        
        /// <summary>
        /// The message direction, that is, whether the message is received or sent.
        ///
        /// - 'SEND': This message is sent from the local client.
        /// - `RECEIVE`: The message is received by the local client.
        /// </summary>
        public EChatMessageDirection ChatMessageDirection { get; set; }
        
        /// <summary>
        /// The local Unix timestamp for creating the message. The unit is millisecond.
        /// </summary>
        public long LocalTime { get; internal set; }
        
        /// <summary>
        /// The Unix timestamp when the message is received by the server. The unit is millisecond.
        /// </summary>
        public long ServerTime { get; internal set; }
        
        /// <summary>
        /// The text message content.
        /// </summary>
        public string Text { get; internal set; }
        
        public ChatMessage(string msgId, string conversationId, string from, string to, EChatMessageType chatMessageType, EChatMessageDirection chatMessageDirection, long localTime, long serverTime, string text)
        {
            MsgId = msgId;
            ConversationId = conversationId;
            From = from;
            To = to;
            ChatMessageType = chatMessageType;
            ChatMessageDirection = chatMessageDirection;
            LocalTime = localTime;
            ServerTime = serverTime;
            Text = text;
        }
    }
}
