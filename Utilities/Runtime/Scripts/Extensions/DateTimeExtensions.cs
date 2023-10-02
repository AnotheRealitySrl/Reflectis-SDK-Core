using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Reflectis.SDK.Utilities
{
    public static class DateTimeExtensions
    {

        public static DateTime ParseStringToDate(this DateTime date, string dateString, string format = "dd/MM/yyyy HH:mm")
        {

            if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                Debug.Log("Data e ora: " + date);
                return date;
            }
            else
            {
                date = new DateTime();
                Debug.LogError("Formato data/ora non valido.");
                return date;
            }

        }
    }
}
