using System;

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
        [SerializeField] private string addressableKey;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
        public string AddressableKey { get => addressableKey; set => addressableKey = value; }
    }
}
