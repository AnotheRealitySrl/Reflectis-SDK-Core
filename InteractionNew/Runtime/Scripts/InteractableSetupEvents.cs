using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.InteractionNew
{
    public class InteractableSetupEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent onInteractableSetupCompleteCallbacks = new();

        public UnityEvent OnInteractableSetupCompleteCallbacks { get => onInteractableSetupCompleteCallbacks; set => onInteractableSetupCompleteCallbacks = value; }
    }
}
