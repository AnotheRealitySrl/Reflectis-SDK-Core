using Reflectis.SDK.Core.SystemFramework;
using Reflectis.SDK.Core.Transitions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

using static Reflectis.SDK.Core.Interaction.ContextualMenuManageable;

namespace Reflectis.SDK.Core.Interaction
{
    public class ContextualMenuController : MonoBehaviour
    {
        [SerializeField]
        private GameObject unlockButton;

        [Serializable]
        private class ContextualMenuItem
        {
            public EContextualMenuOption key;
            public GameObject value;
        }

        [SerializeField] private List<ContextualMenuItem> contextualMenuItems = new();

        public UnityEvent onShow;
        public UnityEvent onHide;

        protected AbstractTransitionProvider transitionProvider;

        ContextualMenuManageable manageable;

        protected virtual void Awake()
        {
            transitionProvider = GetComponentInChildren<AbstractTransitionProvider>();
            Unsetup();
            Hide();
        }

        public virtual void Setup(ContextualMenuManageable contextualMenuManageable)
        {
            if (manageable != null)
            {
                manageable.OnCurrentBlockedChanged.RemoveListener(UpdateMenuButtons);
            }
            manageable = contextualMenuManageable;
            manageable.OnCurrentBlockedChanged.AddListener(UpdateMenuButtons);
            UpdateMenuButtons(manageable.CurrentBlockedState);
        }

        protected virtual void UpdateMenuButtons(InteractableBehaviourBase.EBlockedState blockedState)
        {
            if (blockedState.HasFlag(InteractableBehaviourBase.EBlockedState.BlockedByLockObject))
            {
                contextualMenuItems.ForEach(x => x.value.gameObject.SetActive(false));
                unlockButton.SetActive(true);
            }
            else
            {
                contextualMenuItems.ForEach(x =>
                {
                    if (x.value)
                        x.value.SetActive(manageable.ContextualMenuOptions.HasFlag(x.key));
                });
                unlockButton.SetActive(false);
            }
        }

        public virtual void Unsetup()
        {
            manageable?.OnCurrentBlockedChanged.RemoveListener(UpdateMenuButtons);
            contextualMenuItems.ForEach(x =>
            {
                if (x.value)
                    x.value.SetActive(false);
            });
        }

        public virtual async Task Show()
        {
            gameObject.SetActive(true);
            await transitionProvider.DoTransitionAsync(true);
            onShow?.Invoke();
        }


        public virtual async Task Hide()
        {
            await transitionProvider.DoTransitionAsync(false);
            onHide?.Invoke();
            gameObject.SetActive(false);
        }

        public void OnContextualMenuButtonClicked(int option)
        {
            SM.GetSystem<ContextualMenuSystem>().SelectedInteractable.OnContextualMenuButtonClicked((EContextualMenuOption)option);
        }
    }
}
