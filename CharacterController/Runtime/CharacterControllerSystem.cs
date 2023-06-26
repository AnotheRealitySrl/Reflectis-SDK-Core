using SPACS.Core;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SPACS.SDK.CharacterController
{
    [CreateAssetMenu(menuName = "SPACS/SDK-CharacterController/CharacterControllerBaseSystemConfig", fileName = "CharacterControllerBaseSystemConfig")]
    public class CharacterControllerSystem : BaseSystem, ICharacterControllerSystem
    {
        #region Inspector variables

        [Header("Initialization")]
        [SerializeField, Tooltip("Create a character controller instance on system init")] 
        protected bool createCharacterControllerInstanceOnInit = true;

        [SerializeField, Tooltip("Is the character conroller already in scene or should be instantiated from a prefab?")]
        private bool characterControllerAlreadyInScene;


        [SerializeField, Tooltip("Reference to the character controller. It can be already a GameObject in scene or a prefab in the Assets folder")]
        protected CharacterControllerBase characterControllerPrefab;
        
        [SerializeField, Tooltip("Spawn position and rotation of the character controller")]
        protected Pose spawnPose;

        #endregion

        #region Properties

        public CharacterControllerBase CharacterControllerInstance { get; protected set; }

        #endregion

        #region Unity Events

        public UnityEvent<CharacterBase> OnCharacterControllerSetupComplete { get; } = new();

        #endregion

        #region Interface implementation

        public override void Init()
        {
            if (createCharacterControllerInstanceOnInit)
            {
                if (characterControllerAlreadyInScene)
                {
                    if (FindObjectOfType<CharacterControllerBase>() is CharacterControllerBase characterController)
                    {
                        CreateCharacterControllerInstance(characterController);
                    }
                    else
                    {
                        throw new Exception("Character controller not found in scene");
                    }
                }
                else
                {
                    if (characterControllerPrefab)
                    {
                        CreateCharacterControllerInstance(characterControllerPrefab);
                    }
                    else
                    {
                        throw new Exception("Character controller prefab not specified");
                    }
                }
            }
        }

        #endregion

        #region Public API

        public virtual void CreateCharacterControllerInstance(CharacterControllerBase characterController) 
        {
            // Destroys the old character controller instance
            if (CharacterControllerInstance)
            {
                DestroyCharacterControllerInstance();
            }

            // Checks if the referenced character controller is already in scene
            CharacterControllerInstance = string.IsNullOrEmpty(characterController.gameObject.scene.name)
                ? Instantiate(characterController, spawnPose.position, spawnPose.rotation).GetComponent<CharacterControllerBase>()
                : characterController;
        }

        public virtual void DestroyCharacterControllerInstance()
        {
            if (CharacterControllerInstance)
            {
                Destroy(CharacterControllerInstance.gameObject);
            }

            CharacterControllerInstance = null;
        }

        public virtual void MoveCharacter(Pose newPose)
        {
            CharacterControllerInstance.transform.SetPositionAndRotation(newPose.position, newPose.rotation);
        }

        #endregion
    }

}
