using SPACS.Tween;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SPACS.Utilities.FadeSystem.Runtime
{
    public class VolumeManager : MonoBehaviour, IFadeManager
    {
        #region Inspector variables

        [Header("Configuration")]
        [SerializeField] private Volume fadeVolume;
        [SerializeField] private Volume desaturateVolume;

        #endregion

        #region Private variables

        private Coroutine blackCoroutine = null;
        private Coroutine desaturatedCoroutine = null;

        #endregion

        #region Interface implementation

        public string UnaffectedByFadeLayerName { get; set; }
        public List<GameObject> ObjsUnaffectedByFade { get; set; }
        public float FadeTime { get; set; }

        public void FadeToBlack(System.Action onEnd = null)
        {
            if (fadeVolume != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                foreach (var go in ObjsUnaffectedByFade)
                {
                    go.layer = LayerMask.NameToLayer(UnaffectedByFadeLayerName);
                }


                blackCoroutine = StartCoroutine(FadeVolumeWeight(fadeVolume, fadeVolume.weight, 1, FadeTime * (1f - fadeVolume.weight), onEnd));
            }
        }

        public void FadeFromBlack(System.Action onEnd = null)
        {
            if (fadeVolume != null)
            {
                if (blackCoroutine != null)
                    StopCoroutine(blackCoroutine);

                blackCoroutine = StartCoroutine(FadeVolumeWeight(fadeVolume, fadeVolume.weight, 0, FadeTime * fadeVolume.weight, () =>
                {
                    onEnd?.Invoke();

                    foreach (var go in ObjsUnaffectedByFade)
                    {
                        go.layer = 0;
                    }
                }));
            }
        }

        public void FadeToDesaturated(System.Action onEnd = null)
        {
            if (desaturateVolume != null)
            {
                if (desaturatedCoroutine != null)
                    StopCoroutine(desaturatedCoroutine);

                foreach (var go in ObjsUnaffectedByFade)
                {
                    go.layer = LayerMask.NameToLayer(UnaffectedByFadeLayerName);
                }

                desaturatedCoroutine = StartCoroutine(FadeVolumeWeight(desaturateVolume, desaturateVolume.weight, 1, FadeTime * (1f - desaturateVolume.weight), onEnd));
            }
        }

        public void FadeFromDesaturated(System.Action onEnd = null)
        {
            if (desaturateVolume != null)
            {
                if (desaturatedCoroutine != null)
                    StopCoroutine(desaturatedCoroutine);

                desaturatedCoroutine = StartCoroutine(FadeVolumeWeight(desaturateVolume, desaturateVolume.weight, 0, FadeTime * desaturateVolume.weight, () =>
                {
                    onEnd?.Invoke();

                    foreach (var go in ObjsUnaffectedByFade)
                    {
                        go.layer = 0;
                    }
                }));
            }
        }

        #endregion

        #region Private methods

        // I would do this with a tween, but unfortunately, Volume.weight can't be modified with a FloatTweener
        IEnumerator FadeVolumeWeight(Volume volume, float startWeight, float endWeight, float fadeTime, System.Action onEnd)
        {
            if (fadeTime > 0f)
            {
                for (float t = 0; t < 1; t += Time.deltaTime / fadeTime)
                {
                    volume.weight = EasingFunctions.EaseInOutQuad(startWeight, endWeight, t);
                    yield return null;
                }
            }
            volume.weight = endWeight;
            onEnd?.Invoke();
            yield return null;
        }

        private bool TryGetLayerByName(string layerName, out int layerMask)
        {
            layerMask = LayerMask.NameToLayer(layerName);
            return layerMask > -1;
        }

        #endregion
    }
}
