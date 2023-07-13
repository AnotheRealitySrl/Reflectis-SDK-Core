using System;
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class PermissionBlock
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private List<Permission> permissions;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<Permission> Permissions { get => permissions; set => permissions = value; }
    }
}
