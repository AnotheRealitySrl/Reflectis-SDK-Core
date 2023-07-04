using System.Collections.Generic;

namespace Reflectis.SDK.Core
{
    public interface ISystem
    {
        void InitInternal(ISystem parentSystem);
        void Init();
        void Finish();

        bool RequiresNewInstance { get; }

        bool AutoInitAtStartup { get; }

        List<ISystem> SubSystems { get; }
    }

}