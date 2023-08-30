using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.RadialMenu
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private GameObject itemModel;
        [SerializeField] private Vector3 heldPosition;
        [SerializeField] private Vector3 startPosition;

        private Rigidbody rb;

        private string itemName;

        private Quaternion startRotation;

        private void Awake()
        {
            //save the standard rotation and position
            startRotation = transform.rotation;
            itemModel.transform.localPosition = startPosition;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }


        public void ActivateItemModel()
        {
            transform.rotation = startRotation;
            rb.isKinematic = false;
            itemModel.SetActive(true);
        }

        public void DeActivateItemModel()
        {
            itemModel.transform.localPosition = startPosition;
            rb.isKinematic = true;
            itemModel.SetActive(false);
        }

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
    }
}
