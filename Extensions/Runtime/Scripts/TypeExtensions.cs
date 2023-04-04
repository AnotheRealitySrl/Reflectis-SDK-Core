using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.SDK.Extensions
{
    public static class TypeExtensions
    {
        public static string LastPartOfTypeName(this Type type)
        {
            string[] typeNameFull = type.FullName?.Split('.');
            string typeNameLast = typeNameFull?[typeNameFull.Length - 1];
            return typeNameLast;
        }
    }
}
