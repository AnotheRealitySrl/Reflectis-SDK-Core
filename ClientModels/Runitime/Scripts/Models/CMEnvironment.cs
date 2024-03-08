using System;
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMEnvironment
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private string imageUrl;
        [SerializeField] private Texture imageTexture;
        [SerializeField] private string addressableKey;
        [SerializeField] private string catalog;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
        public Texture ImageTexture { get => imageTexture; set => imageTexture = value; }
        public string AddressableKey { get => addressableKey; set => addressableKey = value; }
        public string Catalog { get => catalog; set => catalog = value; }
    }

    public class CMEnvironmentNameComparerer : IEqualityComparer<CMEnvironment>
    {
        public bool Equals(CMEnvironment x, CMEnvironment y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(CMEnvironment obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
