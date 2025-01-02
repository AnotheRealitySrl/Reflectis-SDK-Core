using UnityEngine;

namespace Reflectis.SDK.Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        //Classe generica per i singleton
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    return null;
                }
            }
        }

        protected virtual void Awake()
        {
            instance = this as T;
        }
    }
}