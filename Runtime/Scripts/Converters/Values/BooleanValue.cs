using UnityEngine;
using UnityEngine.Events;
#if !UNITY_2020_1_OR_NEWER
using static SPACS.Logic.Events.BasicEvents;
#endif

namespace SPACS.Logic.Values
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A boolean value wrapper
    /// </summary>
    public class BooleanValue : BaseValue
    {
        [Tooltip("The actual boolean value")]
        [SerializeField]
        private bool value = false;

        [Tooltip("The update event")]
        [SerializeField]
#if UNITY_2020_1_OR_NEWER
        public UnityEvent<bool> OnUpdate;
#else
        public BooleanUnityEvent OnUpdate;
#endif


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