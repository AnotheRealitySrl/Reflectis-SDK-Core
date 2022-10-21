using System;
using System.Collections;
using UnityEngine;

namespace SPACS.Toolkit.Extensions.Runtime
{
    /// <summary>
    /// Extend base system Action.
    /// </summary>
    public static class MonoBehaviourExtensions {

        /// <summary>
        /// Invoke action at next frame.
        /// </summary>
        public static void InvokeNextFrame(this MonoBehaviour mono, Action action)
        {
            mono.StartCoroutine(CoroInvokeNextFrame(action));
        }

        private static IEnumerator CoroInvokeNextFrame(Action action)
        {
            yield return null;
            action();
        }

        /// <summary>
        /// Invoke action after some time.
        /// </summary>
        public static void InvokeAfter(this MonoBehaviour mono, float seconds, Action action)
        {
            mono.StartCoroutine(CoroInvokeAfter(seconds, action));
        }

        private static IEnumerator CoroInvokeAfter(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action();
        }

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            if (go.TryGetComponent<T>(out T result))
            {
                return result;
            }
            else
            {
                return go.AddComponent<T>();
            }
        }
    }

}
