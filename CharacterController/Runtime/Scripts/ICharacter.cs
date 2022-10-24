using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    public interface ICharacter
    {
        Transform PivotReference { get;}
        Transform HeadReference { get; }
        Transform LeftInteractorReference { get; }
        Transform RightInteractorReference { get; }

        Task Setup(CharacterControllerBase source);
    }
}