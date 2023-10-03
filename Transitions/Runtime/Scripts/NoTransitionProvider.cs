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
            onEnterTransitionStart?.Invoke();
            await Task.Yield();
            Content.SetActive(true);
            await Task.Yield();
            OnEnterTransitionFinish?.Invoke();
        }

        public override async Task ExitTransitionAsync()
        {
            OnExitTransitionStart?.Invoke();
            await Task.Yield();
            Content.SetActive(false);
            await Task.Yield();
            onExitTransitionFinish?.Invoke();
        }
    }
}