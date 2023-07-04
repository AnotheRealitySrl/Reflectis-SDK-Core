using Reflectis.SDK.Core;
using Reflectis.SDK.Interaction;

namespace Reflectis.SDK.DesktopInteraction
{
    public interface IDesktopInteractionSystem : ISystem
    {
        public enum DesktopSelectionType
        {
            Mouse = 1,
            Proximity = 2
        }

        void Deinit();
        void EnableDesktopInteraction(bool enable);

        void StartSelecting(BaseInteractableGO interactable, DesktopSelectionType mode);
        void StopSelecting(BaseInteractableGO interactable, DesktopSelectionType mode);
    }
}

