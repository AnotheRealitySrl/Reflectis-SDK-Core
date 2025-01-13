using Reflectis.SDK.Core.VisualScripting;

using Unity.VisualScripting;

namespace Reflectis.SDK.Core.Interaction
{
    public abstract class GenericInteractableEventUnit : AwaitableEventUnit<GenericInteractable>
    {
        [DoNotSerialize]
        public ValueOutput Interactable { get; private set; }

        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            Interactable = ValueOutput<GenericInteractable>(nameof(Interactable));
        }

        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, GenericInteractable data)
        {
            flow.SetValue(Interactable, data);
        }

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook("GenericInteractable" + this.ToString().Split("Unit")[0]);
        }
    }
}
