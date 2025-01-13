﻿namespace Reflectis.SDK.Utilities.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// Returns the division rounded up
        /// </summary>
        public static int RoundUpDivision(this int value, int divider)
        {
            return (value + divider - 1) / divider;

        }

    }
}