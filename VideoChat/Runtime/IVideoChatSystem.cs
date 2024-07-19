using Reflectis.SDK.Core;

using UnityEngine;

namespace Reflectis.SDK.VideoChat
{
    public interface IVideoChatSystem : ISystem
    {
        string AppId { get; set; }
        GameObject CurrentVideo { get; set; }

        void JoinChannel(string channelName);
        void LeaveChannel();
    }
}
