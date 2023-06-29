using System;

using UnityEngine;

namespace SPACS.SDK.Interaction
{

    //[CreateAssetMenu(menuName = "AnotheReality/Utilities/Action", fileName = "ScriptableAction")]
    public abstract class ActionScriptable : ScriptableObject
    {

        public BaseInteractableGO InteractableObjectReference;

        public abstract void Action(Action completedCallback = null);

    }
}
