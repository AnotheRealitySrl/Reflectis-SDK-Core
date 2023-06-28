using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SPACS.SDK.SceneLoader
{
    public interface ISceneLoaderSystem
    {
        void LoadScene(string sceneName, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null);

        void LoadScenesAdditive(List<string> scenesToLoad, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null);

        void UnloadScenesAdditive(List<string> scenesToUnload, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null);

        void LoadAddressableScene(string sceneName, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null);

        void LoadAddressableScenesAdditive(List<string> scenesToLoad, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null);

        void UnloadAddressableScenesAdditive(List<string> scenesToUnload, UnityAction onBeforeLoadCallback = null, UnityAction onAfterLoadCallback = null);
    }
}
