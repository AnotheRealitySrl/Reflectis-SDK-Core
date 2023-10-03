using DG.Tweening;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Transitions
{
    /// <summary>
    /// Transition provider that operates on the alpha value of a canvas group
    /// </summary>
    public class CanvasTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeTime = 1f;
        [SerializeField] private Ease easingFunction = Ease.InOutQuad;
        [SerializeField] private bool isActive;
        [SerializeField] private bool activateGameObject = true;


        private Tweener transition;

        private void Awake()
        {
            if (!canvasGroup)
            {
                canvasGroup = GetComponentInChildren<CanvasGroup>();
            }
            if (!isActive)
            {
                if (activateGameObject)
                {
                    canvasGroup.gameObject.SetActive(false);
                }
                canvasGroup.alpha = 0;
            }
        }

        public override async Task EnterTransitionAsync()
        {
            if (activateGameObject)
            {
                canvasGroup.gameObject.SetActive(true);
            }
            KillActiveTransition();
            onEnterTransitionStart?.Invoke();
            transition = canvasGroup.DOFade(1f, fadeTime).SetEase(easingFunction);
            await transition.AsyncWaitForCompletion();
            OnEnterTransitionFinish?.Invoke();
        }

        public override async Task ExitTransitionAsync()
        {
            KillActiveTransition();
            Tweener newTransition = canvasGroup.DOFade(0f, fadeTime).SetEase(easingFunction);
            transition = newTransition;
            OnExitTransitionStart?.Invoke();
            await transition.AsyncWaitForCompletion();
            if (activateGameObject && newTransition == transition)
            {
                canvasGroup.gameObject.SetActive(false);
            }
            transition = null;
            onExitTransitionFinish?.Invoke();
        }

        private void KillActiveTransition()
        {
            if (transition != null && (!transition.IsComplete() || !transition.IsActive()))
            {
                transition.Kill();
            }
        }
    }
}
