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
                duration, LerpFunction, GetStartTime, ease);
        }

        private float GetStartTime()
        {
            float currentPosValue = startPosition != position ?
                Vector3.Distance(transform.localPosition, startPosition) / Vector3.Distance(startPosition, position)
                : 0;
            float currentRotValue = startRotation.eulerAngles != rotation ?
                Vector3.Distance(transform.rotation.eulerAngles, startRotation.eulerAngles) / Vector3.Distance(startRotation.eulerAngles, rotation)
                : 0;
            float currentScaleValue = startScale != scale ?
                Vector3.Distance(transform.localScale, startScale) / Vector3.Distance(startScale, scale)
                : 0;
            float value = Mathf.Max(currentPosValue, currentRotValue, currentScaleValue);

            return interpolator.InverseEase.Evaluate(value) * duration;
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
