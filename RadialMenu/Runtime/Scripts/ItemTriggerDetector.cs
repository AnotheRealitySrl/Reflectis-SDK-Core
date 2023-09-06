using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using Autohand;

namespace Reflectis.SDK.RadialMenu
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Simple component that fires events when a rigidbody enters or exits a
    /// trigger
    /// </summary>
    public class ItemTriggerDetector : MonoBehaviour
    {
        [SerializeField, Tooltip("the name of the item that can trigger the event.")]
        private string itemName;

        [SerializeField, Tooltip("")]
        private Collider trigger = default;

        [Header("Settings")]
        [SerializeField, Tooltip("")]
        private float holdTime = 0.0f;

        [Header("Events")]
        [SerializeField, Tooltip("")]
        private UnityEvent<Collider> onTriggerEnter = default;

        [SerializeField, Tooltip("")]
        private UnityEvent onTriggerExit = default;

        [SerializeField] 
        private bool isHand = false; //check whether or not the item should be the hand


        private TriggerProxy triggerProxy;

        ///////////////////////////////////////////////////////////////////////////
        private void OnEnable()
        {
            triggerProxy = trigger.gameObject.AddComponent<TriggerProxy>();
            triggerProxy.detector = this;
        }

        ///////////////////////////////////////////////////////////////////////////
        private void OnDisable()
        {
            if (triggerProxy != null)
                Destroy(triggerProxy);
        }

        ///////////////////////////////////////////////////////////////////////////
        private class TriggerProxy : MonoBehaviour
        {
            public ItemTriggerDetector detector;
            private Coroutine holdingCoroutine;

            ///////////////////////////////////////////////////////////////////////////
            private void OnTriggerEnter(Collider other)
            {   
                if(!detector.isHand){
                    Item item = other.attachedRigidbody.gameObject.GetComponent<Item>();
                    if(item!=null){
                        if(item.GetName()==detector.itemName){ //item.GetName().Equals(itemName)
                            if (holdingCoroutine != null)
                                StopCoroutine(holdingCoroutine);

                            IEnumerator coroutine()
                            {
                                yield return new WaitForSeconds(detector.holdTime);
                                detector.onTriggerEnter.Invoke(other);
                            }
                            holdingCoroutine = StartCoroutine(coroutine());
                        }
                    }
                }else{
                    Hand hand = other.attachedRigidbody.gameObject.GetComponent<Hand>();
                    if(hand!=null){
                        //Check if there's an item on the hand, if there is then exit, otherwise continue
                        if(hand.GetHeld()){
                            //Do Nothing because hand has an item on it
                        }else{
                            //Hand without Item, then start the trigger
                            if (holdingCoroutine != null)
                                StopCoroutine(holdingCoroutine);

                            IEnumerator coroutine()
                            {
                                yield return new WaitForSeconds(detector.holdTime);
                                detector.onTriggerEnter.Invoke(other);
                            }
                            holdingCoroutine = StartCoroutine(coroutine());
                        }
                    }
                }
            }

            ///////////////////////////////////////////////////////////////////////////
            private void OnTriggerExit(Collider other)
            {
                if(!detector.isHand){
                    Item item = other.attachedRigidbody.gameObject.GetComponent<Item>();
                    if(item!=null){
                        if(item.GetName()==detector.itemName){ 
                            detector.onTriggerExit.Invoke();
                        }
                    }
                    if(holdingCoroutine != null){
                        StopCoroutine(holdingCoroutine);
                    }
                }else{
                    Hand hand = other.attachedRigidbody.gameObject.GetComponent<Hand>();
                    if(hand!=null){
                        detector.onTriggerExit.Invoke();
                    }
                    if(holdingCoroutine != null){
                        StopCoroutine(holdingCoroutine);
                    }
                }

            }
        }
    }
}