using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    public abstract class CharacterControllerBase : MonoBehaviour, ICharacterController
    {
        public Transform HeadReference { get; private set; }

        public abstract Task<ICharacterController> Setup(ICharacterController source);
    }
}