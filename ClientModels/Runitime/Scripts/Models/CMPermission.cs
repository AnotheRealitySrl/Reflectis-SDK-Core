using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMPermission
    {
        [SerializeField] private int id;
        [SerializeField] private int facetId;
        [SerializeField] private bool isEnabled;

        public int ID { get => id; set => id = value; }
        public int FacetId { get => facetId; set => facetId = value; }
        public bool IsEnabled { get => isEnabled; set => isEnabled = value; }
    }
}
