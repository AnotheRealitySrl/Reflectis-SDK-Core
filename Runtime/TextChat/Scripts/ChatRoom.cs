using System.Collections.Generic;

namespace Virtuademy.SDK.Core.TextChat
{
    public class ChatRoom
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<ChatMessage> Messages { get; set; }
        public List<string> MemberList { get; set; }

    }
}
