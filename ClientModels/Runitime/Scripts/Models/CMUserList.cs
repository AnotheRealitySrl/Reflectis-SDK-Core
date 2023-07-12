using System;
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMUserList
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private List<CMUser> users;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<CMUser> Users { get => users; set => users = value; }
    }
}
