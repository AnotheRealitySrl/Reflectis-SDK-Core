using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.FAQ
{
    [Serializable]
    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class FAQQuestionData
    {
        public int QuestionOrder;
        public string QuestionTitle;
        public List<FAQAnswerData> Answers;
    }
}
