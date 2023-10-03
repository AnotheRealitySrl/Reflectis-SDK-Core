using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMUser
    {
        [SerializeField] private int id;
        [SerializeField] private string displayName;
        [SerializeField] private List<CMTag> tags;

        public int ID { get => id; set => id = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public List<CMTag> Tags { get => tags; set => tags = value; }
    }

}
