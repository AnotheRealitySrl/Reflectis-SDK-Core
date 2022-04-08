using UnityEngine;
using UnityEngine.Events;

namespace SPACS.Logic.Transformers
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// This transformer applies a multiplier and an offset to a float value
    /// and fires the transformed value into an event
    /// </summary>
    public class FloatToFloat : MonoBehaviour
    {
        [Tooltip("The multiplier to apply on the input float")]
        [SerializeField]
        private float multiplier = 1.0f;

        [Tooltip("The offset to apply on the input float")]
        [SerializeField]
        private float offset = 0.0f;

        [Tooltip("The event fired when the transformed value has been calculated")]
        [SerializeField]
        private UnityEvent<float> OnProcess = default;

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Call this method to provide the input value to this transformer. The 
        /// input value will be processed and the resulting value will be fired
        /// with the OnProcess event
        /// </summary>
        /// <param name="f">The input value</param>
        public void Process(float f)
        {
            if (!gameObject.activeInHierarchy)
                return;

            float result = (offset + f) * multiplier;
            OnProcess.Invoke(result);
        }
    }
}