using System;
using UnityEngine;
using UnityEngine.Events;
#if !UNITY_2020_1_OR_NEWER
using static SPACS.Logic.Events.BasicEvents;
#endif

namespace SPACS.Logic.Transformers
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// This transformer converts a boolean value to its inverted value. The
    /// transformed value will be fired into an event
    /// </summary>
    public class BooleanToBoolean : MonoBehaviour
    {
        [Serializable]
        public enum BooleanFilter { Any, OnlyTrue, OnlyFalse }

        [Tooltip("Only the values allowed by the filter will be processed")]
        [SerializeField]
        private BooleanFilter filter = BooleanFilter.Any;

        [Tooltip("If true, the value will be inverted")]
        [SerializeField]
        private bool inverted = default;

        [Tooltip("The event fired when the transformed value has been calculated")]
        [SerializeField]
#if UNITY_2020_1_OR_NEWER
        private UnityEvent<bool> OnProcess = default;
#else
        private BooleanUnityEvent OnProcess = default;
#endif


        /// <summary> If true, the value will be inverted </summary>
        public bool Inverted { get => inverted; set => inverted = value; }


        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Call this method to provide the input value to this transformer. The 
        /// input value will be processed and the resulting value will be fired
        /// with the OnProcess event
        /// </summary>
        /// <param name="b">The input value</param>
        public void Process(bool b)
        {
            if (gameObject.activeInHierarchy && ApplyFilter(b))
                OnProcess.Invoke(Inverted ? !b : b);
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Applies the specified filter to a value
        /// </summary>
        /// <param name="b">The value to process</param>
        /// <returns>True if the value satisfies the filter</returns>
        public bool ApplyFilter(bool b)
        {
            if (filter == BooleanFilter.OnlyTrue && b == false)
                return false;

            if (filter == BooleanFilter.OnlyFalse && b == true)
                return false;

            return true;
        }
    }
}