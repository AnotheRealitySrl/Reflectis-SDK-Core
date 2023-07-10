using System;
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class UserList
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private List<User> users;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<User> Users { get => users; set => users = value; }
    }
}
