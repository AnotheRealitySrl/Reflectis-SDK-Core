using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    public abstract class CharacterControllerBase : MonoBehaviour, ICharacterController
    {
        #region Inspector variables

        [Header("Avatar references")]
        [SerializeField] protected Transform pivotReference;
        [SerializeField] protected Transform headReference;

        #endregion

        #region Interface implementation

        public Transform PivotReference => pivotReference;
        public Transform HeadReference => headReference;

        public abstract Task Setup(CharacterControllerBase source);

        #endregion

    }
}