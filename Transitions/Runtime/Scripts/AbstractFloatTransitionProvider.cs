using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Reflectis.SDK.Transitions
{
    public abstract class AbstractFloatTransitionProvider : AbstractTransitionProvider
    {
        [Header("Transition parameters")]
        [SerializeField, Tooltip("Destination")]
        private float destination;
        [SerializeField]
        private float duration;
        [SerializeField]
        private AnimationCurve ease;

        private float defaultValue = 0f;

        private Tween tween;
        protected virtual void Awake()
        {
            if (ease.keys.Count() == 0)
            {
                ease = AnimationCurve.Linear(0, 0, duration, 1);
            }
            defaultValue = Getter();
        }
        public override async Task EnterTransitionAsync()
        {
            onEnterTransition?.Invoke();
            if (tween != null)
            {
                tween.Kill();
            }
            tween = DOTween.To(Getter, Setter, destination, duration).SetEase(ease);
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
            tween = DOTween.To(Getter, Setter, defaultValue, duration).SetEase(ease);
            while (tween.IsPlaying())
            {
                await Task.Yield();
            }
            onExitTransition?.Invoke();
        }

        protected abstract float Getter();

        protected abstract void Setter(float value);

    }
}
