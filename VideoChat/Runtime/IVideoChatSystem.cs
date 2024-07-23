using Reflectis.SDK.Core;

namespace Reflectis.SDK.VideoChat
{
    public interface IVideoChatSystem : ISystem
    {
        string AppId { get; set; }

        void JoinChannel(string channelName);
        void LeaveChannel();
        void CreateVideoView(IVideoChatController videoChatController, uint userId);
    }
}
