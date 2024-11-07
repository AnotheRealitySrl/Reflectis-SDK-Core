using Reflectis.SDK.Core;
using System.Threading.Tasks;
using UnityEngine;
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
        /// Wheter or not this client is the master client
        /// </summary>
        public bool IsMasterClient { get; }

        public void Setup();

        /// <summary>
        /// Opens the event shard where the local user currently is. 
        /// </summary>
        public Task OpenCurrentShard();
        /// <summary>
        /// Closes the event shard where the local user currently is.
        /// </summary>
        public Task CloseCurrentShard();

        /// <summary>
        /// Setup spawnable object
        /// </summary>
        /// <param name="prefabKey"></param>
        /// <param name="loadedPrefab"></param>
        void SetupSpawnableObject(string prefabKey, GameObject loadedPrefab);

        /// <summary>
        /// Method used to spawn objects that were previously setup
        /// </summary>
        GameObject SpawnObject(string objectKey, Vector3 position, Quaternion rotation, byte group = 0, object[] data = null);
    }

}
