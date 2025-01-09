using Reflectis.SDK.Utilities.Extensions;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Core.Transitions
{
    /// <summary>
    /// Transition provider that operates on a bool parameter of an animator controller
    /// </summary>
    public class AnimatorTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string animatorParameter = "Visible";

        private void Awake()
        {
            if (!animator)
            {
                animator = GetComponent<Animator>();
            }
        }

        public override async Task EnterTransitionAsync()
        {
            int currentAnimation = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;

            if (animator.ContainsParam(animatorParameter))
            {
                animator.SetBool(animatorParameter, true);
            }
            onEnterTransitionStart?.Invoke();
            while (currentAnimation == animator.GetCurrentAnimatorStateInfo(0).shortNameHash
                || animator.IsInTransition(0)
                || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                await Task.Yield();
            }
            OnEnterTransitionFinish?.Invoke();
        }

        public override async Task ExitTransitionAsync()
        {
            //Check null reference on application quit log error.
            if (animator == null) return;

            int currentAnimation = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            if (animator.ContainsParam(animatorParameter))
            {
                animator.SetBool(animatorParameter, false);
            }
            OnExitTransitionStart?.Invoke();

            //Check null reference on application quit log error.
            if (animator == null) return;

            while (animator != null && currentAnimation == animator.GetCurrentAnimatorStateInfo(0).shortNameHash
            || animator != null && animator.IsInTransition(0) || animator != null && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                await Task.Yield();
            }


            onExitTransitionFinish?.Invoke();
        }
    }
}