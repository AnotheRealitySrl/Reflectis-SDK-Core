using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMWorld
    {
        [SerializeField] private int id;
        [SerializeField] private string label;
        [SerializeField] private string description;
        [SerializeField] private bool enabled;

        public int Id { get => id; set => id = value; }
        public string Label { get => label; set => label = value; }
        public string Description { get => description; set => description = value; }
        public bool Enabled { get => enabled; set => enabled = value; }
    }
}
