using UnityEngine;
using UnityEngine.Events;

namespace SPACS.Utilities
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// This transformer converts a float value to a boolean value. The
    /// transformed value will be fired into an event
    /// </summary>
    public class FloatToBoolean : MonoBehaviour
    {
        [Tooltip("The threshold to use as true/false separator")]
        [SerializeField]
        private float threshold = 0.5f;

        [Tooltip("The event fired when the transformed value has been calculated")]
        [SerializeField]
        private UnityEvent<bool> OnProcess = default;

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

            OnProcess.Invoke(f > threshold);
        }
    }
}