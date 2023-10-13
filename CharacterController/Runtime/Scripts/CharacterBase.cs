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