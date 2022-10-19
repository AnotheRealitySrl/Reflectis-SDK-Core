using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    public abstract class CharacterControllerBase<T> : MonoBehaviour, ICharacterController where T : ICharacterController
    {
        public Transform HeadReference { get; private set; }

        public abstract Task<T> Setup(T source);

    }
}