using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class AwaitableScriptableAction : ScriptableObject
    {
        public abstract Task Action(IInteractable interactable = null);
    }
}
