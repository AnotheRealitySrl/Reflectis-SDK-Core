using SPACS.SDK.CharacterController;

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.Avatars
{
    public interface IAvatarController
    {
        Task Setup(CharacterControllerBase characterController);
        Task Unsetup();
    }
}
