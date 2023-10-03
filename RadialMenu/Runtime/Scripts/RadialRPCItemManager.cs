using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Reflectis.SDK.Avatars;
using Autohand;
using Reflectis.SDK.Core;
using Photon.Realtime;
using Reflectis.SDK.CharacterController;

namespace Reflectis.SDK.RadialMenu
{
    public class RadialRPCItemManager : MonoBehaviourPunCallbacks
    {
        public static RadialRPCItemManager Instance { get; private set; }

        [SerializeField]
        private List<GameObject> radialMenuItemModels; //list of the radial menu item models prefabs. Keep them ordered like the ones in the radialMenu!

        private List<GameObject> instantiatedClientObjects; //list of the object that this exact client instantiated

        private Hand hand; //the hand of the current player

        private int playerPhotonViewId; //the id of the avatar of the player, used to set the item as its child

        #region Unity Functions
        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            instantiatedClientObjects = new List<GameObject>();
        }
        #endregion

        #region Room functions

        //When a player joins the room instantiate the items on their hands
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            StartCoroutine(InitItemsAndGetPlayerHands());

        }

        //When a player leaves the room delete all its items and previous rpc calls
        public override void OnPlayerLeftRoom(Player otherPlayer) //OnPhotonPlayerDisconnected(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            PhotonNetwork.DestroyPlayerObjects(otherPlayer);
            PhotonNetwork.RemoveRPCs(otherPlayer);
        }

        #endregion

        #region RPC Functions

        //Instantiate the radialMenuItemModels across all the clients in the player's hand position
        [PunRPC]
        public void RPC_CreateItemInScene(int itemID, int listPos, Vector3 pos, int playerPhotonId, PhotonMessageInfo info)
        {
            GameObject itemGO = Instantiate(radialMenuItemModels[listPos], Vector3.zero, Quaternion.identity);
            PhotonView itemPhoton = itemGO.GetComponent<PhotonView>();
            itemPhoton.ViewID = itemID;
            itemGO.SetActive(false);
            Debug.LogError(itemGO);
            Debug.LogError(playerPhotonId);

            //set the player's hands as parent
            GameObject player = PhotonView.Find(playerPhotonId).gameObject;
            StartCoroutine(InstantiateOnPlayerHands(itemGO, player));

        }

        //Destroys the items in the scene that were created  by a certain player
        [PunRPC]
        public void RPC_DestroyItemInScene(int itemID, PhotonMessageInfo info)
        {
            GameObject toDestroy = PhotonView.Find(itemID).gameObject;
            if (toDestroy != null)
            {
                Destroy(toDestroy);
            }
        }

        //DeActivate the item across all the other clients
        [PunRPC]
        public void RPC_DeActivateItem(int itemID)
        {
            GameObject toDeActivate = PhotonView.Find(itemID).gameObject;
            toDeActivate.SetActive(false);
        }


        [PunRPC]
        public void RPC_ActivateItem(int itemID)
        {
            GameObject toActivate = PhotonView.Find(itemID).gameObject;
            toActivate.SetActive(true);
        }

        #endregion

        #region Main Functions

        //get the player hands and call the item instantiation function
        public IEnumerator InitItemsAndGetPlayerHands()
        {
            //Can probably comment this part and instantiate the item on the ground, it remains always inactive in the client that spawned it
            while (!hand)
            {
                if (SM.GetSystem<AvatarSystem>().AvatarInstance)
                {

                    //choose which hand
                    hand = SM.GetSystem<AvatarSystem>().AvatarInstance.CharacterReference.RightInteractorReference.GetComponentInChildren<Hand>();
                }
                yield return null;
            }

            playerPhotonViewId = SM.GetSystem<AvatarSystem>().AvatarInstance.CharacterReference.gameObject.GetComponent<PhotonView>().ViewID;
            Debug.LogError("This is player photon id " + playerPhotonViewId);

            //init the items on the hand
            yield return StartCoroutine(InitializeItems());
        }

        //Instantiate the items in all the current clients and future ones
        public IEnumerator InitializeItems()
        {

            for (int i = 0; i < radialMenuItemModels.Count; i++)
            {
                Vector3 spawnPosition = hand.gameObject.transform.position;

                GameObject itemGO = Instantiate(radialMenuItemModels[i], spawnPosition, Quaternion.identity);
                PhotonView itemPhoton = itemGO.GetComponent<PhotonView>();

                if (PhotonNetwork.AllocateViewID(itemPhoton))
                {
                    instantiatedClientObjects.Add(itemGO);
                    photonView.RPC("RPC_CreateItemInScene", Photon.Pun.RpcTarget.OthersBuffered, itemPhoton.ViewID, i, spawnPosition, playerPhotonViewId);
                }

                itemGO.SetActive(false);
            }

            yield return null;
        }

        //Set the items to active false in all clients
        public void DeActivateAll()
        {
            for (int i = 0; i < instantiatedClientObjects.Count; i++)
            {
                int toDeActivateID = instantiatedClientObjects[i].GetComponent<PhotonView>().ViewID;
                photonView.RPC("RPC_DeActivateItem", Photon.Pun.RpcTarget.OthersBuffered, toDeActivateID);

            }
        }

        //Instantiate the object in the player hand
        IEnumerator InstantiateOnPlayerHands(GameObject itemGO, GameObject player)
        {

            Hand otherPlayerHand = null;
            while (!otherPlayerHand)
            {
                if (player)
                {
                    otherPlayerHand = player.GetComponent<CharacterBase>().RightInteractorReference.GetComponentInChildren<Hand>();
                }
                yield return null;
            }

            itemGO.transform.SetParent(otherPlayerHand.gameObject.transform);

            itemGO.transform.localPosition = Vector3.zero;
        }

        //Set an item to active false
        public void DeActivateItem(int itemPos)
        {
            //should get the position of tjhe item in thje list when user wants to spawn it. Then get the instantiatedClientObjects[] in the same position. get the viewId and despawn it everywhere 
            int toDeActivateID = instantiatedClientObjects[itemPos].GetComponent<PhotonView>().ViewID;

            //call the rpc to deactivate the item with that id
            photonView.RPC("RPC_DeActivateItem", Photon.Pun.RpcTarget.OthersBuffered, toDeActivateID);

        }

        //Activate the item aross all the other clients
        public void ActivateItem(int itemPos)
        {
            //should get the position of tjhe item in thje list when user wants to spawn it. Then get the instantiatedClientObjects[] in the same position. get the viewId and despawn it everywhere 
            int toActivateID = instantiatedClientObjects[itemPos].GetComponent<PhotonView>().ViewID;

            //call the rpc to deactivate the item with that id
            photonView.RPC("RPC_ActivateItem", Photon.Pun.RpcTarget.OthersBuffered, toActivateID);

        }

        #endregion

    }
}
