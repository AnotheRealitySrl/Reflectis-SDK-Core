using Reflectis.SDK.Utilities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Reflectis.SDK.Fade
{
    public class CanvasFadeManager : MonoBehaviour, IFadeManager
    {
        #region Inspector variables

        [Header("Configuration")]
        [SerializeField] private GameObject canvas;
        [SerializeField] private Image fadeVolume;

        #endregion

        #region Private variables

        private Coroutine blackCoroutine = null;
        private Coroutine desaturatedCoroutine = null;

        #endregion

        #region Interface implementation

        public float FadeTime { get; set; }
        public bool FadeOnStart { get; set; }

        public ILayerManager LayerManager { get; set; }

        public UnityEvent OnFadeStart { get; } = new();
        public UnityEvent OnFadeEnd { get; } = new();

        public void Init()
        {
            if (FadeOnStart)
            {
                FadeFromBlack();
            }
        }

        public void FadeToBlack(System.Action onEnd = null)
        {
            if (fadeVolume != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                canvas.SetActive(true);
                LayerManager.MoveObjectsToLayer();
                blackCoroutine = StartCoroutine(FadeVolumeWeight(fadeVolume, fadeVolume.color.a, 1, FadeTime * (1f - fadeVolume.color.a), onEnd));
            }
        }

        public void FadeFromBlack(System.Action onEnd = null)
        {
            if (fadeVolume != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                blackCoroutine = StartCoroutine(FadeVolumeWeight(fadeVolume, fadeVolume.color.a, 0, FadeTime * fadeVolume.color.a, () =>
                {
                    onEnd?.Invoke();
                    LayerManager.ResetObjectsLayer();
                    canvas.SetActive(false);
                }));
            }
        }


        public void FadeToDesaturated(Action onEnd = null)
        {
            throw new NotImplementedException();
        }

        public void FadeFromDesaturated(Action onEnd = null)
        {
            throw new NotImplementedException();
        }


        public void InterruptFade()
        {
            if (blackCoroutine != null)
                StopCoroutine(blackCoroutine);

            if (desaturatedCoroutine != null)
                StopCoroutine(desaturatedCoroutine);

            StopAllCoroutines();
        }
        #endregion

        #region Private methods

        // I would do this with a tween, but unfortunately, Volume.weight can't be modified with a FloatTweener
        IEnumerator FadeVolumeWeight(Image volume, float startWeight, float endWeight, float fadeTime, System.Action onEnd)
        {
            Color newColor = volume.color;

            yield return null;
            if (fadeTime > 0f)
            {
                for (float t = 0; t < 1; t += Time.deltaTime / fadeTime)
                {
                    newColor.a = EasingFunctions.Linear(startWeight, endWeight, t);
                    volume.color = newColor;
                    yield return null;
                }
            }

            newColor.a = endWeight;
            volume.color = newColor;
            yield return null;
            onEnd?.Invoke();
            yield return null;
        }
        #endregion
    }
}

