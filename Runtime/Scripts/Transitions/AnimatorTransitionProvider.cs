using System.Threading.Tasks;
using UnityEngine;

namespace SPACS.Core.Transitions
{
    public class AnimatorTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField] 
        private Animator animator;
        
        public override async Task EnterTransition(TransitionArgs args)
        {
            if (animator.ContainsParam("Animate"))
            {
                animator.SetBool ("Animate", !args.skipTransitionAnimation);
            }
            if (animator.ContainsParam ("Visible"))
            {
                animator.SetBool ("Visible", true);
            }

            while (animator.IsInTransition(0) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) {
                await Task.Yield();
            }
        }

        public override async Task ExitTransition(TransitionArgs args)
        {
            if (animator.ContainsParam ("Animate"))
            {
                animator.SetBool ("Animate", !args.skipTransitionAnimation);
            }
            if (animator.ContainsParam ("Visible"))
            {
                animator.SetBool ("Visible", false);
            }
            
            while (animator.IsInTransition(0) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) {
                await Task.Yield();
            }
        }
    }
}