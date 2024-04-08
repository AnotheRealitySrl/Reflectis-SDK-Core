using System.Threading.Tasks;

namespace Reflectis.SDK.InteractionNew
{
    public interface IInteractableBehaviour
    {
        Task Setup();

        void HoverEnter();
        void HoverExit();
    }
}
