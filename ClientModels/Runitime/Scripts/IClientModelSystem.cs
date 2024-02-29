using Reflectis.SDK.Core;

using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine.Events;

namespace Reflectis.SDK.ClientModels
{
    public enum FileTypeExt
    {
        None = -1,
        Video = 1,
        Documents = 2,
        Images = 3,
        Asset3D = 4,
    }

    public interface IClientModelSystem : ISystem
    {
        #region Worlds

        CMWorld CurrentWorld { get; set; }

        /// <summary>
        /// Returns all the available worlds
        /// </summary>
        Task<List<CMWorld>> GetAllWorlds();

        #endregion

        #region Events

        CMEvent CurrentEvent { get; set; }
        CMEvent DefaultEvent { get; set; }
        List<CMEvent> StaticEvents { get; }

        /// <summary>
        /// Force refresh on cached event data
        /// </summary>
        /// <returns></returns>
        Task RefreshCachedData();

        /// <summary>
        /// Force refresh on cached event data
        /// </summary>
        /// <returns></returns>
        Task RefreshEventsData();

        /// <summary>
        /// Returns the default event of a world
        /// </summary>
        Task<CMEvent> GetDefaultWorldEvent(int worldId);

        /// <summary>
        /// Returns the static events
        /// </summary>
        Task<List<CMEvent>> GetStaticEvents();

        /// <summary>
        /// Returns an event given its id
        /// </summary>
        Task<CMEvent> GetEventById(int id, bool useCache = true);

        /// <summary>
        /// Returns the list of all events visible by user
        /// </summary>
        Task<List<CMEvent>> GetActiveEvents();

        /// <summary>
        /// Return the list events in which the player is also the owner
        /// </summary>
        /// <returns></returns>
        Task<List<CMEvent>> GetMyActiveEvents();

        /// <summary>
        /// Returns the list of all events visible by user filtered by category
        /// </summary>
        Task<List<CMEvent>> GetActiveEventsByCategoryID(int categoryId);

        /// <summary>
        /// Returns the list of all events visible by user filtered by environment
        /// </summary>
        Task<List<CMEvent>> GetActiveEventsByEnvironmentID(int environmentId);

        /// <summary>
        /// Returns the list of all events visible by user filtered by environment
        /// </summary>
        Task<List<CMEvent>> GetActiveEventsByEnvironmentName(string environmentName);

        /// <summary>
        /// Returns the list of users registered for this event.
        /// </summary>
        Task<List<CMUser>> GetEventParticipants(int eventId);

        /// <summary>
        /// Create an event with given e data.
        /// If successfull return the eventId, -1 otherwise
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Task<int> CreateEvent(CMEvent e);

        /// <summary>
        /// Delete an event with given id.
        /// If successfull return empty string, response reason phrase otherwise
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Task<string> DeleteEvent(int eventId);

        /// <summary>
        /// Update an event with given e data.
        /// If successfull return true, false otherwise
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Task<bool> UpdateEvent(CMEvent e);

        /// <summary>
        /// Ask to API to replace all the users in the specified event with the users listed in <see cref="CMEvent.Participants">
        /// </summary>
        /// <param name="cMEvent"></param>
        /// <returns></returns>
        Task<bool> InviteUsersToEvent(CMEvent cMEvent);

        /// <summary>
        /// Create all event permission for the given event
        /// </summary>
        /// <param name="_event"></param>
        /// <returns></returns>
        Task<bool> CreateEventPermissions(CMEvent _event);

        /// <summary>
        /// Request to join a specific ID. Return  the Event ID if request success otherwise return -1
        /// </summary>
        Task<bool> JoinEventRequest(int eventId);

        /// <summary>
        /// Add all asset list to the ones usable in the given event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="assets"></param>
        /// <returns></returns>
        Task<bool> ShareAssetsInEvent(int eventId, List<CMResource> assets);

        /// <summary>
        /// replace the asset list in the given event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="assets"></param>
        /// <returns></returns>
        Task<bool> UpdateAssetsInEvent(int eventId, List<CMResource> assets);

        /// <summary>
        /// load the asset list saved previously.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="assets"></param>
        /// <returns></returns>
        Task<bool> UpdateSavedAssets(int eventId, List<CMTemplateObj> assets);
        #endregion

        #region Categories

        Task<List<CMCategoryInfo>> GetAllEventCategoriesInfo();

        /// <summary>
        /// Return list of all categories
        /// </summary>
        /// <returns></returns>
        Task<List<CMCategory>> GetAllCategories();

        /// <summary>
        /// Return list of all subcategories
        /// </summary>
        /// <returns></returns>
        Task<List<CMCategory>> GetAllEventSubCategories();

        /// <summary>
        /// return list of all subcategories of a category
        /// </summary>
        /// <returns></returns>
        Task<List<CMCategory>> GetEventSubCategoriesOfCategory(CMCategory parentCategory);

        #endregion

        #region Environments

        Task<List<CMEnvironment>> GetAllEnvironments();

        #endregion

        #region Users

        CMUser UserData { get; set; }

        /// <summary>
        /// Return all users that match search criteria
        /// </summary>
        Task<List<CMUser>> SearchUsersByNickname(string nicknameSubstring);
        /// <summary>
        /// Return the local player data
        /// </summary>
        /// <returns></returns>
        Task<CMUser> GetPlayerData();

        /// <summary>
        /// Return data of the player with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CMUser> GetUserData(int id);

        Task<CMUserPreference> GetUserPreference(int userId);
        /// <summary>
        /// Update user preferences
        /// if fails returns error string, null if successfull
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task<string> UpdateUserPreference(CMUserPreference newPreferences);

        /// <summary>
        /// Get all the users with given tag id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<CMUser>> GetUsersWithTag(int id);
        #endregion

        #region Facets

        public List<CMFacet> WorldFacets { get; set; }

        /// <summary>
        /// Get all facets of the current world
        /// </summary>
        /// <param name="worldId"></param>
        /// <returns></returns>
        Task<List<CMFacet>> GetWorldFacets(int worldId);

        public bool IsEventPermissionGranted(EFacetIdentifier identifier);
        public bool IsWorldPermissionGranted(EFacetIdentifier identifier);

        #endregion

        #region Permissions

        List<CMPermission> MyEventPermissions { get; set; }
        List<CMPermission> WorldPermissions { get; set; }

        /// <summary>
        /// Get the permission avaible to the player for the given event
        /// </summary>
        /// <returns></returns>
        Task<List<CMPermission>> GetMyEventPermissions(int eventId);

        /// <summary>
        /// Get the permission avaible in the current event
        /// </summary>
        /// <returns></returns>
        Task<List<CMPermission>> GetEventPermissions(int eventId);

        /// <summary>
        /// Get all permission for the current world
        /// </summary>
        /// <returns></returns>
        Task<List<CMPermission>> GetWorldPermissions(int worldId);


        #endregion

        #region Assets

        Task<CMResource> GetEventAssetById(int assetId);

        Task<List<CMResource>> GetMyAssets(string searchQuery);

        Task<CMSearch<CMFolder>> GetEventAssetsFolders(int eventId, int pageSize, int page = 1, IEnumerable<FileTypeExt> fileTypes = null);

        Task<CMSearch<CMResource>> GetEventAssetsInFolder(int eventId, string path, int pageSize, int page = 1, IEnumerable<FileTypeExt> fileTypes = null);

        Task CreateEventAssetsAssociation(int eventId, List<CMResource> resources);

        #endregion

        #region Tags
        /// <summary>
        /// Get all tags avaible to users
        /// </summary>
        /// <returns></returns>
        Task<List<CMTag>> GetAllUsersTags();

        /// <summary>
        /// Search user tag
        /// </summary>
        /// <param name="labelSubstring"></param>
        /// <returns></returns>
        Task<List<CMTag>> SearchUserTags(string labelSubstring);

        /// <summary>
        /// Search env tag
        /// </summary>
        /// <param name="labelSubstring"></param>
        /// <returns></returns>
        Task<List<CMTag>> SearchEnvironmentTags(string labelSubstring);



        #endregion

        #region Online presence

        List<CMOnlinePresence> OnlineUsersList { get; set; }
        UnityEvent OnlineUsersUpdated { get; }
        CMOnlinePresence FindOnlineUser(int userId);
        string FindOnlineUserDisplayName(int userId);
        string FindOnlineUserAvatarPng(int userId);
        int FindOnlineUserShard(int userId);
        int FindOnlineUserEvent(int userId);
        CMOnlinePresence.Platform FindOnlineUserPlatform(int userId);
        bool IsOnlineUser(int userId);
        Task<List<CMOnlinePresence>> GetOnlineUsers(bool includeMyself = true);
        Task PingMyOnlinePresence(int? eventId, int? shardId);

        Task<List<CMOnlinePresence>> GetUsersInEvent(int eventId, bool forceRefresh = true);

        #endregion

        #region Shards

        int MaxShardCapacity { get; set; }
        CMShard CurrentShard { get; set; }

        /// <summary>
        /// Retrieves the current shards of an event.
        /// </summary>
        Task<List<CMShard>> GetEventShards(int eventId);

        /// <summary>
        /// Wheter or not the shard is full
        /// </summary>
        /// <param name="shard"></param>
        /// <returns></returns>
        Task<bool> IsShardFull(CMShard shard);

        #endregion
    }
}