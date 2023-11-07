using Reflectis.SDK.Core;
using Reflectis.SDK.Transitions;

using System;
using System.Collections.Generic;

using UnityEngine;

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

        private AbstractTransitionProvider transitionProvider;

        private void Awake()
        {
            transitionProvider = GetComponent<AbstractTransitionProvider>();

            Unsetup();
        }

        public async void Setup(EContextualMenuOption options)
        {
            contextualMenuItems.ForEach(x => x.value?.SetActive(options.HasFlag(x.key)));
            await transitionProvider.EnterTransitionAsync();
        }

        public async void Unsetup()
        {
            await transitionProvider.ExitTransitionAsync();
            contextualMenuItems.ForEach(e => e.value?.SetActive(false));
        }

        public void OnContextualMenuButtonClicked(int option)
        {
            SM.GetSystem<ContextualMenuSystem>().OnContextualMenuButtonClicked.Invoke((EContextualMenuOption)option);
        }
    }
}
