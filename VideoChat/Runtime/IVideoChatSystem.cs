using Reflectis.SDK.Core;

using UnityEngine.Events;

namespace Reflectis.SDK.VideoChat
{
    public interface IVideoChatSystem : ISystem
    {
        public class StreamingClientData
        {
            public uint userId;
            public uint screenShareId;

            public StreamingClientData(uint userId, uint screenShareId)
            {
                this.userId = userId;
                this.screenShareId = screenShareId;
            }
        }

        string AppId { get; set; }
        bool IsStreaming { get; }

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
        void LeaveChannel();
        void AddVideoView(int videoViewId, StreamingClientData videoViewData);
        void RemoveVideoView(int videoViewId);
    }
}
