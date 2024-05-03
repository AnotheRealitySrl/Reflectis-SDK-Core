
using Reflectis.SDK.Utilities;
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

        private bool isInit = false;

        private Color defaultColor = Color.white;

        private Interpolator interpolator;
        public Color Color { get => color; set => color = value; }

        protected virtual void Awake()
        {
            Init();
        }
        public override async Task EnterTransitionAsync()
        {
            Init();
            onEnterTransitionStart?.Invoke();
            await interpolator.PlayForward();
            OnEnterTransitionFinish?.Invoke();
        }

        public override async Task ExitTransitionAsync()
        {
            Init();

            OnExitTransitionStart?.Invoke();
            await interpolator.PlayBackwards();
            onExitTransitionFinish?.Invoke();
        }

        private void Init()
        {
            if (!isInit)
            {
                defaultColor = Getter();
                if (ease.keys.Count() == 0)
                {
                    ease = AnimationCurve.Linear(0, 0, duration, 1);
                }
                interpolator = new Interpolator(duration, LerpFunction, ease);

                isInit = true;
            }
        }

        private void LerpFunction(float value)
        {
            Setter(Color.Lerp(defaultColor, Color, value));
        }

        protected abstract Color Getter();

        protected abstract void Setter(Color color);

    }
}
