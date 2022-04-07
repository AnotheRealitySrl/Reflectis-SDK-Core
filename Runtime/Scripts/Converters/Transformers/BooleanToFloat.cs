using UnityEngine;
using UnityEngine.Events;
#if !UNITY_2020_1_OR_NEWER
using static SPACS.Logic.Events.BasicEvents;
#endif

namespace SPACS.Logic.Transformers
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// This transformer converts a boolean value to a float value and applies
    /// a multiplier and an offset to the result. The transformed value will
    /// be fired into an event
    /// </summary>
    public class BooleanToFloat : MonoBehaviour
    {
        [Tooltip("The multiplier to apply on the converted float")]
        [SerializeField]
        private float multiplier = 1.0f;

        [Tooltip("The offset to apply on the converted float")]
        [SerializeField]
        private float offset = 0.0f;

        [Tooltip("The event fired when the transformed value has been calculated")]
        [SerializeField]
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
        public void Process(bool b)
        {
            if (!gameObject.activeInHierarchy)
                return;
            
            float result = (offset + (b ? 1.0f : 0.0f)) * multiplier;
            OnProcess.Invoke(result);
        }
    }
}