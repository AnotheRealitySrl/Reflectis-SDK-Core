using UnityEngine;
using static Reflectis.SDK.Core.Interaction.ContextualMenuManageable;

namespace Reflectis.SDK.Core.Interaction
{
    [System.Serializable]
    public class ContextualMenuDict
    {
        public EContextualMenuType Key;
        public GameObject Value;
    }
}
