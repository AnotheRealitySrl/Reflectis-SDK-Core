using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    public interface ICharacterController
    {
        Transform PivotReference { get; }
        Transform HeadReference { get; }

        Task Setup(CharacterControllerBase source);
    }
}
