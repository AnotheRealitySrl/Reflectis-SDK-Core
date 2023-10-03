using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.RadialMenu
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private GameObject itemObj; //the obj containing the model and colliders, no logic
        [SerializeField] private Vector3 startPosition; //the start position of the item, usefull to place it with some offset on the hands
        [SerializeField] private GameObject itemModel; //model of the item

        [SerializeField] private Quaternion startRotation = new Quaternion (0f,0f,0f,0f); //the start rotaton of the item, useful to place it in the correct position in the hands

        private Rigidbody rb; 

        private string itemName; //name of the item
        private int itemNumber; //the number of the item, usefull to acess ordered networked lists

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
            itemObj.SetActive(true);
        }

        //Deactivate the item and reset it
        public void DeActivateItemModel()
        {
            itemObj.transform.localPosition = startPosition;
            transform.localRotation = startRotation;
            rb.isKinematic = true;
            itemObj.SetActive(false);
        }

        #region Setter and Getter

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        public void SetItemName(string name){
            itemName = name;
        }

        public void SetItemNumber(int number)
        {
            itemNumber = number;
        }

        public string GetName(){
            return itemName;
        }

        public int GetItemNumber()
        {
            return itemNumber;
        }

        public GameObject GetModel()
        {
            return itemModel;
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
