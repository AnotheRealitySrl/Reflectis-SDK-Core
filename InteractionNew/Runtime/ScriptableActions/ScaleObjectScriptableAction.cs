using DG.Tweening;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    [CreateAssetMenu(menuName = "Reflectis/ScriptableActions/ScaleObjectScriptableAction", fileName = "ScaleObjectScriptableAction")]
    public class ScaleObjectScriptableAction : AwaitableScriptableAction
    {
        [SerializeField] private float scaleFactor = 1f;

        public override async Task Action(IInteractable interactable)
        {
            bool completed = false;

            Transform referenceTransform = interactable.GameObjectRef.transform;
            referenceTransform.DOScale(referenceTransform.localScale.x * scaleFactor, 1f).onComplete += () => completed = true;

            while (!completed)
            {
                await Task.Delay(25);
            }
        }
    }
}
