using System;

using UnityEngine;

namespace Reflectis.InteractionLegacy
{

    /// <summary>
    /// Common interaface for any interactable entity.
    /// </summary>
    public interface IInteractable
    {
        GameObject gameObject { get; }

        GameObject InteractionTarget { get; }

        void Interact(Action completedCallback = null);

        void StopInteract(Action completedCallback = null);

    }
}
