using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField]
        private float duration;
        [SerializeField]
        private AnimationCurve ease;

        private Color defaultColor = Color.white;

        private Tween tween;

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
            onEnterTransition?.Invoke();
            if (tween != null)
            {
                tween.Kill();
            }
            tween = DOTween.To(Getter, Setter, color, duration).SetEase(ease);
            while (tween.IsPlaying())
            {
                await Task.Yield();
            }
        }

        public override async Task ExitTransitionAsync()
        {
            if (tween != null)
            {
                tween.Kill();
            }
            tween = DOTween.To(Getter, Setter, defaultColor, duration).SetEase(ease);
            while (tween.IsPlaying())
            {
                await Task.Yield();
            }
            onExitTransition?.Invoke();
        }

        protected abstract Color Getter();

        protected abstract void Setter(Color color);

    }
}
