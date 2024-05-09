using Reflectis.SDK.Utilities;
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

        private Interpolator interpolator;

        protected virtual void Awake()
        {
            if (ease.keys.Count() == 0)
            {
                ease = AnimationCurve.Linear(0, 0, duration, 1);
            }
            defaultValue = Getter();
            interpolator = new Interpolator(duration, LerpFunction, GetStartTime, ease);
        }

        private float GetStartTime()
        {
            return interpolator.InverseEase.Evaluate(Getter()) * duration;
        }

        private void LerpFunction(float obj)
        {
            Setter(defaultValue + (defaultValue - destination) * obj);
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
            await interpolator.PlayForward();
            onExitTransitionFinish?.Invoke();
        }

        protected abstract float Getter();

        protected abstract void Setter(float value);

    }
}
