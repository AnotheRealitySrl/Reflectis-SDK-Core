using System;

using UnityEngine;

namespace Reflectis.SDK.Core.Utilities
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class OnChangedCallAttribute : PropertyAttribute
    {
        public string methodName;
        public OnChangedCallAttribute(string methodNameNoArguments)
        {
            methodName = methodNameNoArguments;
        }
    }
}
