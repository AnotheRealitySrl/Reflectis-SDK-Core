using UnityEngine;
using UnityEngine.Events;
#if !UNITY_2020_1_OR_NEWER
using static SPACS.Logic.Events.BasicEvents;
#endif

namespace SPACS.Logic.Values
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A floating-point number wrapper
    /// </summary>
    public class FloatValue : BaseValue
    {

        [Tooltip("The actual float value")]
        [SerializeField]
        private float value = 0.0f;

        [Tooltip("The tolerance used as minimum acceptable difference between two numbers in order to consider them equal")]
        [SerializeField]
        private float tolerance = Mathf.Epsilon;

        [Tooltip("The value will be clamped using this minimum limit")]
        [SerializeField]
        private float min = float.MinValue;

        [Tooltip("The value will be clamped using this maximum limit")]
        [SerializeField]
        private float max = float.MaxValue;

        [Tooltip("The update event")]
        [SerializeField]
#if UNITY_2020_1_OR_NEWER
        public UnityEvent<float> OnUpdate = default;
#else
        public FloatUnityEvent OnUpdate = default;
#endif

        protected override void FireEvent() => OnUpdate.Invoke(Value);

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The actual value
        /// </summary>
        public float Value
        {
            get => value;
            set
            {
                if (isActiveAndEnabled)
                {
                    float newValue = Mathf.Clamp(value, min, max);
                    bool changed = Mathf.Abs(this.value - newValue) > tolerance;
                    this.value = newValue;
                    if (IsUpdateMode(UpdateEventMode.OnSet) || changed && IsUpdateMode(UpdateEventMode.OnChange))
                        FireEvent();
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Adding operation
        /// </summary>
        /// <param name="f">The float value to add</param>
        public void Add(float f)
        {
            Value += f;
        }
    }
}