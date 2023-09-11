using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMTag 
    {

        [SerializeField] private int id;
        [SerializeField] private DateTime creationDate;
        [SerializeField] private DateTime lastUpdate;
        [SerializeField] private string label;
        [SerializeField] private string note;
        [SerializeField] private bool isEnabled;
        [SerializeField] private Color color;
        [SerializeField] private TagType type;

        public int Id { get => id; set => id = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public DateTime LastUpdate { get => lastUpdate; set => lastUpdate = value; }
        public string Label { get => label; set => label = value; }
        public string Note { get => note; set => note = value; }
        public bool IsEnabled { get => isEnabled; set => isEnabled = value; }
        public Color Color { get => color; set => color = value; }
        public TagType Type { get => type; set => type = value; }
    }

    public enum TagType
    {
        Environment, User, Tag
    }
}
