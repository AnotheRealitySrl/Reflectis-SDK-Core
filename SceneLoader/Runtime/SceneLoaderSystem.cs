using SPACS.Core;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SPACS.Utilities.SceneLoader.Runtime
{
    [CreateAssetMenu(menuName = "AnotheReality/Systems/Utilities/SceneLoader", fileName = "SceneLoaderConfig")]
    public class SceneLoaderSystem : BaseSystem
    {
        public bool IsLoading { get; private set; }
        public UnityEvent<AsyncOperationHandle> LoadingAddressableEvent { get; private set; } = new();

        public override void Init()
        {
            //activeScenes.Add(SceneManager.GetActiveScene());
        }

        #region Public methods

        public void LoadScene(string sceneName, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null)
        {
            IsLoading = true;
            onBeforeLoadCallback?.Invoke();

            AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            loadSceneOperation.completed += (op) =>
            {
                onAfterLoadCallback?.Invoke();
                IsLoading = false;
            };
        }

        public void LoadScenesAdditive(List<string> scenesToLoad, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null)
        {
            IsLoading = true;
            onBeforeLoadCallback?.Invoke();

            int scenesLoaded = 0;
            foreach (string sceneName in scenesToLoad)
            {
                AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                loadSceneOperation.completed += (op) =>
                {
                    scenesLoaded++;
                    if (scenesLoaded == scenesToLoad.Count - 1)
                    {
                        onAfterLoadCallback?.Invoke();
                        IsLoading = false;
                        return;
                    }
                };
            }
        }

        public void UnloadScenesAdditive(List<string> scenesToUnload, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null)
        {
            IsLoading = true;
            onBeforeLoadCallback?.Invoke();

            int scenesLoaded = 0;
            foreach (string sceneName in scenesToUnload)
            {
                AsyncOperation loadSceneOperation = SceneManager.UnloadSceneAsync(sceneName);
                loadSceneOperation.completed += (op) =>
                {
                    scenesLoaded++;
                    if (scenesLoaded == scenesToUnload.Count - 1)
                    {
                        onAfterLoadCallback?.Invoke();
                        IsLoading = false;
                        return;
                    }
                };
            }
        }

        public async void LoadAddressableScene(string sceneName, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null)
        {
            IsLoading = true;
            onBeforeLoadCallback?.Invoke();

            AsyncOperationHandle<SceneInstance> loadSceneOperation = Addressables.LoadSceneAsync(sceneName);

            loadSceneOperation.Completed += (op) =>
            {
                switch (loadSceneOperation.Status)
                {
                    case AsyncOperationStatus.Succeeded:
                        onAfterLoadCallback?.Invoke();
                        IsLoading = false;
                        break;
                    case AsyncOperationStatus.Failed:
                        throw new Exception("Failed loading Scene");
                }
            };

            await InvokeLoadingEvent(loadSceneOperation);
        }

        public void LoadAddressableAdditiveScene(List<string> scenesToLoad, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null)
        {
            IsLoading = true;
            onBeforeLoadCallback?.Invoke();

            int scenesLoaded = 0;
            foreach (string sceneName in scenesToLoad)
            {
                AsyncOperationHandle<SceneInstance> loadSceneOperation = Addressables.LoadSceneAsync(sceneName);

                loadSceneOperation.Completed += (op) =>
                {
                    switch (loadSceneOperation.Status)
                    {
                        case AsyncOperationStatus.Succeeded:
                            scenesLoaded++;
                            if (scenesLoaded == scenesToLoad.Count - 1)
                            {
                                onAfterLoadCallback?.Invoke();
                                IsLoading = false;
                                return;
                            }
                            break;
                        case AsyncOperationStatus.Failed:
                            throw new Exception("Failed loading Scene");
                    }
                };
            }
        }

        public void UnloadAddressableAdditiveScene(List<string> scenesToUnload, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null)
        {
            //IsLoading = true;
            //onBeforeLoadCallback?.Invoke();

            //int scenesLoaded = 0;
            //foreach (string sceneName in scenesToUnload)
            //{
            //    AsyncOperationHandle<SceneInstance> loadSceneOperation = Addressables.UnloadSceneAsync(sceneName);

            //    loadSceneOperation.Completed += (op) =>
            //    {
            //        switch (loadSceneOperation.Status)
            //        {
            //            case AsyncOperationStatus.Succeeded:
            //                scenesLoaded++;
            //                if (scenesLoaded == scenesToUnload.Count - 1)
            //                {
            //                    onAfterLoadCallback?.Invoke();
            //                    IsLoading = false;
            //                    return;
            //                }
            //                break;
            //            case AsyncOperationStatus.Failed:
            //                throw new Exception("Failed loading Scene");
            //        }
            //    };
            //}
        }

        #endregion

        #region Private methods

        async Task InvokeLoadingEvent(AsyncOperationHandle<SceneInstance> loadSceneOperation)
        {
            await Task.Run(() =>
            {
                do
                    LoadingAddressableEvent.Invoke(loadSceneOperation);
                while
                    (!loadSceneOperation.IsDone);
            });
           
            
        }

        #endregion
    }
}
