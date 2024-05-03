using Reflectis.SDK.Utilities;
using System.Threading.Tasks;
using UnityEngine;

namespace Reflectis.SDK.Transitions
{
    public class TransformTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField, Tooltip("Duration of the transition in seconds")]
        private float duration = 0.1f;
        [SerializeField]
        private Vector3 position = Vector3.zero;
        [SerializeField]
        private Vector3 rotation = Vector3.zero;
        [SerializeField]
        private Vector3 scale = Vector3.one;
        [SerializeField]
        private AnimationCurve ease = null;

        private Vector3 startScale;
        private Vector3 startPosition;
        private Quaternion startRotation;

        private Interpolator interpolator;
        private void Awake()
        {
            CreateInterpolator();
        }

        private void CreateInterpolator()
        {
            startScale = transform.localScale;
            startPosition = transform.localPosition;
            startRotation = transform.rotation;
            interpolator = new Interpolator(
                duration, LerpFunction, ease);
        }

        private void LerpFunction(float value)
        {
            transform.localScale = Vector3.Lerp(startScale, scale, value);
            transform.localPosition = Vector3.Lerp(startPosition, position, value);
            transform.localRotation = Quaternion.Lerp(startRotation, Quaternion.Euler(rotation), value);
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
