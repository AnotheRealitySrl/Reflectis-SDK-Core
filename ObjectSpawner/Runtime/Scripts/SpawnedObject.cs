using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Reflectis.SDK.ObjectSpawner
{
    public class SpawnedObject : MonoBehaviour
    {
        internal ObjectSpawnerController objectSpawnerManager;

        public bool isAlive;

        public bool isGettingDestroyed;

        public bool isGettingSpawned;

        public List<Func<Task>> beforeDestructionFunctions = new List<Func<Task>>();

        public async Task Dispose()
        {
            isGettingDestroyed = true;
            IEnumerable<Task> destroyTasks = beforeDestructionFunctions?.Select(async x => await x());
            await Task.WhenAll(destroyTasks);
            isGettingDestroyed = false;
            isAlive = false;
            Destroy(this);
        }
    }
}
