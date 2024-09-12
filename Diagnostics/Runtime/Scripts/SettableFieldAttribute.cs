using System;

namespace Reflectis.SDK.Diagnostics
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class SettableFieldAttribute : Attribute
    {
        public bool isRequired = false;
    }
}
