using Reflectis.SDK.DataAccess;
using Reflectis.SDK.Utilities.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMEvent
    {
        [SerializeField] private int id;
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private CMCategory category;
        [SerializeField] private CMCategory subCategory;
        [SerializeField] private DateTime startDateTime;
        [SerializeField] private DateTime endDateTime;
        [SerializeField] private List<CMUser> participants;
        [SerializeField] private int maxParticipants;
        [SerializeField] private CMEnvironment environment;
        [SerializeField] private bool isFeatured;
        [SerializeField] private string featuredThumb;
        [SerializeField] private bool isPublic;
        [SerializeField] private bool isVisible;
        [SerializeField] private List<CMEventPermissionSet> permissions;
        [SerializeField] private bool isOwner;
        [SerializeField] private bool isDraft;
        [SerializeField] private List<CMResource> resources;
        [SerializeField] private string shortLink;

        public int ID { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public CMCategory Category { get => category; set => category = value; }
        public CMCategory SubCategory { get => subCategory; set => subCategory = value; }
        public DateTime StartDateTime { get => startDateTime; set => startDateTime = value; }
        public DateTime EndDateTime { get => endDateTime; set => endDateTime = value; }
        public List<CMUser> Participants { get => participants; set => participants = value; }
        public int MaxParticipants { get => maxParticipants; set => maxParticipants = value; }
        public CMEnvironment Environment { get => environment; set => environment = value; }
        public bool IsFeatured { get => isFeatured; set => isFeatured = value; }
        public string FeaturedThumb { get => featuredThumb; set => featuredThumb = value; }
        public bool IsPublic { get => isPublic; set => isPublic = value; }
        public bool IsVisible { get => isVisible; set => isVisible = value; }
        public List<CMEventPermissionSet> Permissions { get => permissions; set => permissions = value; }
        public bool IsOwner { get => isOwner; set => isOwner = value; }
        public bool IsDraft { get => isDraft; set => isDraft = value; }
        public List<CMResource> Resources { get => resources; set => resources = value; }
        public string ShortLink { get => shortLink; set => shortLink = value; }

        async public static Task<CMEvent> BuildCMEvent(EventDTO eventDTO, ReflectisDataAccessSystem dataAccessSystem)
        {
            ApiResponse<SubcategoryDTO> subcategoryDTO = await dataAccessSystem.GetSubcategory(eventDTO.SubcategoryId);
            ApiResponse<CategoryDTO> categoryDTO = subcategoryDTO.Content == null ? null : await dataAccessSystem.GetCategory(subcategoryDTO.Content.Id);
            ApiResponse<EnvironmentDTO> environmentDTO = await dataAccessSystem.GetEnvironment(eventDTO.EnvironmentId);
            return new CMEvent(eventDTO, categoryDTO.Content, subcategoryDTO.Content, environmentDTO.Content);
        }
        public NewEvent BuildEventCTO()
        {
            NewEvent eventDTO = new()
            {
                Label = this.title,
                Description = this.description,
                Status = EStatusOption.Enabled,
                VisibleByAnonymous = this.isVisible,
                VisibleByMembers = true,
                VisibilityOption = this.isVisible ? EVisibilityOption.Open : EVisibilityOption.MembersOnly,
                SubcategoryId = this.subCategory.ID,
                Spotlight = this.isFeatured,
                EnvironmentId = this.environment.ID,
                StartDate = this.startDateTime,
                EndDate = this.endDateTime,
                Capacity = this.maxParticipants,
                //Template = template,
                //OrderNumber = orderNumber,
                Multiplayer = this.isPublic
                //StartingPoint = startingPoint
            };
            return eventDTO;
        }
        public CMEvent()
        {
        }
        public CMEvent(EventDTO eventDTO, CategoryDTO category, SubcategoryDTO subcategory, EnvironmentDTO environment)
        {
            this.ID = eventDTO.Id;
            this.title = eventDTO.Label;
            this.description = eventDTO.Description;
            this.category = category == null ? null : new CMCategory(category);
            this.subCategory = subcategory == null ? null : new CMCategory(subcategory, category.Id);
            this.startDateTime = eventDTO.StartDate;
            this.endDateTime = eventDTO.EndDate;
            //this.participants = new List<CMUser>();
            this.maxParticipants = eventDTO.Capacity;
            this.environment = environment == null ? null : new CMEnvironment(environment);
            this.isFeatured = eventDTO.Spotlight;
            //this.featuredThumb = featuredThumb;
            this.isPublic = eventDTO.Multiplayer;
            this.isVisible = eventDTO.VisibleByAnonymous;
            //this.permissions = new List<CMEventPermissionSet>; 
            //this.isOwner = isOwner;
            this.isDraft = eventDTO.Status == EStatusOption.Enabled;
            //this.resources = resources;
            //this.shortLink = shortLink;
        }
    }

}
