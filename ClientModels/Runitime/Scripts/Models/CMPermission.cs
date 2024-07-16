using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMPermission
    {
        [SerializeField] private int tagId;
        [SerializeField] private int facetId;
        [SerializeField] private string facetLabel;
        [SerializeField] private EFacetIdentifier facetIdentifier;
        [SerializeField] private string facetGroup;
        [SerializeField] private bool overridableInEvent;
        [SerializeField] private bool isEnabled;

        public int TagId { get => tagId; set => tagId = value; }
        public int FacetId { get => facetId; set => facetId = value; }
        public string FacetLabel { get => facetLabel; set => facetLabel = value; }
        public EFacetIdentifier FacetIdentifier { get => facetIdentifier; set => facetIdentifier = value; }
        public string FacetGroup { get => facetGroup; set => facetGroup = value; }
        public bool OverridableInEvent { get => overridableInEvent; set => overridableInEvent = value; }
        public bool IsEnabled { get => isEnabled; set => isEnabled = value; }
    }
}
