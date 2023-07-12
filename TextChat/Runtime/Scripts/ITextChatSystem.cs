using Reflectis.SDK.Core;

namespace Reflectis.SDK.TextChat
{
    /// <summary>
    /// Manages the text communication among players.
    /// </summary>
    public interface ITextChatSystem : ISystem
    {
        /// <summary>
        /// Key of the chat project in Agora console.
        /// Looks like: 12345678#1234567
        /// </summary>
        string AppKey { get; }

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
        /// Get all the public channels from the Agora server
        /// </summary>
        void FetchChannelsFromServer();

        /// <summary>
        /// Join a channel
        /// </summary>
        /// <param name="channelId">Id of the channel</param>
        void JoinTextChannel(string channelId);

        /// <summary>
        /// Leave a channel
        /// </summary>
        /// <param name="channelId">Id of the channel</param>
        void LeaveTextChannel(string channelId);
    }
}