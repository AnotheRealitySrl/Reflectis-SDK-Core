using System;

namespace Reflectis.SDK.Core
{
    public static class ActionExtension
    {
        public static void AddListenerOnce(this Action ogAction, Action addAction)
        {
            Action actionAndRemove = null;
            actionAndRemove = () =>
            {
                addAction?.Invoke();
                ogAction -= actionAndRemove;
            };
            ogAction += (actionAndRemove);
        }

        public static void AddListenerOnce<T>(this Action<T> action, Action<T> newAction)
        {
            Action<T> actionAndRemove = null;
            actionAndRemove = (T argument) =>
            {
                newAction?.Invoke(argument);
                action -= actionAndRemove;
            };
            action += actionAndRemove;
        }

        public static void AddListenerOnce<T0, T1>(this Action<T0, T1> action, Action<T0, T1> newAction)
        {
            Action<T0, T1> actionAndRemove = null;
            actionAndRemove = (T0 argument1, T1 argument2) =>
            {
                newAction?.Invoke(argument1, argument2);
                action -= actionAndRemove;
            };
            action -= actionAndRemove;
        }
    }
}
