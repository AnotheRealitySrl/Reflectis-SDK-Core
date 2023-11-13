using DG.Tweening;

using System.Linq;
using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Transitions
{
    public abstract class AbstractColorTransitionProvider : AbstractTransitionProvider
    {
        [Header("Transition parameters")]
        [SerializeField, Tooltip("The color to change it into")]
        private Color color;

        public float duration;
        [SerializeField]
        private AnimationCurve ease;

        private Color defaultColor = Color.white;

        private Tween tween;

        public Color Color { get => color; set => color = value; }

        protected virtual void Awake()
        {
            if (ease.keys.Count() == 0)
            {
                ease = AnimationCurve.Linear(0, 0, duration, 1);
            }
            defaultColor = Getter();
        }
        public override async Task EnterTransitionAsync()
        {
            onEnterTransitionStart?.Invoke();
            if (tween != null)
            {
                tween.Kill();
            }
            tween = DOTween.To(Getter, Setter, color, duration).SetEase(ease);
            while (tween.IsPlaying())
            {
                await Task.Yield();
            }
            OnEnterTransitionFinish?.Invoke();
        }

        public override async Task ExitTransitionAsync()
        {
            if (tween != null)
            {
                tween.Kill();
            }
            OnExitTransitionStart?.Invoke();
            tween = DOTween.To(Getter, Setter, defaultColor, duration).SetEase(ease);
            while (tween.IsPlaying())
            {
                await Task.Yield();
            }
            onExitTransitionFinish?.Invoke();
        }

        protected abstract Color Getter();

        protected abstract void Setter(Color color);

    }
}
