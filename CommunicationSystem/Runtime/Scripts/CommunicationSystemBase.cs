using SPACS.Core;

using System;

using UnityEngine;

namespace SPACS.SDK.CommunicationSystem
{
    public abstract class CommunicationSystemBase : BaseSystem, ICommunicationSystem
    {
        [SerializeField] protected string appId;

        public virtual event Action<string> OnJoinChannelSuccess;
        public virtual event Action<string> OnRejoinChannelSuccess;
        public virtual event Action OnLeaveChannel;
        public virtual event Action<CommunicationMessage> OnError;
        public virtual event Action<int> OnUserJoined;
        public virtual event Action<int> OnUserLeave;
        public virtual event Action<int, bool> OnRemoteAudioStateChanged;
        public virtual event Action<int, bool> OnRemoteVideoStateChanged;

        protected CommunicatioChannel _currentRoom;

        public CommunicatioChannel CurrentRoom
        {
            get { return _currentRoom; }
            set { _currentRoom = value; }
        }
        public string AppId { get => appId; set => appId = value; }

        public abstract void AskPermissions();
        public abstract void ConnectToChannel(CommunicatioChannel _room);
        public abstract void DestroyVideoView(uint uid);
        public abstract void DisconnectFromChannel();
        public abstract void DisposeEngine();
        public abstract string GetChannelName();
        public abstract float GetSystemVolume();
        public abstract void InitEngine();
        public abstract GameObject MakeVideoView(string parentName, GameObject videoView, uint uid = 0, string channelId = "");
        public abstract void MuteAllRemoteStream(bool muteAudio, bool muteVideo);
        public abstract void MuteLocalUser(bool muteAudio, bool muteVideo);
        public abstract void MuteUser(RemoteUser _user);
        public abstract void SetVolume(int volume);
    }
}

