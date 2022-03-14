using System.Threading.Tasks;
using UnityEngine;


namespace SPACS.Core.Transitions
{
    public abstract class AbstractTransitionProvider : MonoBehaviour
    {
        public abstract Task EnterTransition(TransitionArgs args);
        public abstract Task ExitTransition(TransitionArgs args);
    }
}

