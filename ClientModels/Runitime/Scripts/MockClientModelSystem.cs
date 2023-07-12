using Newtonsoft.Json;

using Reflectis.SDK.Core;

using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [CreateAssetMenu(menuName = "Reflectis/SDK-ClientModels/MockClientModelSystem", fileName = "MockClientModelSystemConfig")]
    public class MockClientModelSystem : BaseSystem, IClientModelSystem
    {
        public override void Init()
        {
            // Nothing to do
        }

        #region Events

        [SerializeField, Multiline(10)] private string mockGetAllEventsResponse;
        public async Task<List<Event>> GetAllEvents()
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<Event>>(mockGetAllEventsResponse));
        }

        [SerializeField, Multiline(10)] private string mockGetAllEventsByCategoryIDResponse;
        public async Task<List<Event>> GetAllEventsByCategoryID(int categoryId)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<Event>>(mockGetAllEventsByCategoryIDResponse));
        }

        [SerializeField, Multiline(10)] private string mockGetEventPartecipantsResponse;
        public async Task<List<User>> GetEventPartecipants(int eventId)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<User>>(mockGetEventPartecipantsResponse));
        }

        [SerializeField] private int mockCreateUpdateEventResponse;
        public async Task<int> CreateUpdateEvent(Event e)
        {
            return await Task.FromResult(mockCreateUpdateEventResponse);
        }

        #endregion

        #region Users

        [SerializeField, Multiline(10)] private string mockGetUsersResponse;
        public async Task<List<User>> GetUsers(string searchQuery)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<User>>(mockGetUsersResponse));
        }

        [SerializeField, Multiline(10)] private string mockGetUsersListsResponse;
        public async Task<UserList> GetUsersLists(string searchQuery)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<UserList>(mockGetUsersListsResponse));
        }

        [SerializeField] private int mockCreateUpdateUserListResponse;
        public async Task<int> CreateUpdateUserList(List<EventPermissionSet> permissions)
        {
            return await Task.FromResult(mockCreateUpdateUserListResponse);
        }

        #endregion

        #region Permissions 

        [SerializeField, Multiline(10)] private string mockGetEventPermissionPresetResponse;
        public async Task<List<EventPermissionSet>> GetEventPermissionPreset()
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<EventPermissionSet>>(mockGetEventPermissionPresetResponse));
        }

        [SerializeField] private int mockCreateUpdateEventPermissionPresetResponse;
        public async Task<int> CreateUpdateEventPermissionPreset()
        {
            return await Task.FromResult(mockCreateUpdateEventPermissionPresetResponse);
        }

        #endregion

        #region Assets

        [SerializeField, Multiline(10)] private string mockGetMyAssetsResponse;
        public async Task<List<Resource>> GetMyAssets(string searchQuery)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<Resource>>(mockGetMyAssetsResponse));
        }

        [SerializeField, Multiline(10)] private string mockGetEventAssetsResponse;
        public async Task<List<Resource>> GetEventAssets(int eventId)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<Resource>>(mockGetEventAssetsResponse));
        }

        public Task CreateEventAssetsAssociation(int eventId, List<Resource> resources)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Join events

        [SerializeField] private int mockJoinEventRequestResponse;
        public async Task<int> JoinEventRequest(int eventId)
        {
            return await Task.FromResult(mockJoinEventRequestResponse);
        }

        #endregion
    }
}
