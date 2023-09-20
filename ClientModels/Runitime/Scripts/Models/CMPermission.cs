using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMPermission
    {
        [SerializeField] private int id;
        [SerializeField] private CMFacet facet;
        [SerializeField] private CMTag tag;
        [SerializeField] private bool isEnabled;

        public int Id { get => id; set => id = value; }
        public CMFacet Facet { get => facet; set => facet = value; }
        public bool IsEnabled { get => isEnabled; set => isEnabled = value; }
        public CMTag Tag { get => tag; set => tag = value; }
    }
}
