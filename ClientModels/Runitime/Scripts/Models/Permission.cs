using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class Permission
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private bool isEnabled;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public bool IsEnabled { get => isEnabled; set => isEnabled = value; }
    }
}
