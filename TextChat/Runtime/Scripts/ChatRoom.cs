using System.Collections.Generic;

namespace Reflectis.SDK.TextChat
{
    public class ChatRoom
    {
        public string Id { get; internal set; }

        public string Name { get; internal set; }

        public string Description { get; internal set; }

        public int MaxUsers { get; internal set; }

        public List<string> MemberList { get; internal set; }

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
