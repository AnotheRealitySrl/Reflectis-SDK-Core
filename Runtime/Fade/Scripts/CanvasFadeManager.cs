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
        [SerializeField] private Image fadeImage;
        [SerializeField] private Image desaturatedImage;
        [SerializeField] private Image backgroundImage;

        [Tooltip("Optional: changes the opacity of the material only if filled")]
        [SerializeField] private bool optionalMaterialOpacityParameter;
        [SerializeField, DrawIf(nameof(optionalMaterialOpacityParameter), true)]
        private string opacityParameter = "_Opacity";

        #endregion

        #region Private variables

        private Coroutine blackCoroutine = null;
        private Coroutine desaturatedCoroutine = null;
        private Coroutine backgroundCoroutine = null;

        private float maxDesaturatedAlpha;

        #endregion

        #region Interface implementation

        public float FadeTime { get; set; }
        public bool FadeOnStart { get; set; }

        public ILayerManager LayerManager { get; set; }

        public UnityEvent OnFadeStart { get; } = new();
        public UnityEvent OnFadeEnd { get; } = new();

        public void Init()
        {
            maxDesaturatedAlpha = desaturatedImage.color.a;
            desaturatedImage.color = new Color(desaturatedImage.color.r, desaturatedImage.color.g, desaturatedImage.color.b, 0);
            desaturatedImage.gameObject.SetActive(false);

            if (optionalMaterialOpacityParameter)
            {
                // Needed because otherwise the material asset is overridden
                backgroundImage.material = Instantiate(backgroundImage.material);
            }

            if (FadeOnStart)
            {
                FadeFromBackground();
            }
        }

        public void FadeToBackground(Action onEnd = null)
        {
            if (backgroundImage != null)
            {
                if (backgroundCoroutine != null)
                    StopCoroutine(backgroundCoroutine);

                canvas.SetActive(true);
                backgroundImage.gameObject.SetActive(true);
                fadeImage.gameObject.SetActive(false);
                LayerManager.MoveObjectsToLayer();

                if (!optionalMaterialOpacityParameter)
                {
                    backgroundCoroutine = StartCoroutine(FadeVolumeWeight(backgroundImage, backgroundImage.color.a, 1, FadeTime * (1f - backgroundImage.color.a), onEnd));
                }
                else
                {
                    backgroundCoroutine = StartCoroutine(FadeMaterialParameter(backgroundImage.material, backgroundImage.material.GetFloat(opacityParameter), 1, FadeTime * (1f - backgroundImage.material.GetFloat(opacityParameter)), onEnd));
                }

            }
        }

        public void FadeFromBackground(Action onEnd = null)
        {
            if (backgroundImage != null)
            {
                if (backgroundCoroutine != null)
                    StopCoroutine(backgroundCoroutine);

                fadeImage.gameObject.SetActive(false);

                if (!optionalMaterialOpacityParameter)
                {
                    backgroundCoroutine = StartCoroutine(FadeVolumeWeight(backgroundImage, backgroundImage.color.a, 0, FadeTime * backgroundImage.color.a, () =>
                    {
                        onEnd?.Invoke();
                        LayerManager.ResetObjectsLayer();
                        canvas.SetActive(false);
                        backgroundImage.gameObject.SetActive(false);
                    }));
                }
                else
                {
                    backgroundCoroutine = StartCoroutine(FadeMaterialParameter(backgroundImage.material, backgroundImage.material.GetFloat(opacityParameter), 0, FadeTime * backgroundImage.material.GetFloat(opacityParameter), () =>
                    {
                        onEnd?.Invoke();
                        LayerManager.ResetObjectsLayer();
                        canvas.SetActive(false);
                        backgroundImage.gameObject.SetActive(false);
                    }));
                }
            }
        }

        public void FadeToBlack(System.Action onEnd = null)
        {
            if (fadeImage != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                canvas.SetActive(true);
                fadeImage.gameObject.SetActive(true);
                backgroundImage.gameObject.SetActive(false);
                LayerManager.MoveObjectsToLayer();
                blackCoroutine = StartCoroutine(FadeVolumeWeight(fadeImage, fadeImage.color.a, 1, FadeTime * (1f - fadeImage.color.a), onEnd));
            }
        }

        public void FadeFromBlack(System.Action onEnd = null)
        {
            if (fadeImage != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                backgroundImage.gameObject.SetActive(false);

                blackCoroutine = StartCoroutine(FadeVolumeWeight(fadeImage, fadeImage.color.a, 0, FadeTime * fadeImage.color.a, () =>
                {
                    onEnd?.Invoke();
                    LayerManager.ResetObjectsLayer();
                    canvas.SetActive(false);
                    fadeImage.gameObject.SetActive(false);
                }));
            }
        }


        public void FadeToDesaturated(Action onEnd = null)
        {
            if (desaturatedImage != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                canvas.SetActive(true);
                desaturatedImage.gameObject.SetActive(true);
                LayerManager.MoveObjectsToLayer();
                blackCoroutine = StartCoroutine(FadeVolumeWeight(desaturatedImage, desaturatedImage.color.a, maxDesaturatedAlpha, FadeTime * (1f - desaturatedImage.color.a), onEnd));
            }
        }

        public void FadeFromDesaturated(Action onEnd = null)
        {
            if (desaturatedImage != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                blackCoroutine = StartCoroutine(FadeVolumeWeight(desaturatedImage, desaturatedImage.color.a, 0, FadeTime * desaturatedImage.color.a, () =>
                {
                    onEnd?.Invoke();
                    LayerManager.ResetObjectsLayer();
                    canvas.SetActive(false);
                    desaturatedImage.gameObject.SetActive(false);
                }));
            }
        }


        public void InterruptFade()
        {
            if (blackCoroutine != null)
                StopCoroutine(blackCoroutine);

            if (desaturatedCoroutine != null)
                StopCoroutine(desaturatedCoroutine);

            if (backgroundCoroutine != null)
                StopCoroutine(backgroundCoroutine);

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

        // I would do this with a tween, but unfortunately, Volume.weight can't be modified with a FloatTweener
        IEnumerator FadeMaterialParameter(Material material, float startWeight, float endWeight, float fadeTime, System.Action onEnd)
        {
            yield return null;
            if (fadeTime > 0f)
            {
                for (float t = 0; t < 1; t += Time.deltaTime / fadeTime)
                {
                    material.SetFloat(opacityParameter, EasingFunctions.Linear(startWeight, endWeight, t));
                    yield return null;
                }
            }

            material.SetFloat(opacityParameter, endWeight);
            yield return null;
            onEnd?.Invoke();
            yield return null;
        }

        public void SetTargetCamera(Camera camera)
        {

        }
        #endregion
    }
}

