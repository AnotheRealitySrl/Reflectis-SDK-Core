using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.FAQ
{
    [Serializable]
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class FAQCategoryData
    {
        public int CategoryOrder;
        public string CategoryTitle;
        public List<FAQSubcategoryData> Subcategories;
    }
}
