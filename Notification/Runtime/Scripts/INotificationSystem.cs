using Reflectis.SDK.Core;
using System;
using System.Collections.Generic;

namespace Reflectis.SDK.Notification
{
    public interface INotificationSystem : ISystem
    {
        #region Account events

        /// <summary>
        /// Event invoked when a message is received. 
        /// It will pass the name of the person/channel who
        /// sent it, the content of the message and the time when was send it
        /// </summary>
        event Action<NotificationInfo> OnNotificationReceived;

        /// <summary>
        /// Event invoked when the user connects to the chat
        /// </summary>
        event Action OnUserConnected;

        /// <summary>
        /// Event invoked when the user disconnects to the chat
        /// </summary>
        event Action OnUserDisconnected;

        bool IsConnected { get; }
        #endregion

        #region Methods

        /// <summary>
        /// Request to connect the user to the API
        /// </summary>
        void Connect();
        /// <summary>
        /// Get all the history messages sent through a specific channel
        /// </summary>
        /// <param name="channelId">Id of the channel to fetch all the messages</param>
        /// <param name="type">The type of the channel</param>
        List<NotificationInfo> GetNotificationHistory();

        #endregion
    }
}
