using Reflectis.SDK.DataAccess;
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
        [SerializeField] private string addressableKey;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
        public string AddressableKey { get => addressableKey; set => addressableKey = value; }

        public CMEnvironment()
        {
        }
        public CMEnvironment(EnvironmentDTO environment)
        {
            this.ID = environment.Id;
            this.Name = environment.Label;
        }
    }
}
