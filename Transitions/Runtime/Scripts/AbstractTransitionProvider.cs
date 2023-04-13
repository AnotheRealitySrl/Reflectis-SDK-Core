using System.Threading.Tasks;
using UnityEngine;


namespace SPACS.SDK.Transitions
{
    public abstract class AbstractTransitionProvider : MonoBehaviour
    {
        [SerializeField] private bool reverseTransitions;

        public virtual async void DoTransition(bool value) => await DoTransitionAsync(value);
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

        public virtual async void EnterTransition() => await EnterTransitionAsync();
        public virtual async void ExitTransition() => await ExitTransitionAsync();

        public abstract Task EnterTransitionAsync();
        public abstract Task ExitTransitionAsync();
    }
}

