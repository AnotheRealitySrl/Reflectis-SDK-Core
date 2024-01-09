using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;

using static Reflectis.SDK.InteractionNew.ContextualMenuManageable;

namespace Reflectis.SDK.InteractionNew
{
    [CreateAssetMenu(menuName = "AnotheReality/Utils/ContextualMenuTypesDictionary", fileName = "ContextualMenuTypesDictionary")]
    public class ContextualMenuTypesDictionary : SerializedScriptableObject
    {
        // This Dictionary will be serialized by Odin.
        [SerializeField] private Dictionary<EContextualMenuType, ContextualMenuController> contextualMenuTypes = new();

        public Dictionary<EContextualMenuType, ContextualMenuController> ContextualMenuTypes => contextualMenuTypes;
    }
}
