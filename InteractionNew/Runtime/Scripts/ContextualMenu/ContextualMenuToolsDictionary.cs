using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;

using static Reflectis.SDK.InteractionNew.ContextualMenuManageable;

namespace Reflectis.SDK.InteractionDesktop
{
    [CreateAssetMenu(menuName = "AnotheReality/Utils/ContextualMenuToolsDictionary", fileName = "ContextualMenuToolsDictionary")]
    public class ContextualMenuToolsDictionary : SerializedScriptableObject
    {
        // This Dictionary will be serialized by Odin.
        [SerializeField] private Dictionary<EContextualMenuOption, TextAsset> tools = new();

        public Dictionary<EContextualMenuOption, TextAsset> Tools => tools;
    }
}
