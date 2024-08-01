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
}
