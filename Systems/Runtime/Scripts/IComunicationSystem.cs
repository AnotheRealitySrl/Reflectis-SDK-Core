using UnityEngine;
using UnityEngine.Events;
using SPACS.Core;
using System;

namespace SPACS.SDK.Systems
{
    public interface IComunicationSystem : ISystem
    {
        void AskPermissions();
        void LoadAuthenticationData(string appId, string token, string channelName);

        void LoadEngine();

        //Buono sia per video che audio 
        void ConnectToRoom(Room _room);

        void DisconnectFromRoom();

        void MuteAllRemoteStream(Room room);

        void MuteUser(RemoteUser _user);

        void DestroyVideoView(uint uid);

        void MakeVideoView(uint uid, string parentName, GameObject image, bool islocal, string channelId = "");

        string GetChannelName();

        #region Actions

        StringEvent OnError { get; }
        StringEvent OnGetMessage { get; }

        UnityEvent OnJoinChannelSuccess { get; }

        UnityEvent OnRejoinChannelSuccess { get; }
        UnityEvent OnLeaveChannel { get; }

        UnityEvent OnUserJoined { get; }
        UnityEvent OnUserLeft { get; }

        UnityEvent OnUserOffline { get; }

        UnityEvent OnRemoteAudioStateChanged { get; }
        UnityEvent OnRemoteVideoStateChanged { get; }

        UnityEvent OnClientRoleChanged { get; }
        #endregion
    }

    public struct Room
    {
        public string Name;

        public bool hasAudio;
        public bool hasVideo;

        public bool muteAudio;
        public bool muteVideo;

        public int videoHeight;
        public int videoWidth;
    }

    public struct RemoteUser
    {
        public uint uId;

        public bool muteAudio;
        public bool muteVideo;
    }

    [System.Serializable]
    public class StringEvent : UnityEvent<string>
    {
    }
}

