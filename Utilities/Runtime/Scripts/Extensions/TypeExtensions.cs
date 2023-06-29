using System;

namespace SPACS.SDK.Utilities.Extensions
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
