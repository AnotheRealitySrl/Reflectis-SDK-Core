using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.CharacterController
{
    /// <summary>
    /// Base implementation of <see cref="ICharacter"/>.
    /// </summary>
    public class CharacterBase : MonoBehaviour, ICharacter
    {
        [SerializeField] protected Transform pivotReference;
        [SerializeField] protected Transform headReference;
        [SerializeField] protected Transform leftHandReference;
        [SerializeField] protected Transform rightHandReference;
        [SerializeField] protected Transform labelReference;

        public Transform PivotReference => pivotReference;
        public Transform HeadReference => headReference;
        public Transform LeftInteractorReference => leftHandReference;
        public Transform RightInteractorReference => rightHandReference;
        public Transform LabelReference => labelReference;

        public virtual Task Setup() => Task.CompletedTask;
        public virtual Task Unsetup() => Task.CompletedTask;
    }
}