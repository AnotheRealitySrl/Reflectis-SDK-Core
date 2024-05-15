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
        void InvalidateCache();

        #region Worlds

        /// <summary>
        /// Returns all the available worlds
        /// </summary>
        Task<List<CMWorld>> GetAllWorlds();

        Task<CMWorld> GetWorld(int worldId);

        Task<List<CMCatalog>> GetWorldCatalogs(int worldId);

        #endregion

        #region Events

        /// <summary>
        /// Force refresh on cached event data
        /// Refreshes also cached data that usually should not be refreshed (categories and environments)
        /// </summary>
        /// <returns></returns>
        Task RefreshAllCachedEventsData();

        /// <summary>
        /// Force refresh on cached event data.
        /// Refreshes only data that has a refresh expiring time
        /// </summary>
        /// <returns></returns>
        Task RefreshEventsData();

        /// <summary>
        /// If value the cache variables that have to be auto refreshed will start their refresh
        /// otherwise they will stop refreshing
        /// </summary>
        /// <param name="value"></param>
        Task EnableCacheAutoRefresh(bool value);

        /// <summary>
        /// Returns the default event of a world
        /// </summary>
        Task<CMEvent> GetDefaultWorldEvent();

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
        Task<long> DeleteEvent(int eventId);

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
        Task<bool> InviteUsersToEvent(CMEvent cMEvent, string eventInvitationMessage);

        /// <summary>
        /// Create all event permission for the given event
        /// </summary>
        /// <param name="_event"></param>
        /// <returns></returns>
        Task<bool> CreateEventPermissions(CMEvent _event);

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

        Task<List<CMCategoryInfo>> GetCategoriesInfo();

        /// <summary>
        /// Return list of all categories
        /// </summary>
        /// <returns></returns>
        Task<List<CMCategory>> GetCategories();

        /// <summary>
        /// Return list of all subcategories
        /// </summary>
        /// <returns></returns>
        Task<List<CMCategory>> GetSubCategories();

        /// <summary>
        /// return list of all subcategories of a category
        /// </summary>
        /// <returns></returns>
        Task<List<CMCategory>> GetSubCategories(CMCategory parentCategory);

        #endregion

        #region Environments

        Task<List<CMEnvironment>> GetEnvironments();

        #endregion

        #region Users

        /// <summary>
        /// Return all users that match search criteria
        /// </summary>
        Task<List<CMUser>> SearchUsersByNickname(string nicknameSubstring);

        /// <summary>
        /// Return data of the player with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CMUser> GetUserData(int id);

        /// <summary>
        ///  Return the local player data
        /// </summary>
        /// <returns></returns>
        Task<CMUser> GetMyUserData();

        /// <summary>
        /// Return data of the player with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> GetUserCode(int userId);

        Task<CMUserPreference> GetUserPreference(int userId);
        /// <summary>
        /// Update user preferences
        /// if fails returns error string, null if successfull
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task UpdateUserPreference(CMUserPreference newPreferences);

        /// <summary>
        /// Get all the users with given tag id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<CMUser>> GetUsersWithTag(int id);
        #endregion

        #region Facets

        /// <summary>
        /// Get all facets of the current world
        /// </summary>
        /// <param name="worldId"></param>
        /// <returns></returns>
        Task<List<CMFacet>> GetWorldFacets(int worldId);

        #endregion

        #region Permissions

        public bool IsEventPermissionGranted(EFacetIdentifier identifier);
        public bool IsWorldPermissionGranted(EFacetIdentifier identifier);

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

        #region Keys

        Task<bool> CheckMyKeys();

        #endregion

        #region Schedule

        Task<bool> CheckScheduleAccessibilityForToday();

        #endregion

        #region Assets

        Task<CMResource> GetEventAssetById(int assetId);

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
        /// Get all tags avaible to the single user given the user id
        /// </summary>
        /// <returns></returns>
        Task<List<CMTag>> GetUserTags(int id);

        /// <summary>
        /// Search user tag
        /// </summary>
        /// <param name="labelSubstring"></param>
        /// <returns></returns>
        Task<List<CMTag>> SearchUserTags(string labelSubstring);

        #endregion

        #region Online presence
        UnityEvent OnlineUsersUpdated { get; }
        Task<List<CMOnlinePresence>> GetOnlineUsers(bool forceRefresh = false);
        CMOnlinePresence GetOnlineUser(int userId);
        bool IsOnlineUser(int userId);
        Task PingMyOnlinePresence(int? worldId, int? eventId, int? shardId);
        Task<List<CMOnlinePresence>> GetUsersInEvent(int eventId, bool forceRefresh = true);

        #endregion

        #region Shards

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