using Reflectis.SDK.Core.VisualScripting;

using Unity.VisualScripting;

namespace Reflectis.SDK.Core.Interaction
{
    [UnitTitle("Reflectis Generic Interactable: Unselect OnDestroy")]
    [UnitSurtitle("Generic Interactable")]
    [UnitShortTitle("Unselect OnDestroy")]
    [UnitCategory("Events\\Reflectis")]
    public class UnselectOnDestroyUnit : AwaitableEventUnit<GenericInteractable>
    {
        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook("GenericInteractable" + this.ToString().Split("Unit")[0]);
        }
    }
}
