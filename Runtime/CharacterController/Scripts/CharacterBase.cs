using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Core.CharacterController
{
    /// <summary>
    /// Base implementation of <see cref="ICharacter"/>. It only exposes the properties defined in the interface.
    /// </summary>
    public class CharacterBase : MonoBehaviour, ICharacter
    {
        //The trigger colliders of the hands. Used to know if the hand is entering a rigidbody.
        [Header("Hands Colliders")]
        [SerializeField] private Collider leftColliderInteractorReference;
        [SerializeField] private Collider rightColliderInteractorReference;

        [Header("Character structure")]
        [SerializeField] private Transform pivotReference;
        [SerializeField] private Transform headReference;
        [SerializeField] private Transform leftHandReference;
        [SerializeField] private Transform rightHandReference;
        [SerializeField] private Transform labelReference;
        [SerializeField] private float labelOffsetFromBounds = 0.06f;
        [SerializeField] private Transform tagReference;

        [Header("Avatar Scale")]
        [SerializeField] private float playerHeight = 1.65f;
        [SerializeField] private float headToTopHeadNodesOffset = 0.2f;
        [SerializeField] private float scaleMlp = 1f;

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

        public float ActualMeshHeight { get; set; } = 1.7f;

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
        public float HeadToTopHeadNodesOffset { get => headToTopHeadNodesOffset; set => headToTopHeadNodesOffset = value; }
        public float ScaleMlp { get => scaleMlp; set => scaleMlp = value; }

        public virtual Task Setup() => Task.CompletedTask;
        public virtual Task Unsetup() => Task.CompletedTask;

        public virtual void CalibrateAvatar()
        {
            LabelReference.localPosition = new Vector3(labelReference.localPosition.x, PlayerHeight, labelReference.localPosition.z);
        }
    }
}