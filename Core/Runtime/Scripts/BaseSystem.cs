#if ODIN_INSPECTOR
using Sirenix.OdinInspector;

#endif
using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.Core
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

        [SerializeField]
        private bool autoInitAtStartup = true;

        protected bool isInit;



        public bool IsInit { get => isInit; }

        public bool RequiresNewInstance { get; set; } = true;

        public bool AutoInitAtStartup { get => autoInitAtStartup; }

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
        /// Override with init functionalities and set isInit to true
        /// </summary>
        public virtual void Init()
        {
            isInit = true;
        }

        /// <summary>
        /// Called when finish system lifecycle. Override if needed.
        /// </summary>
        public virtual void Finish() { }
    }
}