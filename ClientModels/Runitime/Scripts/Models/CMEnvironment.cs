using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMEnvironment
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private string imageUrl;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
    }
}
