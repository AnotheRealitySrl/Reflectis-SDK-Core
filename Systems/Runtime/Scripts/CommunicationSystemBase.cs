using SPACS.Core;
using SPACS.SDK.Systems;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommunicationSystemBase : BaseSystem, ICommunicationSystem
{
    [SerializeField] protected string appId;

    public event Action<string> OnJoinChannelSuccess;
    public event Action<string> OnRejoinChannelSuccess;
    public event Action OnLeaveChannel;
    public event Action<CommunicationMessage> OnError;
    public event Action<int> OnUserJoined;
    public event Action<int> OnUserLeave;
    public event Action<int, bool> OnRemoteAudioStateChanged;
    public event Action<int, bool> OnRemoteVideoStateChanged;

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
