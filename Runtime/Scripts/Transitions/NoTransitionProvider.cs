using System.Threading.Tasks;
using UnityEngine;

namespace SPACS.Core.Transitions
{
    public class NoTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField, Tooltip("The GameObject to activate")]
        private GameObject content;
        
        public override Task EnterTransition(TransitionArgs args)
        {
            content.SetActive(true);
            return Task.CompletedTask;
        }

        public override Task ExitTransition(TransitionArgs args)
        {
            content.SetActive(false);
            return Task.CompletedTask;
        }
    }
}