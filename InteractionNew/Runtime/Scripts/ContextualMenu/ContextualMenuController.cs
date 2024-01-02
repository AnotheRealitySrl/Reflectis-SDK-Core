using Reflectis.SDK.Core;
using Reflectis.SDK.Transitions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;
using static Reflectis.SDK.InteractionNew.ContextualMenuManageable;

namespace Reflectis.SDK.InteractionNew
{
    public class ContextualMenuController : MonoBehaviour
    {
        [Serializable]
        private class ContextualMenuItem
        {
            public EContextualMenuOption key;
            public GameObject value;
        }

        [SerializeField] private List<ContextualMenuItem> contextualMenuItems = new();

        public UnityEvent onShow;
        public UnityEvent onHide;


        private AbstractTransitionProvider transitionProvider;


        private void Awake()
        {
            transitionProvider = GetComponent<AbstractTransitionProvider>();
            Unsetup();
            Hide();
        }

        public void Setup(EContextualMenuOption options)
        {
            contextualMenuItems.ForEach(x =>
            {
                if (x.value)
                    x.value.SetActive(options.HasFlag(x.key));
            });
        }

        public void Unsetup()
        {
            contextualMenuItems.ForEach(x =>
            {
                if (x.value)
                    x.value.SetActive(false);
            });
        }

        public virtual async Task Show()
        {
            await transitionProvider.DoTransitionAsync(true);
            onShow?.Invoke();
        }

        public virtual async Task Hide()
        {
            await transitionProvider.DoTransitionAsync(false);
            onHide?.Invoke();
        }

        public void ShowPreview()
        {
            //GetComponentInChildren<CanvasGroup>().alpha = 0.5f;
        }

        public void HidePreview()
        {
            //GetComponentInChildren<CanvasGroup>().alpha = 0f;
        }

        public void OnContextualMenuButtonClicked(int option)
        {
            SM.GetSystem<ContextualMenuSystem>().SelectedInteractable.OnContextualMenuButtonClicked((EContextualMenuOption)option);
        }
    }
}
