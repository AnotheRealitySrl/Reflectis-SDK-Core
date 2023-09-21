using Reflectis.SDK.Core;

using System.Collections.Generic;
using System.Threading.Tasks;

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

        Task<List<CMWorld>> GetAllWorlds();

        #endregion

        #region Events

        /// <summary>
        /// Returns the default event of a world
        /// </summary>
        Task<CMEvent> GetDefaultWorldEvent(int worldId);

        /// <summary>
        /// Returns an event given its id
        /// </summary>
        Task<CMEvent> GetEventById(int id);

        /// <summary>
        /// Returns the list of all events visible by user
        /// </summary>
        Task<List<CMEvent>> GetAllEvents();

        /// <summary>
        /// Returns the list of all events visible by user filtered by category
        /// </summary>
        Task<List<CMEvent>> GetAllEventsByCategoryID(int categoryId);

        /// <summary>
        /// Returns the list of all events visible by user filtered by environment
        /// </summary>
        Task<List<CMEvent>> GetAllEventsByEnvironmentID(int environmentId);

        /// <summary>
        /// Returns the list of all events visible by user filtered by environment
        /// </summary>
        Task<List<CMEvent>> GetAllEventsByTagID(int tagId);

        /// <summary>
        /// Returns the list of users registered for this event.
        /// </summary>
        Task<List<CMUser>> GetEventParticipants(int eventId);

        /// <summary>
        /// Create an event with given e data.
        /// If successfull return the event, null otherwise
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Task<CMEvent> CreateEvent(CMEvent e);

        /// <summary>
        /// Ask to API to replace all the users in the specified event with the users listed in <see cref="CMEvent.Participants">
        /// </summary>
        /// <param name="cMEvent"></param>
        /// <returns></returns>
        Task<int> InviteUsersToEvent(CMEvent cMEvent);


        /// <summary>
        /// Create all event permission for the given event
        /// </summary>
        /// <param name="_event"></param>
        /// <returns></returns>
        Task<int> CreateEventPermissions(CMEvent _event);

        /// <summary>
        /// Request to join a specific ID. Return  the Event ID if request success otherwise return -1
        /// </summary>
        Task<int> JoinEventRequest(int eventId);

        /// <summary>
        /// Retrieves the current shards of an event.
        /// </summary>
        Task<List<CMShard>> GetEventShards(int eventId);

        #endregion

        #region Categories

        Task<List<CMCategoryInfo>> GetAllEventInfo();

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

        #region Environment

        Task<List<CMEnvironment>> GetAllEnvironments();

        #endregion

        #region Users

        /// <summary>
        /// Return all users that match search criteria
        /// </summary>
        Task<List<CMUser>> SearchUsersByNickname(string nicknameSubstring);

        Task<CMUser> GetUserData();

        Task<CMUserPreference> GetUserPreference(int userId);
        /// <summary>
        /// Get all the users with given tag id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<CMUser>> GetUsersWithTag(int id);
        #endregion

        #region Permissions
        /// <summary>
        /// Get all facets of the current world
        /// </summary>
        /// <param name="worldId"></param>
        /// <returns></returns>
        Task<List<CMFacet>> GetWorldFacets(int worldId);

        /// <summary>
        /// Get the permission avaible to the player
        /// </summary>
        /// <returns></returns>
        Task<List<CMPermission>> GetMyEventPermissions(int eventId);

        /// <summary>
        /// Get all permission for the current world
        /// </summary>
        /// <returns></returns>
        Task<List<CMPermission>> GetWorldPermissions(int worldId);


        #endregion

        #region Assets

        Task<CMResource> GetEventAssetById(int eventId, int assetId);

        Task<List<CMResource>> GetMyAssets(string searchQuery);

        Task<List<CMResource>> GetEventAssets(int eventId);

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

        #region Join events

        #endregion
    }
}