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
        /// Ask to API to replace all the users in the specified event with the users listed in cMEvent.Partecipants
        /// </summary>
        /// <param name="cMEvent"></param>
        /// <returns></returns>
        Task<int> InviteUsersToEvent(CMEvent cMEvent);

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
        Task<List<CMUser>> GetUsers(string searchQuery);

        /// <summary>
        /// Return all user lists that match search criteria
        /// </summary>
        Task<List<CMUserList>> GetUsersLists(string searchQuery);

        /// <summary>
        /// Return user list ID created (or updated)
        /// </summary>
        Task<int> CreateUpdateUserList(List<CMEventPermissionSet> permissions);

        Task<CMUser> GetUserData();

        Task<CMUserPreference> GetUserPreference(int userId);

        #endregion

        #region Permissions

        Task<List<CMEventPermissionSet>> GetEventPermissionPreset();

        Task<int> CreateUpdateEventPermissionPreset();

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
        Task<List<CMTag>> GetUsersTags();

        #endregion

        #region Join events

        /// <summary>
        /// Requesto to join a specific ID. Return  the Event ID if request success otherwise return -1
        /// </summary>
        Task<int> JoinEventRequest(int eventId);

        #endregion
    }
}