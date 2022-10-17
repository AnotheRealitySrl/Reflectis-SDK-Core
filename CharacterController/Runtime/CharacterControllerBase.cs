using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    public class CharacterControllerBase : ICharacterController
    {
        public Transform HeadReference { get; private set; }

        public virtual void Setup(ICharacterController source)
        {
            HeadReference = source.HeadReference;
        }
    }
}