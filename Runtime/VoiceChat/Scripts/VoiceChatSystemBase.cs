using Reflectis.SDK.Core.SystemFramework;

using System;

using UnityEngine;

namespace Reflectis.SDK.Core.VoiceChat
{
    /// <summary>
    /// Abstract class based on of <see cref="IVoiceChatSystem"/>
    /// </summary>
    public abstract class VoiceChatSystemBase : BaseSystem, IVoiceChatSystem
    {
        // TODO: unused, since it's configured in Photon server settings
        [Header("Id of Agora subscription")]
        [SerializeField] protected string appId;

        [SerializeField, Min(1)]
        protected byte photonGlobalChannel = 1;

        [Header("Settings")]
        [SerializeField] protected bool enableMicrophoneByDefault = false;

        public virtual event Action<string> OnJoinChannelSuccess;
        public virtual event Action<string> OnRejoinChannelSuccess;
        public virtual event Action OnLeaveChannel;
        public virtual event Action<CommunicationMessage> OnError;
        public virtual event Action<int> OnUserJoined;
        public virtual event Action<int> OnUserLeave;
        public virtual event Action<int, bool> OnRemoteAudioStateChanged;
        public virtual event Action<int, bool> OnRemoteVideoStateChanged;
        public Action<string> OnDisconnected;

        protected CommunicationChannel currentChannel;
        public CommunicationChannel CurrentChannel { get => currentChannel; set => currentChannel = value; }

        public string AppId { get => appId; set => appId = value; }

        public byte PHOTON_GLOBAL_CHANNEL => photonGlobalChannel;

        public abstract bool IsConnected { get; }
        public abstract void ConnectToService(string id);
        public abstract void DisconnectFromService();
        public abstract void AskPermissions();
        public abstract void ConnectToChannel(CommunicationChannel channel);
        public abstract void DestroyVideoView(uint uid);
        public abstract void DisconnectFromChannel();
        public abstract string GetChannelName();
        public abstract float GetSystemVolume();
        public abstract bool GetVoiceDetection();
        public abstract GameObject MakeVideoView(string parentName, GameObject videoView, uint uid = 0, string channelId = "");
        public abstract void MuteAllRemoteStream(bool muteAudio, bool muteVideo);
        public abstract void MuteLocalUser(bool muteAudio);
        public abstract void MuteLocalUser(bool muteAudio, bool muteVideo);
        public abstract void SetVolume(int volume);
    }
}

