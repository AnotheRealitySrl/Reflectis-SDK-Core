using System;
using System.Collections.Generic;

using Reflectis.SDK.Core;
using Reflectis.SDK.TextChat.Enums;
using UnityEngine;

namespace Reflectis.SDK.TextChat
{
    /// <summary>
    /// Manages the text communication among players.
    /// </summary>
    public interface ITextChatSystem : ISystem
    {
        string[] Channels { get; }
        
        bool IsConnected { get; set; }
        
        #region Events
        
        #region Account events
        
        /// <summary>
        /// Event invoked when the user connects to the chat
        /// </summary>
        event Action OnUserConnected;

        /// <summary>
        /// Event invoked when the user disconnects to the chat
        /// </summary>
        event Action OnUserDisconnected;
        
        #endregion

        #region Message events
        /// <summary>
        /// Event invoked when a message is sent.
        /// It will pass the name of the person/channel who
        /// send it and the content of the message
        /// </summary>
        event Action<string, string> OnTxtMsgSent;

        /// <summary>
        /// Event invoked when a message is received. 
        /// It will pass the name of the person/channel who
        /// sent it, the content of the message and for who it is
        /// </summary>
        event Action<string, string, string> OnTxtMsgReceived;


        /// <summary>
        /// Event invoked when the history messages are fetched.
        /// It will pass a list with all the ChatMessages sent.
        /// </summary>
        event Action<List<ChatMessage>> OnTxtMsgFetched;
        #endregion

        #region Channel events

        /// <summary>
        /// Event invoked when all the public channels are fetched from the server.
        /// It will pass a list containing all the chat rooms
        /// </summary>
        event Action<List<ChatRoom>> OnAllChannelsFetched;

        /// <summary>
        /// Event invoked when a user joins a channel
        /// </summary>
        event Action OnJoinedChannel;

        /// <summary>
        /// Event invoked when a public channel is fetched from the server.
        /// It will pass the channel
        /// </summary>
        event Action<ChatRoom> OnChannelFetched;

        #endregion

        #endregion

        #region Methods
        /// <summary>
        /// Connect the current user to the text chat.
        /// </summary>
        /// <param name="userId">Id of the user to connect to the text chat</param>
        /// <param name="token">A token use to authorize the connection if needed</param>
        void ConnectUserToTextChat(string userId, string token = "");

        /// <summary>
        /// Keep the connection alive so a user can get incoming messages continuously
        /// </summary>
        void KeepConnectionAlive();

        /// <summary>
        /// Disconnect the current user to the text chat
        /// </summary>
        void DisconnectCurrentUser();
        
        /// <summary>
        /// Sends a new message to a specific user
        /// </summary>
        /// <param name="userId">Id of the user who will receive the message</param>
        /// <param name="msg">A message</param>
        void SendMessageToUser(string userId, ChatMessage msg);

        /// <summary>
        /// Sends a message to a specific channel
        /// </summary>
        /// <param name="channelId">Id of the channel that will receive the message</param>
        /// <param name="msg">A message</param>
        void SendMessageToChannel(string channelId, ChatMessage msg);

        /// <summary>
        /// Get all the history messages sent through a specific channel
        /// </summary>
        /// <param name="channelId">Id of the channel to fetch all the messages</param>
        /// <param name="type">The type of the channel</param>
        void FetchHistoryMessages(string channelId, EChatMessageType type);

        /// <summary>
        /// Get all the public channels
        /// </summary>
        void FetchAllChannels();

        /// <summary>
        /// Joins the default channels
        /// </summary>
        void JoinTextChannel();
        
        /// <summary>
        /// Joins a text channel
        /// </summary>
        /// <param name="channelId">Id of the channel</param>
        void JoinTextChannel(string channelId);

        
        /// <summary>
        /// Get info from a specific channel
        /// </summary>
        /// <param name="channelId"></param>
        void FetchChannelInfo(string channelId);

        /// <summary>
        /// Leave a text channel
        /// </summary>
        /// <param name="channelId">Id of the channel</param>
        void LeaveTextChannel(string channelId);

        /// <summary>
        /// Delete all messages from a specific conversation.
        /// This method is not supported for PhotonTextChatSystem.
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="type"></param>
        void DeleteConversationFromServer(string conversationId, EChatMessageType type);

        #endregion
    }
}