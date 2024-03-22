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
        [SerializeField] private Texture imageTexture;
        [SerializeField] private string addressableKey;
        [SerializeField] private string catalog;
        [SerializeField] private int worldId;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
        public Texture ImageTexture { get => imageTexture; set => imageTexture = value; }
        public string AddressableKey { get => addressableKey; set => addressableKey = value; }
        public string Catalog { get => catalog; set => catalog = value; }
        public int WorldId { get => worldId; set => worldId = value; }
    }
}
