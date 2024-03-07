#if ODIN_INSPECTOR
using Sirenix.OdinInspector;

#endif
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task InitInternal(ISystem parentSystem = null)
        {
            ParentSystem = parentSystem;
            return Init();
        }

        /// <summary>
        /// Override with init functionalities
        /// </summary>
        public virtual Task Init()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when finish system lifecycle. Override if needed.
        /// </summary>
        public virtual void Finish() { }
    }
}