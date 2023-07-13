using System;
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class Event
    {
        [SerializeField] private int id;
        [SerializeField] private string title;
        [SerializeField] private string longTitle;
        [SerializeField] private string description;
        [SerializeField] private Category category;
        [SerializeField] private Category subCategory;
        [SerializeField] private DateTime startDateTime;
        [SerializeField] private DateTime endDateTime;
        [SerializeField] private List<User> participants;
        [SerializeField] private int maxParticipants;
        [SerializeField] private Environment environment;
        [SerializeField] private bool isFeatured;
        [SerializeField] private string featuredThumb;
        [SerializeField] private bool isPublic;
        [SerializeField] private bool isVisible;
        [SerializeField] private List<EventPermissionSet> permissions;
        [SerializeField] private bool canJoin;
        [SerializeField] private bool isOwner;
        [SerializeField] private bool isDraft;
        [SerializeField] private List<Resource> resources;
        [SerializeField] private string shortLink;

        public int ID { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string LongTitle { get => longTitle; set => longTitle = value; }
        public string Description { get => description; set => description = value; }
        public Category Category { get => category; set => category = value; }
        public Category SubCategory { get => subCategory; set => subCategory = value; }
        public DateTime StartDateTime { get => startDateTime; set => startDateTime = value; }
        public DateTime EndDateTime { get => endDateTime; set => endDateTime = value; }
        public List<User> Participants { get => participants; set => participants = value; }
        public int MaxParticipants { get => maxParticipants; set => maxParticipants = value; }
        public Environment Environment { get => environment; set => environment = value; }
        public bool IsFeatured { get => isFeatured; set => isFeatured = value; }
        public string FeaturedThumb { get => featuredThumb; set => featuredThumb = value; }
        public bool IsPublic { get => isPublic; set => isPublic = value; }
        public bool IsVisible { get => isVisible; set => isVisible = value; }
        public List<EventPermissionSet> Permissions { get => permissions; set => permissions = value; }
        public bool CanJoin { get => canJoin; set => canJoin = value; }
        public bool IsOwner { get => isOwner; set => isOwner = value; }
        public bool IsDraft { get => isDraft; set => isDraft = value; }
        public List<Resource> Resources { get => resources; set => resources = value; }
        public string ShortLink { get => shortLink; set => shortLink = value; }
    }

}
