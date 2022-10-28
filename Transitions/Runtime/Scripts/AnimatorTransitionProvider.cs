using SPACS.Toolkit.Extensions.Runtime;

using System.Threading.Tasks;
using UnityEngine;

namespace SPACS.Toolkit.Transitions.Runtime
{
    public class AnimatorTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField] private Animator animator;

        public override async Task EnterTransition()
        {
            if (animator.ContainsParam("Visible"))
            {
                animator.SetBool("Visible", true);
            }

            while (animator.IsInTransition(0) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) 
            {
                await Task.Yield();
            }
        }

        public override async Task ExitTransition()
        {
            if (animator.ContainsParam("Visible"))
            {
                animator.SetBool("Visible", false);
            }
            
            while (animator.IsInTransition(0) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) 
            {
                await Task.Yield();
            }
        }
    }
}