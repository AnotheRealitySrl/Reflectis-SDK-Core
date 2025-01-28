using Reflectis.SDK.Core.SystemFramework;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.Core.ModelExploder
{
    public interface IModelExploderSystem : ISystem
    {
        Task AssignModelExploder(GameObject obj, bool networkedContext = true);

    }
}
