using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine.Events;

namespace Reflectis.SDK.VideoChat
{
    public interface IVideoChatSystem : ISystem
    {
        string AppId { get; set; }
        bool IsScreenSharing { get; }
        bool InChannel { get; }
        bool Initialized { get; }

        UnityEvent<string, uint> OnChannelJoined { get; }
        UnityEvent OnChannelLeft { get; }
        UnityEvent<uint> OnUserJoined { get; }
        UnityEvent<uint> OnUserLeft { get; }
        UnityEvent<int, string> OnError { get; }
        UnityEvent<string, uint> OnScreenShareStarted { get; }
        UnityEvent<string, uint> OnScreenShareStopped { get; }
        UnityEvent<string, uint> OnScreenShareCanceled { get; }
        UnityEvent<uint, int, int, int> OnVideoSizeChanged { get; }

        void JoinChannel(string channelName);
        Task JoinChannelAsync(string channelName);
        void LeaveChannel();
        Task LeaveChannelAsync();
        void AddVideoView(IVideoChatController videoChatController);
        void RemoveVideoView(IVideoChatController videoChatController);
        Task DestroyActiveVideoViewsAsync();
    }
}
