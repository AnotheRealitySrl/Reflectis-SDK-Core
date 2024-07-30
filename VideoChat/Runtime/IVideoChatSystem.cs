using Reflectis.SDK.Core;

using UnityEngine.Events;

namespace Reflectis.SDK.VideoChat
{
    public interface IVideoChatSystem : ISystem
    {
        string AppId { get; set; }

        UnityEvent<string, uint> OnChannelJoined { get; }
        UnityEvent OnChannelLeft { get; }
        UnityEvent<uint> OnUserJoined { get; }
        UnityEvent<uint> OnUserLeft { get; }
        UnityEvent<int, string> OnError { get; }

        void JoinChannel(string channelName);
        void LeaveChannel();
        void CreateVideoView(IVideoChatController videoChatController, uint userId);
    }
}
