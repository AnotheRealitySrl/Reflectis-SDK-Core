using Reflectis.SDK.Core.SystemFramework;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Core.Fade
{
    [CreateAssetMenu(menuName = "Reflectis/SDK-Fade/FadeSystemConfig", fileName = "FadeSystemConfig")]
    public class FadeSystem : BaseSystem, IFadeSystem
    {
        #region Inspector variables

        [Header("Configuration")]
        [SerializeField, Tooltip("Reference to the fade manager prefab")]
        private GameObject fadeManagerPrefab;

        [SerializeField, Tooltip("Fade time")]
        private float fadeTime = 1f;

        [SerializeField, Tooltip("Performs fade in during initialization")]
        private bool fadeOnStart = true;

        [SerializeField, Tooltip("Reference to the layer manager, i.e. the object that manages any objects unaffected by fade.")]
        private LayerManagerBase layerManager;

        #endregion

        #region Private variables

        private IFadeManager fadeManager;

        #endregion

        #region System implementation

        public override Task Init()
        {
            if (!Instantiate(fadeManagerPrefab, Camera.main.transform).TryGetComponent(out fadeManager))
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
            return base.Init();
        }

        #endregion

        #region Public API

        public void FadeToBackground(Action onEnd = null) => fadeManager.FadeToBackground(onEnd);
        public void FadeFromBackground(Action onEnd = null) => fadeManager.FadeFromBackground(onEnd);
        public void FadeToBlack(Action onEnd = null) => fadeManager.FadeToBlack(onEnd);
        public void FadeFromBlack(Action onEnd = null) => fadeManager.FadeFromBlack(onEnd);
        public void FadeToDesaturated(Action onEnd = null) => fadeManager.FadeToDesaturated(onEnd);
        public void FadeFromDesaturated(Action onEnd = null) => fadeManager.FadeFromDesaturated(onEnd);

        public void UpdateObjsUnaffectedByFade(List<GameObject> objsUnaffectedByFade) => fadeManager.LayerManager?.UpdateObjsUnaffectedByFade(objsUnaffectedByFade);
        public void ResetObjsUnaffectedByFade() => fadeManager.LayerManager?.ResetObjsUnaffectedByFade();

        public void InterruptFade() => fadeManager.InterruptFade();
        public void SetTargetCamera(Camera camera) => fadeManager.SetTargetCamera(camera);
        #endregion
    }
}