using Reflectis.SDK.Core;

using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine.Events;

using static IVideoChatController;

namespace Reflectis.SDK.VideoChat
{
    public interface IVideoChatSystem : ISystem
    {
        string AppId { get; set; }
        bool IsScreenSharing { get; }
        bool InChannel { get; }
        bool Initialized { get; }

        List<uint> ActiveWebcams { get; }
        Dictionary<uint, uint> ActiveScreenshares { get; }

        UnityEvent<string, uint> OnChannelJoined { get; }
        UnityEvent OnChannelLeft { get; }
        UnityEvent<uint> OnUserJoined { get; }
        UnityEvent<uint> OnUserLeft { get; }
        UnityEvent<int, string> OnError { get; }
        UnityEvent<string, uint> OnScreenShareStarted { get; }
        UnityEvent<string, uint> OnScreenShareStopped { get; }
        UnityEvent<string, uint> OnScreenShareCanceled { get; }
        UnityEvent<uint, int, int, int> OnVideoSizeChanged { get; }

        void InitEngine();
        void JoinChannel(string channelName);
        Task JoinChannelAsync(string channelName);
        void LeaveChannel();
        Task LeaveChannelAsync();
        void AddVideoView(IVideoChatController videoChatController);
        void RemoveVideoView(IVideoChatController videoChatController);
        Task DestroyActiveVideoViewsAsync();
        (bool, int) CanSpawn(EVideoChatType videoChatType);
    }
}
