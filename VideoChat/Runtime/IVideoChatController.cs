public interface IVideoChatController
{
    public enum EVideoChatType
    {
        Webcam,
        ScreenShare
    }

    uint UserId { get; }
    public EVideoChatType VideoChatType { get; }
    bool IsStreamer { get; }
    void Resize(float ratio);
}
