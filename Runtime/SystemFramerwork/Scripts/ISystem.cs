using System.Collections.Generic;
using System.Threading.Tasks;

namespace Virtuademy.SDK.Core.SystemFramework
{
    public interface ISystem
    {
        Task InitInternal(ISystem parentSystem);
        Task Init();
        void Finish();

        bool RequiresNewInstance { get; }

        bool AutoInitAtStartup { get; }

        List<ISystem> SubSystems { get; }
    }

}