using System;
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
        [Flags]
        public enum EInteractableType
        {
            GenericInteractable = 1,
            Manipulable = 2,
            ContextualMenuInteractable = 4
        }

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

        void Setup();
        void OnHoverEnter();
        void OnHoverExit();

    }
}