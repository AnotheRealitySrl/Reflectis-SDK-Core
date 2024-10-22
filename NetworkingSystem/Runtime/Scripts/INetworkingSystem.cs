using Reflectis.SDK.Core;
using System.Threading.Tasks;
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

        /// <summary>
        /// Opens the event shard where the local user currently is. 
        /// </summary>
        public Task OpenCurrentShard();
        /// <summary>
        /// Closes the event shard where the local user currently is.
        /// </summary>
        public Task CloseCurrentShard();
    }

}
