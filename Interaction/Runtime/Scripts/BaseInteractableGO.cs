using System;

using UnityEngine;

namespace SPACS.SDK.Interaction
{

    public class BaseInteractableGO : MonoBehaviour, IInteractable
    {

        [SerializeField] protected ActionScriptable InteractAction;
        [SerializeField] protected ActionScriptable StopInteractAction;

        public GameObject InteractionTarget
        {
            get { return _interactionTarget; }
            set { _interactionTarget = value; }
        }
        [SerializeField] private GameObject _interactionTarget;

        public virtual void Interact(Action completedCallback = null)
        {
            if (!InteractAction)
            {
                completedCallback?.Invoke();
                return;
            }

            InteractAction.InteractableObjectReference = this;
            InteractAction.Action(completedCallback);
            InteractAction.InteractableObjectReference = null;
        }

        public virtual void StopInteract(Action completedCallback = null)
        {
            if (!StopInteractAction)
            {
                completedCallback?.Invoke();
                return;
            }

            StopInteractAction.InteractableObjectReference = this;
            StopInteractAction.Action(completedCallback);
            StopInteractAction.InteractableObjectReference = null;
        }
    }
}
