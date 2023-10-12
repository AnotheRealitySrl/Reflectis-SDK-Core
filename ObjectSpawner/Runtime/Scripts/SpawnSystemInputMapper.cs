using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;
using Reflectis.SDK.Core;
using Reflectis.SDK.CreatorKit;

namespace Reflectis.SDK.ObjectSpawner
{
    public class SpawnSystemInputMapper : MonoBehaviour
    {
        protected SpawnableData spawnableData;

        protected void RegisterActionCallback(InputAction action)
        {
            action.Enable();
            action.started += ButtonActionCallback;
            action.performed += ButtonActionCallback;
            action.canceled += ButtonActionCallback;
        }

        protected void DeregisterActionCallback(InputAction action)
        {
            action.started -= ButtonActionCallback;
            action.performed -= ButtonActionCallback;
            action.canceled -= ButtonActionCallback;
            action.Disable();
        }

        protected void ButtonActionCallback(CallbackContext context)
        {
            if (context.action.enabled)
            {
                CallObjectSpawnerSystem(spawnableData);
            }
        }

        protected void CallObjectSpawnerSystem(SpawnableData data)
        {
            SM.GetSystem<ObjectSpawnerSystem>().CheckEntireFovAndSpawn(data);
        }

        protected static InputAction GetInputAction(InputActionReference actionReference)
        {
#pragma warning disable IDE0031 // Use null propagation -- Do not use for UnityEngine.Object types
            return actionReference != null ? actionReference.action : null;
#pragma warning restore IDE0031
        }
    }
}
