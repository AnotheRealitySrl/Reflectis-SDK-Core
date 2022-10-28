using System.Threading.Tasks;
using UnityEngine;


namespace SPACS.Toolkit.Transitions.Runtime
{
    public abstract class AbstractTransitionProvider : MonoBehaviour
    {
        public virtual async Task DoTransition(bool value)
        {
            if (value)
            {
                await EnterTransition();
            }
            else
            {
                await ExitTransition();
            }
        }

        public abstract Task EnterTransition();
        public abstract Task ExitTransition();
    }
}

