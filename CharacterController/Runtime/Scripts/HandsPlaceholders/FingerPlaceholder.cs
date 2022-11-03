using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    public class FingerPlaceholder : MonoBehaviour
    {
        [SerializeField] private EFingerType fingerType;

        public EFingerType FingerType => fingerType;
    }
}