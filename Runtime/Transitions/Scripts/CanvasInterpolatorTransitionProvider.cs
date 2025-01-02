using Reflectis.SDK.Utilities;
using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Transitions
{
    /// <summary>
    /// Transition provider that operates on the alpha value of a canvas group
    /// </summary>
    public class CanvasInterpolatorTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeTime = 1f;
        [SerializeField] private AnimationCurve easingFunction;
        [SerializeField] private bool isActive;
        [SerializeField] private bool activateGameObject = true;
        private Interpolator interpolator;

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
            CreateInterpolator();
        }

        private void CreateInterpolator()
        {
            if (easingFunction == null || easingFunction.keys.Length == 0)
            {
                easingFunction = AnimationCurve.EaseInOut(0, 0, fadeTime, 1);
            }
            interpolator = new Interpolator(
                fadeTime, FadeLerp, GetStartTime, easingFunction
                );
        }

        private float GetStartTime()
        {
            return interpolator.InverseEase.Evaluate(canvasGroup.alpha) * fadeTime;
        }

        private void FadeLerp(float value)
        {
            canvasGroup.alpha = value;
            if (activateGameObject)
            {
                gameObject.SetActive(value != 0);
            }
        }

        public override async Task EnterTransitionAsync()
        {

            onEnterTransitionStart?.Invoke();
            await interpolator.PlayForward();
            OnEnterTransitionFinish?.Invoke();
        }

        public override async Task ExitTransitionAsync()
        {

            OnExitTransitionStart?.Invoke();
            await interpolator.PlayBackwards();
            onExitTransitionFinish?.Invoke();
        }

    }
}