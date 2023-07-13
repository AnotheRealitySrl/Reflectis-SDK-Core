using Reflectis.SDK.Utilities;

using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Events;


namespace Reflectis.SDK.Fade
{
    public class QuadFadeManager : MonoBehaviour, IFadeManager
    {
        #region Inspector variables

        [Header("Configuration")]
        [SerializeField] private Color fadeColor;
        [SerializeField] private Color desaturatedColor;

        #endregion

        #region Private variables

        private Coroutine blackCoroutine = null;
        private Coroutine desaturatedCoroutine = null;

        private Material quadMaterial;

        #endregion

        #region Interface implementation

        public float FadeTime { get; set; }
        public bool FadeOnStart { get; set; }

        public ILayerManager LayerManager { get; set; }

        public UnityEvent OnFadeStart { get; } = new();
        public UnityEvent OnFadeEnd { get; } = new();

        public void Init()
        {
            gameObject.transform.localPosition = new Vector3(0, 0, 0.03f);

            quadMaterial = GetComponent<MeshRenderer>().materials[0];
            quadMaterial.color = fadeColor;

            if (FadeOnStart)
            {
                FadeFromBlack();
            }
        }

        public void FadeToBlack(System.Action onEnd = null)
        {
            if (quadMaterial != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                gameObject.SetActive(true);
                LayerManager.MoveObjectsToLayer();
                blackCoroutine = StartCoroutine(FadeVolumeWeight(quadMaterial, quadMaterial.color.a, 1, FadeTime * (1f - quadMaterial.color.a), onEnd));
            }
        }

        public void FadeFromBlack(System.Action onEnd = null)
        {
            if (quadMaterial != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                blackCoroutine = StartCoroutine(FadeVolumeWeight(quadMaterial, quadMaterial.color.a, 0, FadeTime * quadMaterial.color.a, () =>
                {
                    onEnd?.Invoke();
                    LayerManager.ResetObjectsLayer();
                    gameObject.SetActive(false);
                }));
            }
        }


        public void FadeToDesaturated(Action onEnd = null)
        {
            if (quadMaterial != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                gameObject.SetActive(true);
                LayerManager.MoveObjectsToLayer();
                blackCoroutine = StartCoroutine(FadeVolumeWeight(quadMaterial, quadMaterial.color.a, 0.5f, FadeTime * (1f - quadMaterial.color.a), onEnd));
            }
        }

        public void FadeFromDesaturated(Action onEnd = null)
        {
            if (quadMaterial != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                blackCoroutine = StartCoroutine(FadeVolumeWeight(quadMaterial, quadMaterial.color.a, 0, FadeTime * quadMaterial.color.a, () =>
                {
                    onEnd?.Invoke();
                    LayerManager.ResetObjectsLayer();
                    gameObject.SetActive(false);
                }));
            }
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
        IEnumerator FadeVolumeWeight(Material material, float startWeight, float endWeight, float fadeTime, System.Action onEnd)
        {
            Color newColor = quadMaterial.color;

            yield return null;
            if (fadeTime > 0f)
            {
                for (float t = 0; t < 1; t += Time.deltaTime / fadeTime)
                {
                    newColor.a = EasingFunctions.Linear(startWeight, endWeight, t);
                    quadMaterial.color = newColor;
                    yield return null;
                }
            }

            newColor.a = endWeight;
            quadMaterial.color = newColor;
            yield return null;
            onEnd?.Invoke();
            yield return null;
        }
        #endregion
    }
}

