using System.Threading.Tasks;
using UnityEngine;

namespace SPACS.SDK.Utilities.Transitions
{
    public class NoTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField, Tooltip("The GameObject to activate")]
        private GameObject content;

        public override async Task EnterTransitionAsync()
        {
            await Task.Yield();
            content.SetActive(true);
            await Task.Yield();
        }

        public override async Task ExitTransitionAsync()
        {
            await Task.Yield();
            content.SetActive(false);
            await Task.Yield();
        }
    }
}