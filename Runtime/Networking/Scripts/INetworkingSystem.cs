using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.Core.NetworkingSystem
{
    public interface INetworkingSystem : ISystem
    {

        /// <summary>
        /// Event called upon disconnection. Argument disconnection cause.
        /// </summary>
        public UnityEvent<string> OnDisconnection { get; }
        /// <summary>
        /// int userID, int playerId 
        /// userID is the id of the user in thereflectis ecosystem
        /// playerId is the id of the player inside the network session
        /// </summary>
        public UnityEvent<NetworkPlayerData> OnOtherPlayerJoinShard { get; }
        /// <summary>
        /// int userID, int playerId
        /// userID is the id of the user in thereflectis ecosystem
        /// playerId is the id of the player inside the network session
        /// </summary>
        public UnityEvent<NetworkPlayerData> OnOtherPlayerLeaveShard { get; }

        /// <summary>
        /// Returns true if the networking manager is currently connected to a network shard
        /// </summary>
        public bool ConnectedToShard { get; }

        public bool IsConnected { get; }

        public bool IsDisconnected { get; }

        /// <summary>
        /// Returns true if the shard where the local user currently is is open. 
        /// </summary>
        public bool IsCurrentShardOpen { get; }

        /// <summary>
        /// Wheter or not this client is the master client
        /// </summary>
        public bool IsMasterClient { get; }

        /// <summary>
        /// Number of players in the session or in the last session the player was connected to
        /// </summary>
        int CachedPlayerCount { get; }

        /// <summary>
        /// Number of players currently in this session
        /// </summary>
        int PlayerCount { get; }

        /// <summary>
        /// Current unique session name
        /// </summary>
        string CurrentShardName { get; }

        /// <summary>
        /// Write local player network data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<bool> WritePlayerData(GameObject avatarGameObject);

        /// <summary>
        /// Connect to networking service using key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> ConnectToService(string key);

        /// <summary>
        /// Disconnect from networking service
        /// </summary>
        /// <returns></returns>
        public void DisconnectFromService();

        /// <summary>
        /// Connect to networking server
        /// </summary>
        /// <returns></returns>
        public Task<NetworkStateAsyncOperation> ConnectToServerAsync();

        /// <summary>
        /// Connect to networking server
        /// </summary>
        /// <returns></returns>
        public Task<NetworkStateAsyncOperation> ConnectToShardAsync(string shardName, int maxCapacity);

        /// <summary>
        /// Disconnect from network server
        /// </summary>
        /// <param name="reconnect"></param>
        /// <returns></returns>
        Task DisconnectFromServerAsync(bool reconnect);

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

        /// <summary>
        /// Object to destroy
        /// </summary>
        /// <param name="objectToDestroy"></param>
        void NetworkDestroy(GameObject objectToDestroy);

        /// <summary>
        /// Disconnect from room
        /// </summary>
        Task LeaveRoomAsync();

        /// <summary>
        /// Disconnect from network server
        /// </summary>
        /// <param name="tryToReconnect"></param>
        void DisconnectFromServer(bool tryToReconnect);
        /// <summary>
        /// Get local player Id
        /// </summary>
        /// <returns></returns>
        int GetLocalPlayerId();

        /// <summary>
        /// Get list of all players data
        /// </summary>
        /// <returns></returns>
        IEnumerable<NetworkPlayerData> GetPlayersList();


        /// <summary>
        /// Get player avatar gameobject
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        GameObject GetPlayerAvatarObject(int playerId);

        /// <summary>
        /// Method used for player removal inside the current networked shard
        /// </summary>
        /// <param name="playerId"></param>
        void KickPlayer(int playerId);
    }

}
