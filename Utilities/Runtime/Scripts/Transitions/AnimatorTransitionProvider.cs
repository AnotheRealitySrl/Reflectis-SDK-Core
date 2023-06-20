using SPACS.SDK.Utilities.Extensions;

using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.Utilities.Transitions
{
    public class AnimatorTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField] private Animator animator;

        public override async Task EnterTransitionAsync()
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

        public override async Task ExitTransitionAsync()
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