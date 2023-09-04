using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.RadialMenu
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private GameObject itemModel; //the model of the item
        [SerializeField] private Vector3 startPosition; //the start position of the item, usefull to place it with some offset on the hands

        [SerializeField] private Quaternion startRotation = new Quaternion (0f,0f,0f,0f); //the start rotaton of the item, useful to place it in the correct position in the hands

        private Rigidbody rb; 

        private string itemName; //name of the item

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }


        //Actovate the item, set the rotation and position to the standard ones
        public void ActivateItemModel()
        {
            transform.localRotation = startRotation;
            transform.localPosition = startPosition;
            rb.isKinematic = false;
            itemModel.SetActive(true);
        }

        //Deactivate the item and reset it
        public void DeActivateItemModel()
        {
            itemModel.transform.localPosition = startPosition;
            transform.localRotation = startRotation;
            rb.isKinematic = true;
            itemModel.SetActive(false);
        }

        #region Setter and Getter

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        public void SetItemName(string name){
            itemName = name;
        }

        public string GetName(){
            return itemName;
        }

        public void SetStandardRotation(){
            transform.localRotation = startRotation;
        }

        public void FaceSameDirection(){
            transform.localRotation = startRotation;
        }

        #endregion
    }
}
