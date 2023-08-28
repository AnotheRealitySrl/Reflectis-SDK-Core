using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        private Image background;

        private bool emptyObject = false;
        private GameObject itemSpawned;

        private void Start()
        {
            iconRect = icon.GetComponent<RectTransform>();
        }

        public void SetItemSpawned(GameObject itemGO)
        {
            itemSpawned = itemGO;
        }

        public void SetBackground(Sprite b)
        {
            background.sprite = b;
        }

        public void SetIcon(Sprite i)
        {
            icon.sprite = i;
        }

        public void SetData(RadialMenuItemData data)
        {
            emptyObject = data.emptyObject;
        }

        public void SetRadialMenu(RadialMenu parent)
        {
            radialMenu = parent;
        }

        public void HoverItem()
        {
            iconRect.DOComplete();
            iconRect.DOScale(Vector3.one * 1.5f, .3f).SetEase(Ease.OutQuad);
        }

        public void UnHoverItem()
        {
            iconRect.DOComplete();
            iconRect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad);
        }

        public void ChooseItem()
        {
            //instantiate the item from the radial menu so that I have the player position  
            if (emptyObject)
            {
                radialMenu.RemoveItem();
            }
            else
            {
                radialMenu.InstantiateItem(itemSpawned);
            }
        }
    }
}
