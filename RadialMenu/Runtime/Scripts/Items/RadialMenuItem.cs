using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Reflectis.SDK.RadialMenu
{
    public class RadialMenuItem : MonoBehaviour
    {
        [SerializeField]
        private RadialMenu radialMenu;

        [SerializeField]
        private Image icon;
        private RectTransform iconRect;

        [SerializeField]
        private Image outerBackground;
        private RectTransform outerBackgroundTransform;

        [SerializeField]
        private Image innerBackground;

        [SerializeField]
        private Color hoverOuterColor;

        [SerializeField]
        private Color unHoverOuterColor;

        private string itemName;

        private int itemPosition; //useful for networked items

        private bool emptyObject = false;
        private GameObject itemSpawned;
        private GameObject itemModelSpawned;

        private bool selected = false;

        private void Start()
        {
            iconRect = icon.GetComponent<RectTransform>();
            outerBackgroundTransform = outerBackground.GetComponent<RectTransform>();
        }

        public void SetItemSpawned(GameObject itemGO)
        {
            itemSpawned = itemGO;
        }

        public void SetBackground(Sprite b)
        {
            Debug.LogError("Setting new Background");
            outerBackground.sprite = b;
            Debug.LogError("Setting new Background");
            Debug.LogError(outerBackground.sprite);
        }

        public void SetInnerBackground(Sprite b)
        {
            innerBackground.sprite = b;
        }

        public void SetIcon(Sprite i)
        {
            icon.sprite = i;
        }

        public void SetItemName(string name){
            itemName = name;
        }

        public void SetItemPosition(int pos)
        {
            itemPosition = pos;
        }

        public void SetSelected(bool select)
        {
            selected = select;
        }

        public string GetName(){
            return itemName;
        }

        public GameObject GetSpawned()
        {
            return itemSpawned;
        }

        public void SetEmptyObject(bool empty)
        {
            emptyObject =empty;
        }

        public void SetRadialMenu(RadialMenu parent)
        {
            radialMenu = parent;
        }

        public void SetItemModel(GameObject itemModel)
        {
            itemModelSpawned = itemModel;
        }

        public void HoverItem()
        {
            radialMenu.HoverItem(itemName);

            if (!selected)
            {
                //change icon scale
                //iconRect.DOScale(Vector3.one * 1.5f, .3f).SetEase(Ease.OutQuad);

                //change color and outer background scale
                outerBackground.color = hoverOuterColor;
                outerBackgroundTransform.DOScale(Vector3.one * 0.85f, .3f).SetEase(Ease.OutQuad);
            }
        }

        public void UnHoverItem()
        {
            if (!selected)
            {
                //iconRect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad);

                //change color and outer background scale
                outerBackground.color = unHoverOuterColor;
                outerBackgroundTransform.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad);

                //radialMenu.UnHoverItem();
            }
        }

        public void ChooseItem()
        {
            //instantiate the item from the radial menu so that I have the player position  
            if (emptyObject)
            {
                selected = false;
                radialMenu.RemoveItem();
            }
            else
            {
                outerBackground.color = hoverOuterColor;
                outerBackgroundTransform.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad);

                radialMenu.InstantiateItem(itemSpawned, itemPosition);
            }
        }
    }
}
