using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    public class HandPlaceholder : MonoBehaviour
    {
        [SerializeField] private Transform palm;

        public Transform Palm => palm;
    }
}
