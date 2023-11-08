using Reflectis.SDK.Core;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class ContextualMenuSystem : BaseSystem
    {
        [SerializeField] private ContextualMenuController contextualMenuPrefab;
        [SerializeField] private float showTime = 1.5f;
        [SerializeField] private float hideTime = 1.5f;
        [SerializeField] private bool createMenuOnInit = true;
        [SerializeField] private bool dontDestroyOnLoad = false;
        [SerializeField] private InputActionReference contextualMenuInputActionRef;

        [Header("Scriptable actions")]
        [SerializeField] private List<AwaitableScriptableAction> onHoverEnterActions = new();
        [SerializeField] private List<AwaitableScriptableAction> onHoverExitActions = new();

        protected ContextualMenuController contextualMenu;
        protected ContextualMenuManageable selectedInteractable;


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

        #region API

        public void CreateMenu(Transform parent = null)
        {
            if (parent != null)
            {
                contextualMenu = Instantiate(contextualMenuPrefab, parent);
            }
            else
            {
                contextualMenu = Instantiate(contextualMenuPrefab);
            }

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(contextualMenu);
        }

        public void DestroyMenu()
        {
            if (contextualMenu)
            {
                Destroy(contextualMenu);
                contextualMenu = null;
            }
        }

        public async void ShowContextualMenu(ContextualMenuManageable manageable)
        {
            contextualMenu.Setup(manageable.ContextualMenuOptions);
            await contextualMenu.Show();
        }

        public async void HideContextualMenu()
        {
            await contextualMenu.Hide();
            contextualMenu.Unsetup();
        }

        public void ShowPreviewContextualMenu(ContextualMenuManageable manageable)
        {
            contextualMenu.Setup(manageable.ContextualMenuOptions);
            contextualMenu.ShowPreview();
        }

        public void HidePreviewContextualMenu()
        {
            contextualMenu.HidePreview();
            contextualMenu.Unsetup();
        }

        #endregion
    }
}
