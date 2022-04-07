using UnityEngine;
using UnityEngine.Events;
#if !UNITY_2020_1_OR_NEWER
using static SPACS.Logic.Events.BasicEvents;
#endif

namespace SPACS.Logic.Transformers
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// This transformer converts a Vector2 value to a float value by selecting
    /// the single axis to consider. The transformed value will be fired into
    /// an event
    /// </summary>
    public class Vector2ToFloat : MonoBehaviour
    {
        public enum EAxis
        {
            X = 0,
            Y,
        }

        [SerializeField, Tooltip("The axis to keep")]
        private EAxis axis = default;

        [SerializeField, Tooltip("The event fired when the transformed value has been calculated")]
#if UNITY_2020_1_OR_NEWER
        private UnityEvent<float> OnProcess = default;
#else
        private FloatUnityEvent OnProcess = default;
#endif

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Call this method to provide the input value to this transformer. The 
        /// input value will be processed and the resulting value will be fired
        /// with the OnProcess event
        /// </summary>
        /// <param name="b">The input value</param>
        public void Process(Vector2 b)
        {
            float result = (axis == EAxis.X ? b.x : b.y);
            OnProcess.Invoke(result);
        }
    }
}