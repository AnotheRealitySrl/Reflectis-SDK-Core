using Reflectis.SDK.Core;

using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.InputSystem;

using static Reflectis.SDK.InteractionNew.ContextualMenuManageable;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class ContextualMenuSystem : BaseSystem
    {
        [SerializeField] private List<ContextualMenuDict> customContextualMenuControllers;
        [SerializeField] private float showTime = 1.5f;
        [SerializeField] private float hideTime = 1.5f;
        [SerializeField] private bool createMenuOnInit = true;
        [SerializeField] private bool dontDestroyOnLoad = false;
        [SerializeField, Tooltip("Checked if only one contextual menu can be opened at a time")]
        protected bool esclusiveContextualMenus = false;
        [SerializeField] protected InputActionReference contextualMenuInputActionRef;

        [Header("Scriptable actions")]
        [SerializeField] private List<AwaitableScriptableAction> onHoverEnterActions = new();
        [SerializeField] private List<AwaitableScriptableAction> onHoverExitActions = new();

        protected ContextualMenuController contextualMenu;
        protected ContextualMenuManageable selectedInteractable;

        protected Dictionary<EContextualMenuType, ContextualMenuController> customContextualMenuControllersCache = new();

        public ContextualMenuController ContextualMenuInstance => contextualMenu;
        public ContextualMenuManageable SelectedInteractable => selectedInteractable;

        public float ShowToastTime { get => showTime; private set => showTime = value; }
        public float HideToastTime { get => hideTime; private set => hideTime = value; }

        public List<AwaitableScriptableAction> OnHoverEnterActions => onHoverEnterActions;
        public List<AwaitableScriptableAction> OnHoverExitActions => onHoverExitActions;


        public override void Init()
        {
            if (createMenuOnInit)
            {
                CreateMenu();
            }

            contextualMenuInputActionRef.action.Enable();

            contextualMenuInputActionRef.action.started += OnMenuActivate;
            contextualMenuInputActionRef.action.performed += OnMenuActivate;
            contextualMenuInputActionRef.action.canceled += OnMenuActivate;
        }

        private void OnDestroy()
        {
            contextualMenuInputActionRef.action.started -= OnMenuActivate;
            contextualMenuInputActionRef.action.performed -= OnMenuActivate;
            contextualMenuInputActionRef.action.canceled -= OnMenuActivate;
        }

        #region Input actions callbacks

        public abstract void OnMenuActivate(InputAction.CallbackContext context);

        #endregion

        #region Abstract methods

        public abstract InteractableBehaviourBase SetupInteractableBehaviour(GameObject obj);

        #endregion

        #region API

        public void CreateMenu(Transform parent = null)
        {
            customContextualMenuControllersCache.Clear();
            foreach (var contextualMenu in customContextualMenuControllers)
            {
                if (contextualMenu.Value.GetComponent<ContextualMenuController>())
                {
                    GameObject instantiatedMenu = Instantiate(contextualMenu.Value, parent);
                    customContextualMenuControllersCache.TryAdd(contextualMenu.Key, instantiatedMenu.GetComponent<ContextualMenuController>());

                    if (dontDestroyOnLoad)
                    {
                        DontDestroyOnLoad(instantiatedMenu);
                    }
                }

            }
        }

        public void DestroyMenu()
        {
            if (contextualMenu)
            {
                Destroy(contextualMenu);
                contextualMenu = null;
            }
        }

        public virtual async Task ShowContextualMenu(ContextualMenuManageable manageable)
        {
            var oldContextualMenu = contextualMenu;
            if (customContextualMenuControllersCache.TryGetValue(manageable.ContextualMenuType, out contextualMenu))
            {
                if (esclusiveContextualMenus && contextualMenu != oldContextualMenu)
                {
                    await HideContextualMenu(oldContextualMenu);

                }
                contextualMenu.Setup(manageable.ContextualMenuOptions);
                await contextualMenu.Show();
            }
        }

        public virtual async Task HideContextualMenu()
        {
            if (contextualMenu)
            {
                await contextualMenu.Hide();
                contextualMenu.Unsetup();
            }
        }
        public virtual async Task HideContextualMenu(ContextualMenuController contextualMenu)
        {
            if (contextualMenu)
            {
                await contextualMenu.Hide();
                contextualMenu.Unsetup();
            }
        }

        public void ShowPreviewContextualMenu(ContextualMenuManageable manageable)
        {
            if (customContextualMenuControllersCache.TryGetValue(manageable.ContextualMenuType, out contextualMenu))
            {
                contextualMenu.Setup(manageable.ContextualMenuOptions);
                contextualMenu.ShowPreview();
            }
        }

        public void HidePreviewContextualMenu()
        {
            contextualMenu.HidePreview();
            contextualMenu.Unsetup();
        }

        #endregion
    }
}
