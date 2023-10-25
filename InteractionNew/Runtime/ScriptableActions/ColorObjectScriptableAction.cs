using DG.Tweening;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    [CreateAssetMenu(menuName = "Reflectis/ScriptableActions/ColorObjectScriptableAction", fileName = "ColorObjectScriptableAction")]
    public class ColorObjectScriptableAction : AwaitableScriptableAction
    {
        [SerializeField] private Color targetColor;

        public override async Task Action(IInteractable interactable)
        {
            bool completed = false;

            Transform referenceTransform = interactable.GameObjectRef.transform;
            referenceTransform.GetComponent<Renderer>().material.DOColor(targetColor, 1).onStepComplete += () => completed = true;

            while (!completed) await Task.Delay(100);
        }
    }
}
