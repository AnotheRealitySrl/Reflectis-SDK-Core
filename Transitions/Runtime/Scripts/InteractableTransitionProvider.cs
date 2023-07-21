using Reflectis.SDK.Interaction;
using Reflectis.SDK.Transitions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTransitionProvider : AbstractTransitionProvider
{

    #region Inspector fields
    
    [SerializeField]
    private UnityEvent onInteractionCompleted;
    
    
    [SerializeField]
    private UnityEvent onStopInteractionCompleted;

    #endregion

    #region private variables
    private IInteractable interactable;

    private bool isInteractionRunning;
    #endregion

    private void Awake()
    {
        interactable = GetComponent<IInteractable>();
    }

    public override async Task EnterTransitionAsync()
    {
        isInteractionRunning = true;
        interactable.Interact(OnInteractionCompleted);

        while (isInteractionRunning)
        {
            await Task.Yield();
        }
        
    }

    public override async Task ExitTransitionAsync()
    {
        isInteractionRunning = true;
        interactable.StopInteract(OnExitInteractionCompleted);

        while (isInteractionRunning)
        {
            await Task.Yield();
        }

    }

    private void OnInteractionCompleted()
    {
        isInteractionRunning = false;
        onInteractionCompleted?.Invoke();
    }
    private void OnExitInteractionCompleted()
    {
        isInteractionRunning = false;
        onStopInteractionCompleted?.Invoke();
    }
}
