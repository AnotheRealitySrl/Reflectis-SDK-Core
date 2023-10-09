using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.RadialMenu
{
    public class RadialMenuNetworked : RadialMenu
    {
        public override void InstantiateItem(GameObject item, int pos)
        {

            //Check if there's already an instantiated object
            if (instantiatedItem)
            {
                //Release current item
                hand.Release();
                hand.ForceReleaseGrab();
                instantiatedItem.GetComponent<Item>().DeActivateItemModel();
                RadialRPCItemManager.Instance.DeActivateAll();

                if (instantiatedItem == item)
                {
                    ResetItemArrangement(-50);
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

            //RadialRPCItemManager.Instance.photonView.RPC(nameof(RadialRPCItemManager.Instance.RPC_SpawnRadialItem), Photon.Pun.RpcTarget.Others, 0);
            RadialRPCItemManager.Instance.ActivateItem(pos);

            selectedPos = pos + 1;
            //close the menu --- Here if we want we can add animations too
            ResetItemArrangement(selectedPos);
            return;
        }

        public override void RemoveItem()
        {
            hand.Release();
            hand.ForceReleaseGrab();
            if (instantiatedItem)
            {
                instantiatedItem.GetComponent<Item>().DeActivateItemModel();
                RadialRPCItemManager.Instance.DeActivateAll();
            }
            instantiatedItem = null;
            ResetItemArrangement(-50);
        }
    }
}
