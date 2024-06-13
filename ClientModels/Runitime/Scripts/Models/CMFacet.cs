using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    public enum EFacetIdentifier
    {
        Unknown = 0,
        MuteOthers = 1,
        KickOthers = 2,
        WriteTool = 3,
        LaserPointerTool = 4,
        BoardTool = 5,
        WebViewTool = 6,
        Asset3DSpawn = 7,
        VideoSpawn = 8,
        ImageSpawn = 9,
        DocSpawn = 10,
        MuteAll = 11,
        SendGlobalMessage = 12,
        SendAnnouncement = 13,
        SendShardMessage = 14,
        CreateEvent = 15,
        EnableSpeaker = 16,
        AuthoringTool = 17,
        AssetControlManager = 18,
    }

    public enum EFacetGroup
    {
        Unknown = 0,
        Management = 1,
        Actions = 2,
        Tools = 3,
        System = 4,
    }

    public enum EFacetStatus
    {
        Unknown = 0,
        Enabled = 1,
        Disabled = 2
    }

    [Serializable]
    public class CMFacet
    {
        [SerializeField] private int id;
        [SerializeField] private DateTime creationDate;
        [SerializeField] private DateTime lastUpdate;
        [SerializeField] private EFacetIdentifier identifier;
        [SerializeField] private string label;
        [SerializeField] private string note;
        [SerializeField] private EFacetGroup facetGroup;
        [SerializeField] private EFacetStatus status;
        [SerializeField] private string[] requiredRoles;

        public int Id { get => id; set => id = value; }

        public EFacetIdentifier Identifier { get => identifier; set => identifier = value; }

        public string Label { get => label; set => label = value; }

        public string Note { get => note; set => note = value; }

        public DateTime CreationDate { get => creationDate; set => creationDate = value; }

        public DateTime LastUpdate { get => lastUpdate; set => lastUpdate = value; }

        public EFacetGroup FacetGroup { get => facetGroup; set => facetGroup = value; }

        public EFacetStatus Status { get => status; set => status = value; }

        public string[] RequiredRoles { get => requiredRoles; set => requiredRoles = value; }
    }
}
