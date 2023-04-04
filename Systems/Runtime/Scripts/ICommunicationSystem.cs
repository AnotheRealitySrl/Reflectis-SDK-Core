using UnityEngine;
using UnityEngine.Events;
using SPACS.Core;
using System;

namespace SPACS.SDK.Systems {
    public interface ICommunicationSystem : ISystem {
        CommunicatioChannel CurrentRoom { get; }

        void AskPermissions();
        void InitEngine();
        void DisposeEngine();
        void ConnectToChannel(CommunicatioChannel _room);
        void DisconnectFromChannel();

        void MuteAllRemoteStream(bool muteAudio, bool muteVideo);

        void MuteUser(RemoteUser _user);

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

    public struct CommunicationMessage {
        public int ID;
        public string Message;
    }

    public enum ChannelType {
        None = 0,
        Chat = 1,
        Audio = 2,
        Video_Audio = 3,
    }

    public struct CommunicatioChannel {
        public string name;

        public ChannelType Type;

        public bool hasChat { get { return true; } }
        public bool hasAudio { get { return true; } }
        public bool hasVideo { get { return Type == ChannelType.Video_Audio; } }

        public bool muteAudio;
        public bool muteVideo;

        public int videoHeight;
        public int videoWidth;

        public CommunicatioChannel(string name, ChannelType type, bool muteAudio, bool muteVideo, int videoHeight, int videoWidth) {
            this.name = name;

            Type = type;

            this.muteAudio = muteAudio;
            this.muteVideo = muteVideo;

            this.videoHeight = videoHeight;
            this.videoWidth = videoWidth;
        }

        public CommunicatioChannel(string name, ChannelType type, bool muteAudio, bool muteVideo) {
            this.name = name;
            Type = type;

            this.muteAudio = muteAudio;
            this.muteVideo = muteVideo;

            videoHeight = 640;
            videoWidth = 360;
        }

        public CommunicatioChannel(string name, ChannelType type) {
            this.name = name;
            Type = type;

            muteAudio = false;
            muteVideo = false;

            videoHeight = 640;
            videoWidth = 360;
        }
    }


    public struct RemoteUser {
        public uint uid;

        public bool muteAudio;
        public bool muteVideo;

        public RemoteUser(uint uid, bool muteAudio, bool muteVideo) {
            this.uid = uid;
            this.muteAudio = muteAudio;
            this.muteVideo = muteVideo;
        }
    }




}