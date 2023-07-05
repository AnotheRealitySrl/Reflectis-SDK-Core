using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Transitions
{
    /// <summary>
    /// Fake transition provider, it only activates/deactivates the referenced GameObject
    /// </summary>
    public class NoTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField, Tooltip("The GameObject to activate")]
        private GameObject content;

        private void Awake()
        {
            if (!content)
            {
                content = gameObject;
            }
        }

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