using Reflectis.SDK.DataAccess;
using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMCategory
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private int parent;

        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Parent { get => parent; set => parent = value; }

        public CMCategory()
        {
        }

        public CMCategory(CategoryDTO category)
        {
            this.ID = category.Id;
            this.Name = category.Label;
        }
        public CMCategory(SubcategoryDTO subcategory, int parentId)
        {
            this.ID = subcategory.Id;
            this.Name = subcategory.Label;
            this.Parent = parentId;
        }
    }
}
