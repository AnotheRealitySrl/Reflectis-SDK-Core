using Reflectis.SDK.Core;

using System.Linq;
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
                SM.GetSystem<ContextualMenuSystem>().ShowPreviewContextualMenu((ContextualMenuManageable)interactable.InteractableBehaviours.FirstOrDefault(x => x is ContextualMenuManageable));
            }
            else
            {
                SM.GetSystem<ContextualMenuSystem>().HidePreviewContextualMenu();
            }

            return Task.CompletedTask;
        }
    }
}
