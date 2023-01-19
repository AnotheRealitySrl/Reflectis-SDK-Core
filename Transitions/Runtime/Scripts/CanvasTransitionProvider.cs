using DG.Tweening;

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

namespace SPACS.SDK.Transitions
{
    public class CanvasTransitionProvider : AbstractTransitionProvider
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeTime = 1f;
        [SerializeField] private Ease easingFunction = Ease.InOutQuad;

        private void Awake()
        {
            if (!canvasGroup)
            {
                canvasGroup = GetComponentInChildren<CanvasGroup>();
            }
        }

        public override async Task EnterTransitionAsync()
        {
            canvasGroup.gameObject.SetActive(true);
            await canvasGroup.DOFade(1f, fadeTime).SetEase(easingFunction).AsyncWaitForCompletion();
        }

        public override async Task ExitTransitionAsync()
        {
            await canvasGroup.DOFade(0f, fadeTime).SetEase(easingFunction).AsyncWaitForCompletion();
            canvasGroup.gameObject.SetActive(false);
        }
    }
}
