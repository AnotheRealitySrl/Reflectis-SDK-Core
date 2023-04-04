using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace SPACS.SDK.CharacterController
{
    public class GenericHookComponent : MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] private Transform transformRef;

        public string Id { get => id; private set => id = value; }
        public Transform TransformRef => transformRef;

        [Button]
        public string GenerateRandomId() 
        {
            System.Random rnd = new();
            id = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 5).Select(s => s[rnd.Next(s.Length)]).ToArray());
            return id;
        }
    }
}