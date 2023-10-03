using Reflectis.SDK.Utilities.Extensions;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Transitions
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
            if (animator.ContainsParam(animatorParameter))
            {
                animator.SetBool(animatorParameter, true);
            }
            onEnterTransitionStart?.Invoke();
            while (animator.IsInTransition(0) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                await Task.Yield();
            }
            OnEnterTransitionFinish?.Invoke();
        }

        public override async Task ExitTransitionAsync()
        {
            if (animator.ContainsParam(animatorParameter))
            {
                animator.SetBool(animatorParameter, false);
            }
            OnExitTransitionStart?.Invoke();
            while (animator.IsInTransition(0) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                await Task.Yield();
            }
            onExitTransitionFinish?.Invoke();
        }
    }
}