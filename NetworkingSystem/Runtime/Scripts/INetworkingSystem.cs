using Reflectis.SDK.Core;
using UnityEngine.Events;

namespace Reflectis.SDK.NetworkingSystem
{
    public interface INetworkingSystem : ISystem
    {
        /// <summary>
        /// int userID, int actorNumber
        /// </summary>
        public UnityEvent<int, int> OtherPlayerJoinedRoom { get; }
        /// <summary>
        /// int userID, int actorNumber
        /// </summary>
        public UnityEvent<int, int> OtherPlayerLeftRoom { get; }

        public void Setup();
    }

}
