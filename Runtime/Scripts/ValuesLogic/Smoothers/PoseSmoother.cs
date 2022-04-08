using SPACS.Utilities;

using System.Collections.Generic;

using UnityEngine;

namespace SPACS.Utilities
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Processes pose values in order to smooth them.
    /// </summary>
    public class PoseSmoother : AbstractSmoother<Pose>
    {
        ///////////////////////////////////////////////////////////////////////////
        /// <inheritdoc/>
        protected override Pose Average(IEnumerable<Pose> values) => values.Average();
    }
}