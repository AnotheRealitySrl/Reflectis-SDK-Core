using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.CharacterController
{
    /// <summary>
    /// Base implementation of <see cref="ICharacter"/>. It only exposes the properties defined in the interface.
    /// </summary>
    public class CharacterBase : MonoBehaviour, ICharacter
    {
        [Header("Character structure")]
        [SerializeField] protected Transform pivotReference;
        [SerializeField] protected Transform headReference;
        [SerializeField] protected Transform leftHandReference;
        [SerializeField] protected Transform rightHandReference;
        [SerializeField] protected Transform labelReference;
        [SerializeField] protected Transform tagReference;


        #region FingerBones References
        //Right Hand Fingers
        [SerializeField] protected Transform rightHandMiddleProximalReference;
        [SerializeField] protected Transform rightHandMiddleIntermediateReference;
        [SerializeField] protected Transform rightHandMiddleEndReference;

        [SerializeField] protected Transform rightHandIndexProximalReference;
        [SerializeField] protected Transform rightHandIndexIntermediateReference;
        [SerializeField] protected Transform rightHandIndexEndReference;

        [SerializeField] protected Transform rightHandRingProximalReference;
        [SerializeField] protected Transform rightHandRingIntermediateReference;
        [SerializeField] protected Transform rightHandRingEndReference;

        [SerializeField] protected Transform rightHandPinkyProximalReference;
        [SerializeField] protected Transform rightHandPinkyIntermediateReference;
        [SerializeField] protected Transform rightHandPinkyEndReference;

        [SerializeField] protected Transform rightHandThumbProximalReference;
        [SerializeField] protected Transform rightHandThumbIntermediateReference;
        [SerializeField] protected Transform rightHandThumbEndReference;

        //Left Hand Fingers
        [SerializeField] protected Transform leftHandMiddleProximalReference;
        [SerializeField] protected Transform leftHandMiddleIntermediateReference;
        [SerializeField] protected Transform leftHandMiddleEndReference;

        [SerializeField] protected Transform leftHandIndexProximalReference;
        [SerializeField] protected Transform leftHandIndexIntermediateReference;
        [SerializeField] protected Transform leftHandIndexEndReference;
                                             
        [SerializeField] protected Transform leftHandRingProximalReference;
        [SerializeField] protected Transform leftHandRingIntermediateReference;
        [SerializeField] protected Transform leftHandRingEndReference;

        [SerializeField] protected Transform leftHandPinkyProximalReference;
        [SerializeField] protected Transform leftHandPinkyIntermediateReference;
        [SerializeField] protected Transform leftHandPinkyEndReference;

        [SerializeField] protected Transform leftHandThumbProximalReference;
        [SerializeField] protected Transform leftHandThumbIntermediateReference;
        [SerializeField] protected Transform leftHandThumbEndReference;

        #endregion

        [SerializeField] private float playerHeight = 1.65f;

        public Transform PivotReference => pivotReference;
        public Transform HeadReference => headReference;
        public Transform LeftInteractorReference => leftHandReference;
        public Transform RightInteractorReference => rightHandReference;
        public Transform LabelReference => labelReference;
        public Transform TagReference => tagReference;

        public float PlayerHeight { get => playerHeight; set => playerHeight = value; }

        public virtual Task Setup() => Task.CompletedTask;
        public virtual Task Unsetup() => Task.CompletedTask;
    }
}