using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.CharacterController
{
    /// <summary>
    /// Base implementation of <see cref="ICharacter"/>. It only exposes the properties defined in the interface.
    /// </summary>
    public class CharacterBase : MonoBehaviour, ICharacter
    {
        //The trigger colliders of the hands. Used to know if the hand is entering a rigidbody.
        [Header("Hands Colliders")]
        [SerializeField] protected Collider leftColliderInteractorReference;
        [SerializeField] protected Collider rightColliderInteractorReference;

        [Header("Character structure")]
        [SerializeField] protected Transform pivotReference;
        [SerializeField] protected Transform headReference;
        [SerializeField] protected Transform leftHandReference;
        [SerializeField] protected Transform rightHandReference;
        [SerializeField] protected Transform labelReference;
        [SerializeField] protected float labelOffsetFromBounds = 0.06f;
        [SerializeField] protected Transform tagReference;

        [SerializeField] private float playerHeight = 1.65f;

        #region FingerBones References
        [Header("Fingers structure")]
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

        public Transform PivotReference => pivotReference;
        public Transform HeadReference => headReference;
        public Transform LeftInteractorReference => leftHandReference;
        public Transform RightInteractorReference => rightHandReference;
        public Collider LeftColliderInteractorReference => leftColliderInteractorReference;
        public Collider RightColliderInteractorReference => rightColliderInteractorReference;
        public Transform LabelReference => labelReference;
        public float LabelOffsetFromBounds => labelOffsetFromBounds;
        public Transform TagReference => tagReference;

        public float PlayerHeight { get => playerHeight; set => playerHeight = value; }

        public virtual Task Setup() => Task.CompletedTask;
        public virtual Task Unsetup() => Task.CompletedTask;
    }
}