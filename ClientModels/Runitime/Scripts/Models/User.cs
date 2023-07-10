using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class User
    {
        [SerializeField] private int id;
        [SerializeField] private string displayName;

        public int ID { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
    }

}
