using System;
using System.Text;

namespace Reflectis.SDK.TextChat
{
    [Serializable]
    public class ChatMessage
    {
        /// <summary>
        /// The use ID of the message sender.
        /// </summary>
        public int From { get; set; }

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

        public ChatMessage(int from, long localTime, string text)
        {
            From = from;
            LocalTime = localTime;
            Text = text;
        }

        public static object Deserialize(byte[] data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ChatMessage>(Encoding.UTF8.GetString(data));
        }

        public static byte[] Serialize(object chatMessage)
        {
            return Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(chatMessage));
        }

        //public static object Deserialize(byte[] data)
        //{
        //    using MemoryStream ms = new MemoryStream(data);
        //    XmlSerializer bf = new XmlSerializer(typeof(ChatMessage));

        //    return bf.Deserialize(ms);
        //}

        //public static byte[] Serialize(object chatMessage)
        //{
        //    using MemoryStream ms = new MemoryStream();
        //    XmlSerializer bf = new XmlSerializer(typeof(ChatMessage));
        //    bf.Serialize(ms, chatMessage);

        //    return ms.ToArray();
        //}

        public DateTime GetMessageLocalTime()
        {
            return new DateTime(LocalTime, DateTimeKind.Unspecified).ToLocalTime();
        }
    }
}
