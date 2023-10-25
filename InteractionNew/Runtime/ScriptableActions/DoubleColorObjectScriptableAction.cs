using DG.Tweening;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    [CreateAssetMenu(menuName = "Reflectis/ScriptableActions/DoubleColorObjectScriptableAction", fileName = "DoubleColorObjectScriptableAction")]
    public class DoubleColorObjectScriptableAction : AwaitableScriptableAction
    {
        [SerializeField] private Color targetColor1;
        [SerializeField] private Color targetColor2;

        public override async Task Action(IInteractable interactable)
        {
            bool completed = false;

            Transform referenceTransform = interactable.GameObjectRef.transform;
            referenceTransform.GetComponent<Renderer>().material.DOColor(targetColor2, 1).onStepComplete += () =>
                referenceTransform.GetComponent<Renderer>().material.DOColor(targetColor1, 1).onStepComplete += () =>
                    completed = true;

            while (!completed)
            {
                await Task.Delay(25);
            }
        }
    }
}
