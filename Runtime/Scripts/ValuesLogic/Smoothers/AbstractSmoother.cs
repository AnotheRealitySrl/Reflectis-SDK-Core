using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace SPACS.Utilities
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// This abstract component provides a base implementation of a value
    /// smoother. Each processed value is stored and a new averaged value is
    /// returned. Use the intensity parameter to control the smoothness.
    /// </summary>
    public abstract class AbstractSmoother<Value> : MonoBehaviour
    {
        [SerializeField, Min(1), Tooltip("The intensity of the smoothing effect")]
        private int intensity = 1;

        [SerializeField, Tooltip("Event fired on each processed value. It provides the smoothed result")]
        private UnityEvent<Value> OnProcess = default;


        private Value[] samples = null;
        private int circularIndex = 0;
        private bool arrayFull = false;

        ///////////////////////////////////////////////////////////////////////////
        public void Process(Value value)
        {
            // Initialize the samples array if it is not already
            if (samples == null || samples.Length != intensity)
            {
                samples = new Value[intensity];
                circularIndex = 0;
                arrayFull = false;
            }

            // Store the new input in the array and update the indexes
            samples[circularIndex] = value;
            circularIndex = (circularIndex + 1) % intensity;
            arrayFull |= circularIndex == (intensity - 1);

            // Call the abstract average function
            Value output;
            if (arrayFull)
                output = Average(samples);
            else
                output = Average(new ArraySegment<Value>(samples, 0, circularIndex));

            // Invoke the process event
            OnProcess.Invoke(output);
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary> Calculates the average of the provided values </summary>
        /// <param name="values">The values to average</param>
        /// <returns>The average</returns>
        protected abstract Value Average(IEnumerable<Value> values);
    }
}