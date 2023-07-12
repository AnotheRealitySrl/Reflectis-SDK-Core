using Reflectis.SDK.Core;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reflectis.SDK.ClientModels
{
    public interface IClientModelSystem : ISystem
    {
        #region Events

        /// <summary>
        /// Returns the list of all events visible by user
        /// </summary>
        Task<List<Event>> GetAllEvents();

        /// <summary>
        /// Returns the list of all events visible by user filtered by category
        /// </summary>
        Task<List<Event>> GetAllEventsByCategoryID(int categoryId);

        /// <summary>
        /// Returns the list of users registered for this event.
        /// </summary>
        Task<List<User>> GetEventPartecipants(int eventId);

        Task<int> CreateUpdateEvent(Event e);

        #endregion

        #region Users

        /// <summary>
        /// Return all users that match search criteria
        /// </summary>
        Task<List<User>> GetUsers(string searchQuery);

        /// <summary>
        /// Return all user lists that match search criteria
        /// </summary>
        Task<UserList> GetUsersLists(string searchQuery);

        /// <summary>
        /// Return user list ID created (or updated)
        /// </summary>
        Task<int> CreateUpdateUserList(List<EventPermissionSet> permissions);

        #endregion

        #region Permissions

        Task<List<EventPermissionSet>> GetEventPermissionPreset();

        Task<int> CreateUpdateEventPermissionPreset();

        #endregion

        #region Assets

        Task<List<Resource>> GetMyAssets(string searchQuery);

        Task<List<Resource>> GetEventAssets(int eventId);

        Task CreateEventAssetsAssociation(int eventId, List<Resource> resources);

        #endregion

        #region Join events

        /// <summary>
        /// Requesto to join a specific ID. Return  the Event ID if request success otherwise return -1
        /// </summary>
        Task<int> JoinEventRequest(int eventId);

        #endregion
    }
}