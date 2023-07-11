using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.CharacterController
{
    public interface ISeatable
    {
        bool isInteractable { get; set;}

        public void SitAction(CharacterControllerBase characterController,ref bool isToggled);

        public void StepUpAction(ref bool isToggled);
    }
}
