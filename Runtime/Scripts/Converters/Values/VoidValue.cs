using UnityEngine;
using UnityEngine.Events;

namespace SPACS.Logic.Values
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A void value wrapper
    /// </summary>
    public class VoidValue : BaseValue
    {
        [SerializeField, Tooltip("The update event")]
        public UnityEvent OnUpdate;

        ///////////////////////////////////////////////////////////////////////////
        public void Trigger() => FireEvent();

        ///////////////////////////////////////////////////////////////////////////
        protected override void FireEvent()
        {
            if (isActiveAndEnabled && !IsUpdateMode(UpdateEventMode.Never))
                OnUpdate.Invoke();
        }
    }
}