#if ODIN_INSPECTOR
using Sirenix.OdinInspector;

#endif
using System.Collections.Generic;

using UnityEngine;

namespace SPACS.SDK.Core
{

    /// <summary>
    /// Base implementation of a <see cref="ISystem"/>
    /// </summary>
    public abstract class BaseSystem : ScriptableObject, ISystem
    {
#if ODIN_INSPECTOR
        [InlineEditor]
#endif
        [SerializeField]
        private List<BaseSystem> _subSystems = new();

        public bool RequiresNewInstance { get; set; } = true;

        public bool AutoInitAtStartup { get; set; } = true;

        public ISystem ParentSystem { get; private set; }

        public List<ISystem> SubSystems
        {
            get => _subSystems.ConvertAll(s => (ISystem)s);
            set => _subSystems = value.ConvertAll(s => (BaseSystem)s);
        }

        public void InitInternal(ISystem parentSystem = null)
        {
            ParentSystem = parentSystem;
            Init();
        }

        /// <summary>
        /// Override with init functionalities.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Called when finish system lifecycle. Override if needed.
        /// </summary>
        public virtual void Finish() { }
    }
}