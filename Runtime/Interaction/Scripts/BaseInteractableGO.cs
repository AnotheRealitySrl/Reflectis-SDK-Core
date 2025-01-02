using System;

using UnityEngine;

namespace Reflectis.SDK.Interaction
{

    public class BaseInteractableGO : MonoBehaviour, IInteractable
    {

        [SerializeField] protected ActionScriptable InteractAction;
        [SerializeField] protected ActionScriptable StopInteractAction;

        bool isInteracting;
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
            isInteracting = true;
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
            isInteracting = false;
            StopInteractAction.InteractableObjectReference = this;
            StopInteractAction.Action(completedCallback);
            StopInteractAction.InteractableObjectReference = null;
        }

        public virtual void OnDestroy()
        {
            if(isInteracting)
            {
                StopInteract();
            }
        }
    }
}
