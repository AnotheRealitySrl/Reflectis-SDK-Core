using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.FAQ
{
    [Serializable]
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class FAQAnswerData 
    {
        public int AnswerOrder;
        public string AnswerDetail;
        public string AnswerImage;
    }
}
