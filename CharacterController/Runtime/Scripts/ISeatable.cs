using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.CharacterController
{
    public interface ISeatable
    {
        bool isInteractable { get; set;}

        public void SitAction(CharacterControllerBase characterController);

        public void StepUpAction(CharacterControllerBase characterController);
    }
}
