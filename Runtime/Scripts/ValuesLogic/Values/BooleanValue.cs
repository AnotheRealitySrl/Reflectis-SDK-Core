using UnityEngine;
using UnityEngine.Events;

namespace SPACS.Utilities
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A boolean value wrapper
    /// </summary>
    public class BooleanValue : BaseValue
    {
        [Tooltip("The update event")]
        public UnityEvent<bool> OnUpdate;

        [Tooltip("The actual boolean value"), SerializeField]
        private bool value = false;


        ///////////////////////////////////////////////////////////////////////////
        protected override void FireEvent() => OnUpdate.Invoke(Value);

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The actual value
        /// </summary>
        public bool Value
        {
            get => value;
            set
            {
                if (isActiveAndEnabled)
                {
                    bool changed = this.value != value;
                    this.value = value;
                    if (IsUpdateMode(UpdateEventMode.OnSet) || changed && IsUpdateMode(UpdateEventMode.OnChange))
                        FireEvent();
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Toggles the value
        /// </summary>
        public void Toggle() => Value = !Value;
    }
}