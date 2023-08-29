using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Autohand;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using Reflectis.SDK.Avatars;
using Reflectis.SDK.Core;

namespace Reflectis.SDK.RadialMenu
{
    public class RadialMenu : MonoBehaviour
    {
        #region References
        [SerializeField]
        private GameObject menuObj; //the actual menu that can be opened and clsoed

        [SerializeField]
        private GameObject itemPrefab; //reference to the prefab of the menu item element

        [SerializeField]
        private List<RadialMenuItemData> itemListData; //list of the data of items. They are used to generate the actual items in the menu

        private List<RadialMenuItem> itemList; //the list of the items contained in the radial menu

        private GameObject instantiatedItem; //the item that has been instantiated.

        [SerializeField]
        private float radius = 100f; //the radius of the radial menu

        private bool isOpen;

        [SerializeField]
        private InputAction action;

        private Hand hand;

        private GameObject player;
        #endregion

        #region Unity Methods
        private void Awake()
        {

            isOpen = false;

            itemList = new List<RadialMenuItem>();
            for (int i = 0; i < itemListData.Count; i++)
            {
                AddItem(itemListData[i]);
            }
        }

        private IEnumerator Start()
        {
            instantiatedItem = null;
            //while (!hand)
            while (!hand)
            {
                if (SM.GetSystem<AvatarSystem>().AvatarInstance)
                {
                    //choose which hand
                    hand = SM.GetSystem<AvatarSystem>().AvatarInstance.CharacterReference.RightInteractorReference.GetComponentInChildren<Hand>();
                }
                yield return null;
            }
            
            //Handle button pressed input
            action.Enable();
            action.started += ButtonActionCallback;
            action.performed += ButtonActionCallback;
            action.canceled += ButtonActionCallback;
        }

        public void OnDestroy()
        {
            action.started -= ButtonActionCallback;
            action.performed -= ButtonActionCallback;
            action.canceled -= ButtonActionCallback;
            action.Disable();
        }
        #endregion

        #region Item Managment
        //add Item to the menu
        private void AddItem(RadialMenuItemData itemData)
        {
            GameObject itemPre = Instantiate(itemPrefab, transform);
            itemPre.transform.SetParent(menuObj.transform);

            //set all the item datas
            RadialMenuItem item = itemPre.GetComponent<RadialMenuItem>();
            item.SetData(itemData);
            item.SetIcon(itemData.icon);
            item.SetBackground(itemData.background);
            item.SetRadialMenu(this);

            //Instantiate all gameobjects and save them in variables
            if(itemData.itemPrefab){
                GameObject itemGO = Instantiate(itemData.itemPrefab, Vector3.zero, Quaternion.identity);
                item.SetItemSpawned(itemGO);
            }

            //finally add the item to the list
            itemList.Add(item);
        }

        public void ArrangeItem()
        {
            //Get the x and y position of the item
            float angle = (Mathf.PI * 2) / itemList.Count;

            for (int i = 0; i < itemList.Count; i++)
            {
                float x = Mathf.Sin(angle * i) * radius;
                float y = Mathf.Cos(angle * i) * radius;

                RectTransform itemRect = itemList[i].GetComponent<RectTransform>();
                Vector3 baseScale = itemList[i].gameObject.transform.localScale;

                itemRect.gameObject.transform.localScale = Vector3.zero;
                itemRect.DOScale(baseScale, .3f).SetEase(Ease.OutQuad);
                itemRect.DOAnchorPos(new Vector3(x, y, 0), .3f).SetEase(Ease.OutQuad);
            }
        }

        public void ResetItemArrangement()
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                RectTransform itemRect = itemList[i].GetComponent<RectTransform>();
                Vector3 baseScale = itemRect.localScale;

                itemRect.DOScale(baseScale, .3f).SetEase(Ease.OutQuad);
                itemRect.DOAnchorPos(new Vector3(0, 0, 0), .3f).SetEase(Ease.OutQuad);
                itemList[i].UnHoverItem();
            }
            menuObj.SetActive(false);
            isOpen = false;
        }

        public void InstantiateItem(GameObject item)
        {
            
            //Check if there's already an instantiated object
            if (instantiatedItem)
            {
                //Release current item
                hand.Release();
                hand.ForceReleaseGrab();
                instantiatedItem.GetComponent<Item>().DeActivateItemModel();

                if (instantiatedItem == item)
                {
                    ResetItemArrangement();
                    instantiatedItem = null;
                    return;
                }

            }

            //instantiate the item on the hand
            item.GetComponent<Item>().ActivateItemModel();
            instantiatedItem = item;

            hand.TryGrab(instantiatedItem.GetComponent<Grabbable>());
            //hand.Grab();

            //close the menu --- Here if we want we can add animations too
            ResetItemArrangement();
            return;
        }

        public void RemoveItem()
        {
            hand.Release();
            hand.ForceReleaseGrab();
            if (instantiatedItem)
            {
                instantiatedItem.GetComponent<Item>().DeActivateItemModel();
            }
            instantiatedItem = null;
        }
        #endregion

        #region Input
        public void OpenMenu()
        {
            if (!isOpen)
            {

                menuObj.SetActive(true);
                isOpen = true;
                ArrangeItem();
            }
            else
            {
                ResetItemArrangement();
                menuObj.SetActive(false);
                isOpen = false;

            }
        }

        public void ButtonActionCallback(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OpenMenu();
            }

            if (context.canceled)
            {
                //Do Nothing for now
            }
        }
        #endregion
    }
}
