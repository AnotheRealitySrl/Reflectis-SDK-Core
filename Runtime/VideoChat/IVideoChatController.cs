namespace Reflectis.SDK.Core.VideoChat
{
    public interface IVideoChatController
    {
        public enum EVideoChatType
        {
            Webcam,
            ScreenShare
        }

        uint UserId { get; }
        public EVideoChatType VideoChatType { get; }
        uint ScreenShareId { get; }
        bool AmStreamer { get; }
        int StreamerId { get; }

        void Destroy();
    }
}
