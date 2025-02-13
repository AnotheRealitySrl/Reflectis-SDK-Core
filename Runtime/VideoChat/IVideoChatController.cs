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
        string StreamerId { get; }

        void Destroy();
    }
}
