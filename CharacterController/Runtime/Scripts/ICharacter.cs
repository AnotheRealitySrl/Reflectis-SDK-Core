using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.CharacterController
{
    public interface ICharacter
    {
        Transform PivotReference { get;}
        Transform HeadReference { get; }
        Transform LeftInteractorReference { get; }
        Transform RightInteractorReference { get; }
        Transform LabelReference { get; }

        Task Setup(CharacterControllerBase source);
        Task Unsetup();
    }
}