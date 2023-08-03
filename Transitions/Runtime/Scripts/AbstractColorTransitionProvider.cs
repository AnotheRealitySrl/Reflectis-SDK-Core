using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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

        private Tween forwardTween;

        private Tween backwardTween;
        protected virtual void Awake()
        {
            defaultColor = Getter();
            SetTween(ref forwardTween, color);
            SetTween(ref backwardTween, defaultColor);
        }
        public override async Task EnterTransitionAsync()
        {
            onEnterTransition?.Invoke();
            if (backwardTween != null && backwardTween.IsPlaying())
            {
                backwardTween.Pause();
            }
            forwardTween.Rewind();
            forwardTween.Play();
            while (forwardTween != null && forwardTween.IsPlaying())
            {
                await Task.Yield();
            }
        }

        public override async Task ExitTransitionAsync()
        {
            if (forwardTween != null && forwardTween.IsPlaying())
            {
                forwardTween.Pause();
            }
            backwardTween.Rewind();
            backwardTween.Play();
            while (backwardTween != null && backwardTween.IsPlaying())
            {
                await Task.Yield();
            }
            onExitTransition?.Invoke();
        }

        protected abstract Color Getter();

        protected abstract void Setter(Color color);

        private void SetTween(ref Tween tween, Color color)
        {
            tween = DOTween.To(Getter, Setter, color, duration);
            tween.Pause();
            tween.SetAutoKill(false);
            if (ease == null)
            {
                ease = AnimationCurve.Constant(0, duration, 1);
            }
            tween.SetEase(ease);
        }
    }
}
