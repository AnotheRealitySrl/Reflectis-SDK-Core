using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

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

        private Sequence sequence;


        private void Awake()
        {
            CreateSequence();
        }
        public override async Task EnterTransitionAsync()
        {
            onEnterTransition?.Invoke();
            sequence.PlayForward();
            await sequence.AsyncWaitForCompletion();
        }

        public override async Task ExitTransitionAsync()
        {
            sequence.PlayBackwards();
            await sequence.AsyncWaitForCompletion();
            onExitTransition?.Invoke();
        }

        private void CreateSequence()
        {
            sequence = DOTween.Sequence().Pause();
            
            sequence.SetAutoKill(false);
            sequence.Insert(0, transform.DOScale(scale, duration));
            sequence.Insert(0, transform.DOLocalMove(transform.localPosition + position, duration));
            sequence.Insert(0, transform.DOLocalRotate(transform.localRotation.eulerAngles + rotation, duration));
            if (ease == null)
                ease = AnimationCurve.Constant(0, duration, 1);

            sequence.SetEase(ease);
        }
    }
}
