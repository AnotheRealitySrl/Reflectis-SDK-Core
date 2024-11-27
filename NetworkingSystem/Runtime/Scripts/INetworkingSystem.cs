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
        public UnityEvent<int, int> OtherPlayerJoinedShard { get; }
        /// <summary>
        /// int userID, int actorNumber
        /// </summary>
        public UnityEvent<int, int> OtherPlayerLeftShard { get; }

        /// <summary>
        /// Returns true if the shard where the local user currently is is open. 
        /// </summary>
        public bool IsCurrentShardOpen { get; }

        /// <summary>
        /// Returns the actor number of the local player.
        /// </summary>
        public int LocalPlayerId { get; }
        
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
