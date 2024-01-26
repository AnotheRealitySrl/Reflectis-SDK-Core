using Reflectis.SDK.Core;

using System.Threading.Tasks;

namespace Reflectis.SDK.InteractionNew
{
    public interface IGenericInteractionSystem : ISystem
    {
        void SetupGenericInteractable(GenericInteractable interactable);
        Task UnselectCurrentInteractable();
    }
}
