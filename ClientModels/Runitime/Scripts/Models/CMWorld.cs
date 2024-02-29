using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMWorld
    {
        [SerializeField] private int id;
        [SerializeField] private string label;
        [SerializeField] private bool enabled;

        public int Id { get => id; set => id = value; }
        public string Label { get => label; set => label = value; }
        public bool Enabled { get => enabled; set => enabled = value; }
    }
}
