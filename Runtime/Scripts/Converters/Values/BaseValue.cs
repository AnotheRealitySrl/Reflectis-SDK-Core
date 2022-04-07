using System;
using UnityEngine;

namespace SPACS.Logic.Values
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// An abstract value wrapper, used as base for specific implementations
    /// </summary>
    public abstract class BaseValue : MonoBehaviour
    {
        /// <summary>
        /// The update mode
        /// </summary>
        [Flags]
        public enum UpdateEventMode
        {
            Never = 0,
            OnSet = 1 << 0,
            OnChange = 1 << 1,
            OnStart = 1 << 2,
            OnEachFrame = 1 << 3
        }

        [Tooltip("Select when you want this component to fire the update event")]
        [SerializeField]
        private UpdateEventMode updateEventMode = UpdateEventMode.OnChange;


        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The update event firing method
        /// </summary>
        protected abstract void FireEvent();

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Utility method used to see if the update event mode specified is
        /// compatible with the component configuration
        /// </summary>
        /// <param name="modeToCheck">The update event mode to check</param>
        /// <returns></returns>
        protected bool IsUpdateMode(UpdateEventMode modeToCheck)
        {
            if (modeToCheck == UpdateEventMode.Never)
                return updateEventMode == UpdateEventMode.Never;
            else
                return (updateEventMode & modeToCheck) == modeToCheck;
        }

        ///////////////////////////////////////////////////////////////////////////
        protected void Start()
        {
            if (IsUpdateMode(UpdateEventMode.OnStart))
                FireEvent();
        }

        ///////////////////////////////////////////////////////////////////////////
        protected void Update()
        {
            if (IsUpdateMode(UpdateEventMode.OnEachFrame))
                FireEvent();
        }
    }
}