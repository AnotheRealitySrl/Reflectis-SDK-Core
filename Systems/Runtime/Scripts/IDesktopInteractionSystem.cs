using SPACS.Core;

namespace SPACS.SDK.Systems
{
    public interface IDesktopInteractionSystem : ISystem
    {
        public enum DesktopSelectionType
        {
            Mouse = 1,
            Proximity = 2
        }

        void EnableDesktopInteraction(bool enable);

        void StartSelecting(BaseInteractableGO interactable, DesktopSelectionType mode);
        void StopSelecting(BaseInteractableGO interactable, DesktopSelectionType mode);
    }
}

