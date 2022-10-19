using SPACS.Core;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SPACS.Toolkit.FadeSystem.Runtime
{
    [CreateAssetMenu(menuName = "AnotheReality/Systems/Utilities/FadeSystem", fileName = "FadeSystemConfig")]
    public class FadeManagerSystem : BaseSystem
    {
        #region Inspector variables

        [Header("Configuration")]
        [SerializeField] private GameObject volumeManagerPrefab;
        [SerializeField] private float fadeTime = 1f;
        [SerializeField] private bool fadeOnStart = true;
        [SerializeField] private LayerManagerBase layerManager;

        #endregion

        #region private variables

        private IFadeManager fadeManager;

        #endregion

        #region System implementation

        public override void Init()
        {
            if (!Instantiate(volumeManagerPrefab, Camera.main.transform).TryGetComponent(out fadeManager))
            {
                throw new Exception("No fade manager specified");
            }

            fadeManager.FadeTime = fadeTime;
            fadeManager.FadeOnStart = fadeOnStart;

            if (layerManager)
            {
                fadeManager.LayerManager = layerManager;

                fadeManager.OnFadeStart?.AddListener(layerManager.MoveObjectsToLayer);
                fadeManager.OnFadeEnd?.AddListener(layerManager.ResetObjectsLayer);
            }

            fadeManager.Init();
        }

        #endregion

        #region Public API

        public void FadeToBlack(Action onEnd = null) => fadeManager.FadeToBlack(onEnd);
        public void FadeFromBlack(Action onEnd = null) => fadeManager.FadeFromBlack(onEnd);
        public void FadeToDesaturated(Action onEnd = null) => fadeManager.FadeToDesaturated(onEnd);
        public void FadeFromDesaturated(Action onEnd = null) => fadeManager.FadeFromDesaturated(onEnd);

        public void UpdateObjsUnaffectedByFade(List<GameObject> objsUnaffectedByFade) => fadeManager.LayerManager?.UpdateObjsUnaffectedByFade(objsUnaffectedByFade);
        public void ResetObjsUnaffectedByFade() => fadeManager.LayerManager?.ResetObjsUnaffectedByFade();

        #endregion
    }

}