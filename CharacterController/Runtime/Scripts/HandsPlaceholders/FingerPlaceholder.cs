using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    public class FingerPlaceholder : MonoBehaviour
    {
        [SerializeField] private EFingerType fingerType;
        [SerializeField] private Transform tip;

        public EFingerType FingerType => fingerType;
        public Transform Tip => tip;
    }
}