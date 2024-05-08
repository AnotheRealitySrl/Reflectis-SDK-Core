
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
                interpolator = new Interpolator(duration, LerpFunction, GetStartTime, ease);

                isInit = true;
            }
        }

        private float GetStartTime()
        {
            Color currentColor = Getter();
            if (defaultColor.a != Color.a)//we can look at the alpha for linear interpolation
            {
                return interpolator.InverseEase.Evaluate(currentColor.a / Color.a);
            }
            else
            {
                Vector3 currentColorVector = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                Vector3 colorVector = new Vector3(Color.r, Color.g, Color.b);
                Vector3 defaultColorVector = new Vector3(defaultColor.r, defaultColor.g, defaultColor.b);
                float currentPos = Vector3.Distance(colorVector, currentColorVector) / Vector3.Distance(colorVector, defaultColorVector);
                return interpolator.InverseEase.Evaluate(currentPos);
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
