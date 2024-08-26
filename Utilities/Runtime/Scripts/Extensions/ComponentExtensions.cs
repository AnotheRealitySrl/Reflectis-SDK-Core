using UnityEngine;

namespace Reflectis.SDK.Utilities
{
    public static class ComponentExtensions
    {
        public static T GetComponentInactive<T>(this Component component) where T : Component
        {
            T componentToGet = component.GetComponentInChildren<T>(true);
            if (componentToGet != null && componentToGet.gameObject == component.gameObject)
            {
                return componentToGet;
            }
            return null;
        }
    }
}
