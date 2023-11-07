#if ODIN_INSPECTOR
using Reflectis.SDK.Transitions;

using Sirenix.OdinInspector;
using Sirenix.Utilities;
#endif

using System.Collections.Generic;

using UnityEngine;

using static Reflectis.SDK.InteractionNew.ContextualMenuManageable;

namespace Reflectis.SDK.InteractionNew
{
    public class ContextualMenuController : MonoBehaviour
    {
        [CreateAssetMenu(menuName = "AnotheReality/Utils/ContextualMenuDictionary", fileName = "ContextualMenuDictionary")]
        public class ContextualMenuDictionary
#if ODIN_INSPECTOR
            : SerializedScriptableObject
#endif
        {
            [SerializeField] private Dictionary<EContextualMenuOption, GameObject> contextualMenuItems = new();
            public Dictionary<EContextualMenuOption, GameObject> ContextualMenuItems => contextualMenuItems;
        }

        [SerializeField] private ContextualMenuDictionary menuDictionary;

        AbstractTransitionProvider transitionProvider;

        private void Awake()
        {
            transitionProvider = GetComponent<AbstractTransitionProvider>();

            Unsetup();
        }

        public async void Setup(EContextualMenuOption options)
        {
            menuDictionary.ContextualMenuItems.ForEach(x => x.Value.SetActive(options.HasFlag(x.Key)));
            await transitionProvider.EnterTransitionAsync();
        }

        public async void Unsetup()
        {
            await transitionProvider.ExitTransitionAsync();
            menuDictionary.ContextualMenuItems.Values.ForEach(e => e.SetActive(false));
        }
    }
}
