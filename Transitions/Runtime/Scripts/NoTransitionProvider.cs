using System.Threading.Tasks;
using UnityEngine;

namespace SPACS.Toolkit.Transitions.Runtime
{
    public class NoTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField, Tooltip("The GameObject to activate")]
        private GameObject content;

        public override async Task EnterTransition()
        {
            await Task.Yield();
            content.SetActive(true);
            await Task.Yield();
        }

        public override async Task ExitTransition()
        {
            await Task.Yield();
            content.SetActive(false);
            await Task.Yield();
        }
    }
}