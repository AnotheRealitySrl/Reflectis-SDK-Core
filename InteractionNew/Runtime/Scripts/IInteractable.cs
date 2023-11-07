using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.InteractionNew
{
    /// <summary>
    /// Common interaface for any interactable entity.
    /// </summary>
    public interface IInteractable
    {
        public enum EInteractionState
        {
            Idle,
            Hovered,
            Interaction
        }

        List<IInteractableBehaviour> InteractableBehaviours { get; }
        EInteractionState InteractionState { get; set; }

        GameObject GameObjectRef { get; }
        List<Collider> InteractionColliders { get; }

        UnityEvent OnInteractableSetupComplete { get; }

        void OnHoverEnter();
        void OnHoverExit();
    }
}