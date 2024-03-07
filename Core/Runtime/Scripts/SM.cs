using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Reflectis.SDK.Core
{

    /// <summary>
    /// System manager instance.
    /// </summary>
    public class SM
    {
        #region Readiness

        public static Action OnAllSystemsSetupsDone;

        public static bool IsReady { get; private set; } = false;

        public static void DoOnceReady(Action callback)
        {
            if (callback == null)
            {
                Debug.LogWarning("[SM] Trying to execute null callback!");
                return;
            }

            // In case of valid callback, manage its execution.
            if (IsReady)
            {
                callback.Invoke();
            }
            else
            {
                OnAllSystemsSetupsDone += callback;
            }
        }

        #endregion


        #region Systems

        public static List<ISystem> CurrentSystems { get; set; }

        /// <summary>
        /// Instantiated (if required) and initializes a list of systems
        /// </summary>
        /// <param name="systems"></param>
        public static async void LoadAndSetup(List<BaseSystem> systems)
        {
            IsReady = false;

            CurrentSystems = new List<ISystem>();
            for (int i = 0; i < systems.Count; i++)
            {
                BaseSystem system = systems[i];
                if (system != null)
                {

                    BaseSystem systemInstance = system.RequiresNewInstance ? ScriptableObject.Instantiate(system) : system;
                    CurrentSystems.Add(systemInstance);
                    if (system.AutoInitAtStartup)
                    {
                        _ = await InitSystem(systemInstance, null);
                    }
                }
                else
                {
                    // Warning! The system in position [i] is not valid!
                    Debug.LogWarning($"[SystemManager] System not valid in SystemManagerController, index [{i}].");
                }
            }

            //while (CurrentSystems.Exists(x => x.AutoInitAtStartup && !x.IsInit))
            //{
            //    Debug.Log($"System {string.Join("", CurrentSystems.Where(x => x.AutoInitAtStartup && !x.IsInit).Select(x => "|" + x.ToString() + "|").ToList())} not initialized yet");
            //    await Task.Yield();
            //}

            IsReady = true;

            OnAllSystemsSetupsDone?.Invoke();
        }

        /// <summary>
        /// Initializes a system
        /// </summary>
        /// <param name="systemToInitialize"></param>
        /// <param name="parentSystem"></param>
        /// <returns></returns>
        private static async Task<ISystem> InitSystem(ISystem systemToInitialize, ISystem parentSystem)
        {
            await systemToInitialize.InitInternal(parentSystem);
            foreach (ISystem subSystem in systemToInitialize.SubSystems)
            {
                //if (subSystem.AutoInitAtStartup)
                //{
                BaseSystem systemInstance = subSystem.RequiresNewInstance ? ScriptableObject.Instantiate(subSystem as BaseSystem) : subSystem as BaseSystem;
                CurrentSystems.Add(systemInstance);
                _ = await InitSystem(systemInstance, systemToInitialize);
                //}
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
        /// <summary>
        /// Get an instantiated system of type T/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ISystem GetSystem(ISystem system)
        {
            ISystem returnSystem = CurrentSystems.Find(s => s.GetType() == system.GetType() || system.GetType().IsAssignableFrom(s.GetType()));
            return returnSystem;
        }

        #endregion
    }

}
