using Reflectis.SDK.Utilities;
using System.Collections.Generic;

namespace Reflectis.SDK.Diagnostics
{
    public abstract class DisplayableContentBase
    {
        public abstract void CheckValidity();

        public abstract void AssignValues(List<Field> args);
    }
}
