using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.Transitions
{
    /// <summary>
    /// Abstract class with definitions of methods performing transitions.
    /// </summary>
    public abstract class AbstractTransitionProvider : MonoBehaviour
    {
        [SerializeField, Tooltip("If true, methods DoTranition and DoTransitionAsync revert their boolean parameter value")]
        private bool reverseTransitions;


        /// <summary>
        /// Performs an enter transition. It can be awaited
        /// </summary>
        /// <returns>Awaitable task</returns>
        public abstract Task EnterTransitionAsync();

        /// <summary>
        /// Performs an exit transition. It can be awaited
        /// </summary>
        /// <returns>Awaitable task</returns>
        public abstract Task ExitTransitionAsync();


        /// <summary>
        /// Non-awaitable version of <see cref="EnterTransitionAsync"/>
        /// </summary>
        public virtual async void EnterTransition() => await EnterTransitionAsync();

        /// <summary>
        /// Non-awaitable version of <see cref="ExitTransitionAsync"/>
        /// </summary>
        public virtual async void ExitTransition() => await ExitTransitionAsync();


        /// <summary>
        /// Calls either <see cref="EnterTransitionAsync"/> or <see cref="ExitTransitionAsync"/> depending on the value passed
        /// (and on the value of <see cref="reverseTransitions"/> field)
        /// </summary>
        /// <param name="value">If true, it performes <see cref="EnterTransitionAsync"/>, otherwise <see cref="ExitTransitionAsync"/></param>
        public virtual async Task DoTransitionAsync(bool value)
        {
            if (reverseTransitions)
                value = !value;

            if (value)
            {
                await EnterTransitionAsync();
            }
            else
            {
                await ExitTransitionAsync();
            }
        }

        /// <summary>
        /// Non-awaitable version of <see cref="DoTransition"/>
        /// </summary>
        /// <param name="value">If true, it performes <see cref="EnterTransitionAsync"/>, otherwise <see cref="ExitTransitionAsync"/></param>
        public virtual async void DoTransition(bool value) => await DoTransitionAsync(value);
    }
}

