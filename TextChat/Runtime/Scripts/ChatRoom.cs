using System.Collections.Generic;

namespace Reflectis.SDK.TextChat
{
    public class ChatRoom
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MaxUsers { get; set; }

        public List<ChatMessage> Messages { get; set; }
        public List<string> MemberList { get; set; }

        public ChatRoom(string id, string name, string description, int maxUsers, List<string> memberList)
        {
            Id = id;
            Name = name;
            Description = description;
            MaxUsers = maxUsers;
            MemberList = memberList;
        }
    }
}
