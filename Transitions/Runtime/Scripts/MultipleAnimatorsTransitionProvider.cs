using Reflectis.SDK.Utilities.Extensions;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Transitions
{
    /// <summary>
    /// Transition provider that operates on a multiple animators simultauneously
    /// </summary>
    public class MultipleAnimatorsTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField] private List<Animator> animatorsList = new();
        [SerializeField] private string animatorParameter = "Visible";

        public override async Task EnterTransitionAsync()
        {
            onEnterTransition?.Invoke();
            IEnumerable<Task> animatorsTaskList = animatorsList.Select(async animator =>
            {
                if (animator.ContainsParam(animatorParameter))
                {
                    animator.SetBool(animatorParameter, true);
                }

                while (animator.IsInTransition(0) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                {
                    await Task.Yield();
                }
            });

            await Task.WhenAll(animatorsTaskList);
        }

        public override async Task ExitTransitionAsync()
        {
            IEnumerable<Task> animatorsTaskList = animatorsList.Select(async animator =>
            {
                if (animator.ContainsParam(animatorParameter))
                {
                    animator.SetBool(animatorParameter, false);
                }

                while (animator.IsInTransition(0) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                {
                    await Task.Yield();
                }
            });

            await Task.WhenAll(animatorsTaskList);
            onExitTransition?.Invoke();
        }
    }
}