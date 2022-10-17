using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    public interface ICharacterController
    {
        Transform HeadReference { get; }

        void Setup(ICharacterController source);
    }
}
