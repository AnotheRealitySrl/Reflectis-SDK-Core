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

        void MuteAllRemoteStream(bool muteAudio, bool muteVideo);

        void MuteUser(RemoteUser _user);

        void MuteLocalUser(bool muteAudio, bool muteVideo);

        void DestroyVideoView(uint uid);

        GameObject MakeVideoView(string parentName, GameObject videoView, uint uid = 0, string channelId = "");

        //void MakeVideoView(uint uid, string parentName, GameObject image, bool islocal, string channelId = "");

        string GetChannelName();

        #region Actions

        StringEvent OnError { get; }
        StringEvent OnGetMessage { get; }

        UintEvent OnJoinChannelSuccess { get; }

        UnityEvent OnRejoinChannelSuccess { get; }
        UnityEvent OnLeaveChannel { get; }

        UintEvent OnUserJoined { get; }
        UnityEvent OnUserLeft { get; }

        UnityEvent OnUserOffline { get; }

        UnityEvent OnRemoteAudioStateChanged { get; }
        UnityEvent OnRemoteVideoStateChanged { get; }

        UnityEvent OnClientRoleChanged { get; }
        #endregion
    }

    public struct Room
    {
        public string name;

        public bool hasAudio;
        public bool hasVideo;

        public bool muteAudio;
        public bool muteVideo;

        public int videoHeight;
        public int videoWidth;

        public Room(string name, bool hasAudio, bool hasVideo, bool muteAudio, bool muteVideo, int videoHeight, int videoWidth)
        {
            this.name = name;

            this.hasAudio = hasAudio;
            this.hasVideo = hasVideo;

            this.muteAudio = muteAudio;
            this.muteVideo = muteVideo;

            this.videoHeight = videoHeight;
            this.videoWidth = videoWidth;
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
            this.muteVideo= muteVideo;
        }
    }



    [System.Serializable]
    public class StringEvent : UnityEvent<string>
    {
    }

    [System.Serializable]
    public class UintEvent : UnityEvent<uint>
    {

    }
}