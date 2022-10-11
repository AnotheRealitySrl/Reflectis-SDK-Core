using SPACS.Core;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SPACS.Utilities.FadeSystem.Runtime
{
    [CreateAssetMenu(menuName = "AnotheReality/Systems/Utilities/FadeSystem", fileName = "FadeSystemConfig")]
    public class FadeSystem : BaseSystem
    {
        #region Inspector variables

        //[Header("Fade system references")]
        //[SerializeField] private Camera camera;

        [Header("Component")]
        [SerializeField] private GameObject volumeManagerPrefab;

        [Header("Configuration")]
        [SerializeField] private float fadeTime;
        [SerializeField] private string unaffectedByFadeLayerName;
        [SerializeField] private List<GameObject> objsUnaffectedByFade;

        #endregion

        #region private variables

        private IFadeManager fadeManager;

        #endregion

        #region System implementation

        public override void Init()
        {
            if (!Instantiate(volumeManagerPrefab, Camera.main.transform).TryGetComponent(out fadeManager))
            {
                throw new Exception("No Volume manager specified");
            }

            fadeManager.FadeTime = fadeTime;
            fadeManager.UnaffectedByFadeLayerName = unaffectedByFadeLayerName;
            fadeManager.ObjsUnaffectedByFade = objsUnaffectedByFade;
        }

        #endregion

        #region Public API

        public void FadeToBlack(Action onEnd = null) => fadeManager.FadeToBlack(onEnd);

        public void FadeFromBlack(Action onEnd = null) => fadeManager.FadeFromBlack(onEnd);

        public void FadeToDesaturated(Action onEnd = null) => fadeManager.FadeToDesaturated(onEnd);

        public void FadeFromDesaturated(Action onEnd = null) => fadeManager.FadeFromDesaturated(onEnd);

        #endregion
    }

}