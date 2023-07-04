using Sirenix.OdinInspector;

using System.Linq;

using UnityEngine;

namespace Reflectis.SDK.CharacterController
{
    /// <summary>
    /// This script should be atached to some child nodes of a character, in order to identify them
    /// </summary>
    public class GenericHookComponent : MonoBehaviour
    {
        [SerializeField, Tooltip("Unique id to identify the node in the character hierarchy")]
        private string id;

        [SerializeField, Tooltip("Needs to be specified in case the GenericHookComponent refers to a transform other than the one to which it is applied")]
        private Transform transformRef;

        /// <summary>
        /// Unique id to identify the node in the character hierarchy
        /// </summary>
        public string Id { get => id; private set => id = value; }

        /// <summary>
        /// Needs t be specified in case the GenericHookComponent refers to a transform other than the one to which it is applied
        /// </summary>
        public Transform TransformRef => transformRef != null ? transformRef : transform;

        [Button]
        public string GenerateRandomId()
        {
            System.Random rnd = new();
            id = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 5).Select(s => s[rnd.Next(s.Length)]).ToArray());
            return id;
        }
    }
}