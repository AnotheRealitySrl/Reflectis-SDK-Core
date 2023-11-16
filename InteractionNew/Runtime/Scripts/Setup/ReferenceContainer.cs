using System;
using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public class ReferenceContainer : MonoBehaviour
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private Reference[] customReferences;

        public Reference[] CustomReferences { get => customReferences; set => customReferences = value; }
        public string Id { get => id; set => id = value; }
    }

    [Serializable]
    public struct Reference
    {
        public string name;
        public Component component;
    }
}
