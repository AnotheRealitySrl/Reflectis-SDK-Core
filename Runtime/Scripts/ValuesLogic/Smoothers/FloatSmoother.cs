using System.Collections.Generic;

namespace SPACS.Utilities
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Processes float values in order to smooth them.
    /// </summary>
    public class FloatSmoother : AbstractSmoother<float>
    {
        ///////////////////////////////////////////////////////////////////////////
        /// <inheritdoc/>
        protected override float Average(IEnumerable<float> values)
        {
            float sum = 0.0f;
            int count = 0;
            foreach (float value in values)
            {
                sum += value;
                count++;
            }

            return count > 0 ? sum / count : sum;
        }
    }
}