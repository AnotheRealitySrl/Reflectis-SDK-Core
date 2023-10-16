using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;
using Reflectis.SDK.Core;
using Reflectis.SDK.CreatorKit;
using System.Threading.Tasks;
using System.Linq;
using System;
using UnityEngine.AddressableAssets;
using Sirenix.Utilities;

namespace Reflectis.SDK.ObjectSpawner
{
    public abstract class ObjectSpawnerManager : MonoBehaviour, IRuntimeComponent
    {
        protected SpawnableData spawnableData;

        private List<SpawnedObject> spawnedObjects = new List<SpawnedObject>();

        public Task Init(SceneComponentPlaceholderBase placeholder)
        {
            spawnableData = (placeholder as SpawnableObjPlaceholder).Data;

            RegisterActionCallback(GetInputAction());

            return Task.CompletedTask;
        }

        protected virtual void RegisterActionCallback(InputAction action)
        {
            action.Enable();
            action.started += ButtonActionCallback;
            action.performed += ButtonActionCallback;
            action.canceled += ButtonActionCallback;
        }

        protected virtual void DeregisterActionCallback(InputAction action)
        {
            action.started -= ButtonActionCallback;
            action.performed -= ButtonActionCallback;
            action.canceled -= ButtonActionCallback;
            action.Disable();
        }

        protected async void ButtonActionCallback(CallbackContext context)
        {
            spawnedObjects = spawnedObjects.Where(x => x != null).ToList();
            if (context.action.enabled)
            {
                //There is an active gameobject that has to be destroyed instead of spawning a new one
                if(spawnableData.OnlyOneNpc && spawnedObjects.Any((x) => x.isAlive && !x.isGettingDestroyed))
                {
                    await spawnedObjects.FirstOrDefault((x) => x.isAlive && !x.isGettingDestroyed).Dispose();
                }
                else
                {
                    if(spawnableData.OnlyOneNpc && spawnedObjects.Any((x) => (x.isAlive && x.isGettingDestroyed) || x.isGettingSpawned))
                    {
                        return;
                    }
                    await SpawnObj(spawnableData);
                }
            }
        }

        protected async Task SpawnObj(SpawnableData data)
        {
            GameObject spawned = SM.GetSystem<ObjectSpawnerSystem>().CheckEntireFovAndSpawn(data);
            if (spawned == null)
            {
                return;
            }
            var spawnedObject = spawned.AddComponent<SpawnedObject>();
            spawnedObjects.Add(spawnedObject);
            SceneComponentsMapper mapper = await Addressables.LoadAssetAsync<SceneComponentsMapper>("LearningSpaceComponentsMapper").Task;
            
            spawnedObject.isAlive = true;

            spawnedObject.objectSpawnerManager = this;
            foreach (SceneComponentPlaceholderBase placeholder in spawned.GetComponentsInChildren<SceneComponentPlaceholderBase>(true))
            {
                await placeholder.Init(mapper);
            }
        }


        protected InputAction GetInputAction()
        {
            InputActionReference actionReference = GetInputActionReference();
#pragma warning disable IDE0031 // Use null propagation -- Do not use for UnityEngine.Object types
            return actionReference != null ? actionReference.action : null;
#pragma warning restore IDE0031
        }

        protected abstract InputActionReference GetInputActionReference();

        private void OnDestroy()
        {
            DeregisterActionCallback(spawnableData.DesktopInput);
        }

    }
}
