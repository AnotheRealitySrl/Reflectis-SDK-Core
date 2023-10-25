using DG.Tweening;

using Reflectis.SDK.CharacterController;

using System.Linq;
using System.Threading.Tasks;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    [CreateAssetMenu(menuName = "Reflectis/ScriptableActions/PanCameraScriptableAction", fileName = "PanCameraScriptableAction")]
    public class PanCameraScriptableAction : AwaitableScriptableAction
    {
        [SerializeField] private string panTransformId;

        public override async Task Action(IInteractable interactable)
        {
            bool completed = false;

            Transform panTransform = interactable.GameObjectRef.GetComponentsInChildren<GenericHookComponent>().FirstOrDefault(x => x.Id == panTransformId).transform;
            Camera mainCamera = Camera.main;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(mainCamera.transform.DOMove(panTransform.position, 1f));
            sequence.Append(mainCamera.transform.DORotate(panTransform.rotation.eulerAngles, 1f));
            sequence.onComplete += () => completed = true;
            sequence.Restart();

            while (!completed)
            {
                await Task.Delay(25);
            }
        }
    }
}
