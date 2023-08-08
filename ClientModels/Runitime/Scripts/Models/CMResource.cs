using Reflectis.SDK.DataAccess;
using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMResource
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private string path;
        [SerializeField] private int type;
        [SerializeField] private DateTime creationDate;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }
        public int Type { get => type; set => type = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }

        public CMResource(AssetDTO asset)
        {
            this.ID = asset.Id;
            this.Name = asset.Label;
            this.Path = asset.Folder;
            //this.Type = asset.Extension;
            this.CreationDate = asset.CreationDate;
        }
    }
}
