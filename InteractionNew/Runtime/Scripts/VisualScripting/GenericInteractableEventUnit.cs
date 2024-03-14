using Unity.VisualScripting;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class GenericInteractableEventUnit : AwaitableEventNode<GenericInteractable>
    {
        [DoNotSerialize]
        public ValueOutput interactable { get; private set; }

        protected override void Definition()
        {
            base.Definition();
            // Setting the value on our port.
            interactable = ValueOutput<GenericInteractable>(nameof(interactable));
        }

        // Setting the value on our port.
        protected override void AssignArguments(Flow flow, GenericInteractable data)
        {
            flow.SetValue(interactable, data);
        }

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook("GenericInteractable" + this.ToString().Split("Unit")[0]);
        }
    }
}
