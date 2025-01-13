using System;

using UnityEngine;

namespace Reflectis.InteractionLegacy
{

    //[CreateAssetMenu(menuName = "AnotheReality/Utilities/Action", fileName = "ScriptableAction")]
    public abstract class ActionScriptable : ScriptableObject
    {

        public BaseInteractableGO InteractableObjectReference;

        public abstract void Action(Action completedCallback = null);

    }
}
