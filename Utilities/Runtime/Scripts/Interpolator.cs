using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Reflectis.SDK.Utilities
{
    public class Interpolator
    {
        private float duration;

        private AnimationCurve ease;

        private Action<float> lerpFunction;

        private float currentTime = 0;

        private Task runningInterpolation = null;

        private bool runningForward = false;

        private bool runningBackward = false;


        public Interpolator(float duration, Action<float> lerpFunction, AnimationCurve ease = null)
        {
            this.duration = duration;
            this.lerpFunction = lerpFunction;
            this.ease = ease;
            if (ease == null)
            {
                this.ease = AnimationCurve.Constant(0, duration, 1);
            }
        }

        public async Task PlayForward()
        {
            Stop();
            runningInterpolation = PlayInterpolationForward();
            await runningInterpolation;
        }

        private async Task PlayInterpolationForward()
        {
            if (duration <= 0)
            {
                lerpFunction(1);
                return;
            }
            currentTime = 0;
            lerpFunction((currentTime / duration) /** ease.Evaluate(currentTime)*/);
            runningForward = true;
            while (currentTime < duration && runningForward)
            {
                currentTime = Math.Min(duration, currentTime + Time.deltaTime);
                await Task.Yield();
                lerpFunction(currentTime / duration * ease.Evaluate(currentTime / duration));
            }
        }

        public async Task PlayBackwards()
        {
            Stop();
            runningInterpolation = PlayInterpolationBackwards();
            await runningInterpolation;
        }

        private async Task PlayInterpolationBackwards()
        {
            if (duration <= 0)
            {
                lerpFunction(0);
                return;
            }
            lerpFunction(currentTime / duration * ease.Evaluate(currentTime));
            runningBackward = true;
            while (currentTime > 0 && runningBackward)
            {
                currentTime = Math.Max(0, currentTime - Time.deltaTime);
                await Task.Yield();
                lerpFunction(currentTime / duration * ease.Evaluate(currentTime / duration));
            }
        }

        public void Stop()
        {
            runningBackward = false;
            runningForward = false;
        }
    }
}
