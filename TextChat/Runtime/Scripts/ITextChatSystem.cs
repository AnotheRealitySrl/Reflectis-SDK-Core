using System;
using System.Collections.Generic;

using Reflectis.SDK.Core;
using Reflectis.SDK.TextChat.Enums;

namespace Reflectis.SDK.TextChat
{
    /// <summary>
    /// Manages the text communication among players.
    /// </summary>
    public interface ITextChatSystem : ISystem
    {
        #region Properties
        /// <summary>
        /// Key of the chat project in Agora console.
        /// Looks like: 12345678#1234567
        /// </summary>
        string AppKey { get; }

        #endregion

        #region Events

        #region Account events
        /// <summary>
        /// Event invoked when the user logs in without any problems
        /// </summary>
        event Action OnLoginSuccessful;

        /// <summary>
        /// Event invoked when the user could not log in.
        /// It will pass the code of the error and its description
        /// </summary>
        event Action<int, string> OnLoginFailed;

        /// <summary>
        /// Event invoked when the user logs out without any problems
        /// </summary>
        event Action OnLogoutSuccessful;

        /// <summary>
        /// TODO
        /// </summary>
        event Action<int, string> OnLogoutFailed;
        
        /// <summary>
        /// Event invoked when the user could not log out.
        /// It will pass the code of the error and its description
        /// </summary>
        event Action OnLogout;
        #endregion

        #region Message events
        /// <summary>
        /// Event invoked when a message is sent. It will pass the name of the person/channel who
        /// send it, the content of the message and the local time when it was sent
        /// </summary>
        event Action<string, string, long> OnTxtMsgSent;

        /// <summary>
        /// Event invoked when a message is received. 
        /// It will pass the name of the person/channel who
        /// send it, the content of the message and the local time when it was sent
        /// </summary>
        event Action<string, string, long> OnTxtMsgReceived;


        /// <summary>
        /// 
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
        /// Logs in to the chat server with the user ID and an Agora token.
        /// </summary>
        /// <param name="username">The user ID</param>
        /// <param name="token">The agora token of the user</param>
        void LoginWithToken(string username, string token);

        /// <summary>
        /// Logs out the current user of the chat server
        /// </summary>
        void Logout();

        /// <summary>
        /// Adds a chat manager listener
        /// </summary>
        void AddChatDelegate();

        /// <summary>
        /// Removes a chat manager listener
        /// </summary>
        void RemoveChatDelegate();

        /// <summary>
        /// Sends a new message to a specific user
        /// </summary>
        /// <param name="username">Name of the user who will receive the message</param>
        /// <param name="msgContent">Content of the message</param>
        void SendMessageToUser(string username, string msgContent);

        /// <summary>
        /// Sends a message to a specific channel
        /// </summary>
        /// <param name="channelId">Id of the channel that will receive the message</param>
        /// <param name="msgContent">Content of the message</param>
        void SendMessageToChannel(string channelId, string msgContent);

        /// <summary>
        /// 
        /// </summary>
        void FetchHistoryMessages(string conversationId, EChatMessageType type);
        
        /// <summary>
        /// Get all the public channels from the Agora server
        /// </summary>
        void FetchAllChannels();

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

        #endregion
    }

}