using System;
using UnityEngine;
using UnityEngine.Events;

namespace SPACS.Logic.Events
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A collection of basic UnityEvents
    /// </summary>
#if UNITY_2020_1_OR_NEWER
    [System.Obsolete("Used before Unity 2020.")]
#endif
    public static class BasicEvents
    {
#if UNITY_2020_1_OR_NEWER
        [System.Obsolete("Used before Unity 2020. Use \"UnityEvent\" instead.")]
#endif
        [Serializable]
        public class TriggerUnityEvent : UnityEvent { }

#if UNITY_2020_1_OR_NEWER
        [System.Obsolete("Used before Unity 2020. Use \"UnityEvent<bool>\" instead.")]
#endif
        [Serializable]
        public class BooleanUnityEvent : UnityEvent<bool> { }

#if UNITY_2020_1_OR_NEWER
        [System.Obsolete("Used before Unity 2020. Use \"UnityEvent<int>\" instead.")]
#endif
        [Serializable]
        public class IntegerUnityEvent : UnityEvent<int> { }

#if UNITY_2020_1_OR_NEWER
        [System.Obsolete("Used before Unity 2020. Use \"UnityEvent<float>\" instead.")]
#endif
        [Serializable]
        public class FloatUnityEvent : UnityEvent<float> { }

#if UNITY_2020_1_OR_NEWER
        [System.Obsolete("Used before Unity 2020. Use \"UnityEvent<string>\" instead.")]
#endif
        [Serializable]
        public class StringUnityEvent : UnityEvent<string> { }

#if UNITY_2020_1_OR_NEWER
        [System.Obsolete("Used before Unity 2020. Use \"UnityEvent<Vector2>\" instead.")]
#endif
        [Serializable]
        public class Vector2UnityEvent : UnityEvent<Vector2> { }

#if UNITY_2020_1_OR_NEWER
        [System.Obsolete("Used before Unity 2020. Use \"UnityEvent<Vector3>\" instead.")]
#endif
        [Serializable]
        public class Vector3UnityEvent : UnityEvent<Vector3> { }

#if UNITY_2020_1_OR_NEWER
        [System.Obsolete("Used before Unity 2020. Use \"UnityEvent<Pose>\" instead.")]
#endif
        [Serializable]
        public class PoseUnityEvent : UnityEvent<Pose> { }

#if UNITY_2020_1_OR_NEWER
        [System.Obsolete("Used before Unity 2020. Use \"UnityEvent<Color>\" instead.")]
#endif
        [Serializable]
        public class ColorUnityEvent : UnityEvent<Color> { }

#if UNITY_2020_1_OR_NEWER
        [System.Obsolete("Used before Unity 2020. Use \"UnityEvent<GameObject>\" instead.")]
#endif
        [Serializable]
        public class GameObjectUnityEvent : UnityEvent<GameObject> { }
    }
}