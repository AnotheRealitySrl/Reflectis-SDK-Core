using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace SPACS.Utilities
{
    public static class VectorExtensions
    {
        ///////////////////////////////////////////////////////////////////////////
        public static Vector3 Average(this IEnumerable<Vector3> vectors)
        {
            Vector3 average = Vector3.zero;

            foreach (Vector3 vector in vectors)
                average += vector;
            average /= vectors.Count();

            return average;
        }

    }
}