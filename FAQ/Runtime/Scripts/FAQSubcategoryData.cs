using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.FAQ
{
    [Serializable]
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class FAQSubcategoryData : MonoBehaviour
    {
        public int SubcategoryOrder;
        public string SubcategoryTitle;
        public List<FAQQuestionData> Questions;
    }
}
