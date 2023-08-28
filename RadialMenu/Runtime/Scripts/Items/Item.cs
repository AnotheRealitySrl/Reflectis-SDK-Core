using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject itemModel;
    [SerializeField] private Vector3 heldPosition;
    [SerializeField] private Vector3 startPosition;

    private Rigidbody rb;

    private Quaternion startRotation;

    private void Awake(){
        //save the standard rotation and position
        startRotation = transform.rotation;
        itemModel.transform.localPosition = startPosition;
    }

    private void Start(){
        rb = GetComponent<Rigidbody>();
    }
    

    public void ActivateItemModel(){
        transform.rotation = startRotation;
        itemModel.SetActive(true);
        rb.isKinematic = false;
    }

    public void DeActivateItemModel(){
        itemModel.transform.localPosition = startPosition;
        itemModel.SetActive(false);
        rb.isKinematic = true;
    }   

    public void SetPosition(Vector3 pos){
        transform.position = pos;
    }
}
