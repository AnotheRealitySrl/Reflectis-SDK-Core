using Reflectis.SDK.Core;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reflectis.SDK.ClientModels
{
    public interface IClientModelSystem : ISystem
    {
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
        /// Returns the list of users registered for this event.
        /// </summary>
        Task<List<CMUser>> GetEventPartecipants(int eventId);

        Task<int> CreateUpdateEvent(CMEvent e);

        #endregion

        #region Categories

        /// <summary>
        /// Return list of all categories
        /// </summary>
        /// <returns></returns>
        Task<List<CMCategory>> GetAllEventCategories();

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

        Task<List<CMEnvironment>> GetAllEnvironment();

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

        #region Join events

        /// <summary>
        /// Requesto to join a specific ID. Return  the Event ID if request success otherwise return -1
        /// </summary>
        Task<int> JoinEventRequest(int eventId);

        #endregion
    }
}