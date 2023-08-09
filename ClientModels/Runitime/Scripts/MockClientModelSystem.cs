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

        [SerializeField, Multiline(10)] private string mockGetEventByIdResponse;
        public async Task<CMEvent> GetEventById(int id)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<CMEvent>(mockGetEventByIdResponse));
        }

        [SerializeField, Multiline(10)] private string mockGetAllEventsResponse;
        public async Task<List<CMEvent>> GetAllEvents()
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<CMEvent>>(mockGetAllEventsResponse));
        }

        [SerializeField, Multiline(10)] private string mockGetAllEventsByCategoryIDResponse;
        public async Task<List<CMEvent>> GetAllEventsByCategoryID(int categoryId)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<CMEvent>>(mockGetAllEventsByCategoryIDResponse));
        }

        [SerializeField, Multiline(10)] private string mockGetEventPartecipantsResponse;
        public async Task<List<CMUser>> GetEventPartecipants(int eventId)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<CMUser>>(mockGetEventPartecipantsResponse));
        }

        [SerializeField] private int mockCreateUpdateEventResponse;
        public async Task<int> CreateUpdateEvent(CMEvent e)
        {
            return await Task.FromResult(mockCreateUpdateEventResponse);
        }

        #endregion

        #region Categories

        [SerializeField, Multiline(10)] private string mockGetAllEventCategories;
        /// <summary>
        /// Return list of all categories
        /// </summary>
        /// <returns></returns>
        public async Task<List<CMCategory>> GetAllEventCategories()
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<CMCategory>>(mockGetAllEventCategories));
        }


        [SerializeField, Multiline(10)] private string mockGetAllEventSubCategories;
        /// <summary>
        /// Return list of all subcategories
        /// </summary>
        /// <returns></returns>
        public async Task<List<CMCategory>> GetAllEventSubCategories()
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<CMCategory>>(mockGetAllEventSubCategories));
        }


        [SerializeField, Multiline(10)] private string mockGetEventSubCategoriesOfCategory;
        /// <summary>
        /// return list of all subcategories of a category
        /// </summary>
        /// <returns></returns>
        public async Task<List<CMCategory>> GetEventSubCategoriesOfCategory(CMCategory parentCategory)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<CMCategory>>(mockGetEventSubCategoriesOfCategory));
        }

        #endregion

        #region Users

        [SerializeField, Multiline(10)] private string mockGetUsersResponse;
        public async Task<List<CMUser>> GetUsers(string searchQuery)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<CMUser>>(mockGetUsersResponse));
        }

        [SerializeField, Multiline(10)] private string mockGetUsersListsResponse;
        public async Task<List<CMUserList>> GetUsersLists(string searchQuery)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<CMUserList>>(mockGetUsersListsResponse));
        }

        [SerializeField] private int mockCreateUpdateUserListResponse;
        public async Task<int> CreateUpdateUserList(List<CMEventPermissionSet> permissions)
        {
            return await Task.FromResult(mockCreateUpdateUserListResponse);
        }

        #endregion

        #region Permissions 

        [SerializeField, Multiline(10)] private string mockGetEventPermissionPresetResponse;
        public async Task<List<CMEventPermissionSet>> GetEventPermissionPreset()
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<CMEventPermissionSet>>(mockGetEventPermissionPresetResponse));
        }

        [SerializeField] private int mockCreateUpdateEventPermissionPresetResponse;
        public async Task<int> CreateUpdateEventPermissionPreset()
        {
            return await Task.FromResult(mockCreateUpdateEventPermissionPresetResponse);
        }

        #endregion

        #region Assets

        [SerializeField, Multiline(10)] private string mockGetAssetByIdResponse;
        public async Task<CMResource> GetEventAssetById(int eventId, int assetId)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<CMResource>(mockGetAssetByIdResponse));
        }

        [SerializeField, Multiline(10)] private string mockGetMyAssetsResponse;
        public async Task<List<CMResource>> GetMyAssets(string searchQuery)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<CMResource>>(mockGetMyAssetsResponse));
        }

        [SerializeField, Multiline(10)] private string mockGetEventAssetsResponse;
        public async Task<List<CMResource>> GetEventAssets(int eventId)
        {
            return await Task.FromResult(JsonConvert.DeserializeObject<List<CMResource>>(mockGetEventAssetsResponse));
        }

        public Task CreateEventAssetsAssociation(int eventId, List<CMResource> resources)
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

        public Task<List<CMCategory>> GetEventCategories()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CMResource>> GetMyAssets(string searchQuery, int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<CMUser> GetUserData()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
