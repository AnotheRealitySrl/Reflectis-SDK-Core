using Reflectis.SDK.Core;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    [CreateAssetMenu(menuName = "Reflectis/SDK-Interaction/HalfTransitionScriptableAction", fileName = "HalfTransitionScriptableAction")]
    public class HalfTransitionScriptableAction : AwaitableScriptableAction
    {
        [SerializeField] private bool activate;
        [SerializeField] private float transitionTimeInSeconds = 0.2f;

        public override Task Action(IInteractable interactable)
        {
            if (activate)
            {
                SM.GetSystem<ContextualMenuSystem>().ContextualMenuInstance.GetComponentInChildren<CanvasGroup>().alpha = 0.5f;
            }
            else
            {
                SM.GetSystem<ContextualMenuSystem>().ContextualMenuInstance.GetComponentInChildren<CanvasGroup>().alpha = 0f;
            }

            return Task.CompletedTask;
        }
    }
}
