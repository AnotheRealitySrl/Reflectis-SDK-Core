using System;
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.Core
{

    /// <summary>
    /// System manager instance.
    /// </summary>
    public class SM
    {

        #region Systems

        public static Action OnAllSystemsSetupsDone;

        public static List<ISystem> CurrentSystems { get; set; }

        /// <summary>
        /// Instantiated (if required) and initializes a list of systems
        /// </summary>
        /// <param name="systems"></param>
        public static void LoadAndSetup(List<BaseSystem> systems)
        {
            CurrentSystems = new List<ISystem>();
            for (int i = 0; i < systems.Count; i++)
            {
                BaseSystem system = systems[i];
                if (system != null)
                {
                    BaseSystem systemInstance = system.RequiresNewInstance ? ScriptableObject.Instantiate(system) : system;
                    CurrentSystems.Add(systemInstance);
                    _ = InitSystem(systemInstance, null);
                }
                else
                {
                    // Warning! The system in position [i] is not valid!
                    Debug.LogWarning($"[SystemManager] System not valid in SystemManagerController, index [{i}].");
                }
            }
            OnAllSystemsSetupsDone?.Invoke();
        }

        /// <summary>
        /// Initializes a system
        /// </summary>
        /// <param name="systemToInitialize"></param>
        /// <param name="parentSystem"></param>
        /// <returns></returns>
        private static ISystem InitSystem(ISystem systemToInitialize, ISystem parentSystem)
        {

            systemToInitialize.InitInternal(parentSystem);
            foreach (ISystem subSystem in systemToInitialize.SubSystems)
            {
                if (subSystem.AutoInitAtStartup)
                {
                    BaseSystem systemInstance = subSystem.RequiresNewInstance ? ScriptableObject.Instantiate(subSystem as BaseSystem) : subSystem as BaseSystem;
                    CurrentSystems.Add(systemInstance);
                    _ = InitSystem(systemInstance, systemToInitialize);
                }
            }
            return systemToInitialize;
        }

        /// <summary>
        /// Get an instantiated system of type T/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetSystem<T>() where T : ISystem
        {
            ISystem returnSystem = CurrentSystems.Find(s => s.GetType() == typeof(T) || typeof(T).IsAssignableFrom(s.GetType()));
            return (T)returnSystem;
        }

        #endregion
    }

}
