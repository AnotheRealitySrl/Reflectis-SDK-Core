using SPACS.SDK.Utilities.Extensions;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.Utilities.Transitions
{
    public class MultipleAnimatorsTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField] private List<Animator> animatorsList;

        public override async Task EnterTransitionAsync()
        {
            IEnumerable<Task> animatorsTaskList = animatorsList.Select(async animator => 
            {
                if (animator.ContainsParam("Visible"))
                {
                    animator.SetBool("Visible", true);
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
                if (animator.ContainsParam("Visible"))
                {
                    animator.SetBool("Visible", false);
                }

                while (animator.IsInTransition(0) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                {
                    await Task.Yield();
                }
            });

            await Task.WhenAll(animatorsTaskList);
        }
    }
}