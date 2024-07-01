using System;
using System.IO;
using System.Xml.Serialization;

namespace Reflectis.SDK.TextChat
{
    [Serializable]
    public class ChatMessage
    {
        /// <summary>
        /// The use ID of the message sender.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// The user ID of the message recipient.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// The local Unix timestamp for creating the message. The unit is millisecond.
        /// </summary>
        public long LocalTime { get; set; }

        /// <summary>
        /// The text message content.
        /// </summary>
        public string Text { get; set; }

        public ChatMessage()
        {
        }

        public ChatMessage(string from, string to, long localTime, string text)
        {
            From = from;
            To = to;
            LocalTime = localTime;
            Text = text;
        }

        //public static object Deserialize(byte[] data)
        //{
        //    return Newtonsoft.Json.JsonConvert.DeserializeObject<ChatMessage>(Encoding.ASCII.GetString(data));
        //}

        //public static byte[] Serialize(object chatMessage)
        //{
        //    return Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(chatMessage));
        //}
        public static object Deserialize(byte[] data)
        {
            using MemoryStream ms = new MemoryStream(data);
            XmlSerializer bf = new XmlSerializer(typeof(ChatMessage));

            return bf.Deserialize(ms);
        }

        public static byte[] Serialize(object chatMessage)
        {
            using MemoryStream ms = new MemoryStream();
            XmlSerializer bf = new XmlSerializer(typeof(ChatMessage));
            bf.Serialize(ms, chatMessage);

            return ms.ToArray();
        }
    }
}
