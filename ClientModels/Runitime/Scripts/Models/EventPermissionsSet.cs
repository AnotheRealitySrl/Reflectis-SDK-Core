using System;
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class EventPermissionSet
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private List<PermissionBlock> permissionBlocks;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<PermissionBlock> PermissionBlocks { get => permissionBlocks; set => permissionBlocks = value; }
    }
}
