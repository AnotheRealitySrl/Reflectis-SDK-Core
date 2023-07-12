using System;
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMEventPermissionSet
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private List<CMPermissionBlock> permissionBlocks;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<CMPermissionBlock> PermissionBlocks { get => permissionBlocks; set => permissionBlocks = value; }
    }
}
