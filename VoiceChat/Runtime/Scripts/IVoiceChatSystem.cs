using Reflectis.SDK.Core;

using System;

using UnityEngine;

namespace Reflectis.SDK.VoiceChat
{
    /// <summary>
    /// Manages the voice (and potentially video, but not yet implemented) communication among players
    /// </summary>
    public interface IVoiceChatSystem : ISystem
    {
        CommunicationChannel CurrentChannel { get; }
        string AppId { get; }

        void AskPermissions();

        /// <summary>
        /// Must be called BEFORE joining a channel
        /// </summary>
        void InitEngine();

        /// <summary>
        /// Must be called AFTER leaving a channel
        /// </summary>
        void DisposeEngine();

        /// <summary>
        /// Connects to a communication channel channel
        /// </summary>
        /// <param name="channel">Contains the name of the channel and the type of communication Audio/Video</param>
        void ConnectToChannel(CommunicationChannel channel);

        /// <summary>
        /// Disconnects from the current channel
        /// </summary>
        void DisconnectFromChannel();

        void MuteAllRemoteStream(bool muteAudio, bool muteVideo);

        void MuteUser(RemoteUser user);

        void MuteLocalUser(bool muteAudio, bool muteVideo);

        void DestroyVideoView(uint uid);

        GameObject MakeVideoView(string parentName, GameObject videoView, uint uid = 0, string channelId = "");

        string GetChannelName();

        void SetVolume(int volume);

        float GetSystemVolume();

        #region Events

        event Action<string> OnJoinChannelSuccess;
        event Action<string> OnRejoinChannelSuccess;
        event Action OnLeaveChannel;
        event Action<CommunicationMessage> OnError;
        event Action<int> OnUserJoined;
        event Action<int> OnUserLeave;
        event Action<int, bool> OnRemoteAudioStateChanged;
        event Action<int, bool> OnRemoteVideoStateChanged;

        #endregion
    }

    public struct CommunicationMessage
    {
        public int ID;
        public string Message;
    }

    public enum ChannelType
    {
        None = 0,
        Chat = 1,
        Audio = 2,
        AudioVideo = 3,
    }

    public struct CommunicationChannel
    {
        public string name;

        public ChannelType Type;

        public bool HasChat => true;
        public bool HasAudio => true;
        public bool HasVideo => Type == ChannelType.AudioVideo;

        public bool muteAudio;
        public bool muteVideo;

        public int videoHeight;
        public int videoWidth;

        public CommunicationChannel(string name, ChannelType type, bool muteAudio, bool muteVideo, int videoHeight, int videoWidth)
        {
            this.name = name;

            Type = type;

            this.muteAudio = muteAudio;
            this.muteVideo = muteVideo;

            this.videoHeight = videoHeight;
            this.videoWidth = videoWidth;
        }

        public CommunicationChannel(string name, ChannelType type, bool muteAudio, bool muteVideo)
        {
            this.name = name;
            Type = type;

            this.muteAudio = muteAudio;
            this.muteVideo = muteVideo;

            videoHeight = 640;
            videoWidth = 360;
        }

        public CommunicationChannel(string name, ChannelType type)
        {
            this.name = name;
            Type = type;

            muteAudio = false;
            muteVideo = false;

            videoHeight = 640;
            videoWidth = 360;
        }
    }

    public struct RemoteUser
    {
        public uint uid;

        public bool muteAudio;
        public bool muteVideo;

        public RemoteUser(uint uid, bool muteAudio, bool muteVideo)
        {
            this.uid = uid;
            this.muteAudio = muteAudio;
            this.muteVideo = muteVideo;
        }
    }

}