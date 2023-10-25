using DG.Tweening;

using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    [CreateAssetMenu(menuName = "Reflectis/ScriptableActions/ReversePanCameraScriptableAction", fileName = "ReversePanCameraScriptableAction")]
    public class ReversePanCameraScriptableAction : AwaitableScriptableAction
    {
        public override async Task Action(IInteractable interactable)
        {
            bool completed = false;
            Camera mainCamera = Camera.main;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(mainCamera.transform.DOMove(new Vector3(0, 0, -10f), 1f));
            sequence.Append(mainCamera.transform.DORotate(Vector3.zero, 1f));
            sequence.onComplete += () => completed = true;
            sequence.Restart();

            while (!completed)
            {
                await Task.Delay(25);
            }
        }
    }
}
