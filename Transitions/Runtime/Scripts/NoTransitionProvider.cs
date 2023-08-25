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

        public GameObject Content { 
            get 
            {
                if(content == null)
                {
                    return gameObject;
                }
                return content;
            } 
        }

        public override async Task EnterTransitionAsync()
        {
            onEnterTransition?.Invoke();
            await Task.Yield();
            Content.SetActive(true);
            await Task.Yield();
        }

        public override async Task ExitTransitionAsync()
        {
            await Task.Yield();
            Content.SetActive(false);
            await Task.Yield();
            onExitTransition?.Invoke();
        }
    }
}