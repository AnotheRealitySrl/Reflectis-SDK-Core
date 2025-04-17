using System.Collections.Generic;
using Unity.VisualScripting;

namespace Reflectis.SDK.Core.VisualScripting
{

    public abstract class InstanceDataUnit<DataType> : Unit
    {
        protected Dictionary<GraphReference, DataType> instanceData = new();

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);
            instanceData[instance] = GetData(instance);
        }

        public override void Uninstantiate(GraphReference instance)
        {
            base.Uninstantiate(instance);
            if (instanceData.ContainsKey(instance))
            {
                instanceData.Remove(instance);
            }
        }

        protected abstract DataType GetData(GraphReference instance);
    }
}