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
        [SerializeField]
        private Vector3 positionOffset = Vector3.zero; //the offset of the menu from the player position, good values: 0.3f, 0.5f, 1.5f

        private bool isOpen;

        [SerializeField]
        private InputAction action;

        private Hand hand;

        private Camera mainCamera; //used to put the radial menu in front of the player

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

            //Set menu position
            mainCamera = Camera.main;
            SetPositionWithOffset();

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
            item.SetEmptyObject(itemData.emptyObject);
            item.SetIcon(itemData.icon);
            item.SetBackground(itemData.background);
            item.SetItemName(itemData.itemName);
            item.SetRadialMenu(this);

            //Instantiate all gameobjects and save them in variables
            if(itemData.itemPrefab){
                GameObject itemGO = Instantiate(itemData.itemPrefab, Vector3.zero, Quaternion.identity);
                item.SetItemSpawned(itemGO);
                itemGO.GetComponent<Item>().SetItemName(itemData.itemName);
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

            //Set the hand as parent and put the item right on it
            instantiatedItem = item;
            instantiatedItem.transform.parent = hand.gameObject.transform.parent;
            instantiatedItem.transform.localPosition = Vector3.zero;

            //Get the Item component
            Item instantiatedItemComponent = instantiatedItem.GetComponent<Item>();

            //Now the item is in the correct place to be grabbed, but we have to make sure that it is rotated in the same direction as the hands.
            instantiatedItemComponent.FaceSameDirection();

            //Now we should be able to grab the item in the perfect position. Activate the item and perform the grab
            instantiatedItemComponent.ActivateItemModel();
            //hand.TryGrab(instantiatedItem.GetComponent<Grabbable>());            
            hand.Grab();

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
            ResetItemArrangement();
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

        #region SetterAndGetter

            //Set the position of the menu with respect to the player. It also uses the offset variable to put the menu in front of the player
            private void SetPositionWithOffset(){

                Transform menuCanvas = transform.parent;

                menuCanvas.parent = mainCamera.transform;
                
                float posX = positionOffset.x;
                float posY = positionOffset.y;
                float posZ = positionOffset.z;

                menuCanvas.localRotation = Quaternion.identity;

                menuCanvas.localPosition = new Vector3(posX, posY, posZ); 
            }

        #endregion
    
    }
}
