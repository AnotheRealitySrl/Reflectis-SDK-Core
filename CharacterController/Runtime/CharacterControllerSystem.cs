using SPACS.Core;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SPACS.SDK.CharacterController
{
    public class CharacterControllerSystem : BaseSystem
    {
        #region Inspector variables

        [Header("General")]
        [SerializeField] protected bool SpawnCharacterOnInit = false;
        [Header("Character controller")]
        [SerializeField] protected CharacterControllerBase characterControllerPrefab;
        [SerializeField] protected Pose spawnPose;

        #endregion

        #region Properties

        public CharacterControllerBase CharacterControllerInstance { get; protected set; }

        #endregion

        #region Unity Events

        public UnityEvent<CharacterControllerBase> OnCharacterControllerSetupComplete { get; } = new();

        #endregion

        #region Interface implementation

        public override void Init()
        {
            if (SpawnCharacterOnInit)
                Spawn();
        }

        #endregion

        #region Public API

        //public virtual void Spawn(Pose _startingPose) { 

        //}

        public virtual void Spawn() {

            if (!characterControllerPrefab) {
                CharacterControllerInstance = FindObjectOfType<CharacterControllerBase>();
            } else {
                if (!Instantiate(characterControllerPrefab, spawnPose.position, spawnPose.rotation).TryGetComponent(out CharacterControllerBase characterController)) {
                    throw new Exception("Character controller not found");
                }
                CharacterControllerInstance = characterController;
            }

        }

        public void MoveCharacter(Pose newPose)
        {
            CharacterControllerInstance.transform.SetPositionAndRotation(newPose.position, newPose.rotation);
        }

        public virtual void Destroy() {
            if (CharacterControllerInstance != null)
                Destroy(CharacterControllerInstance.gameObject);
        }

        #endregion
    }

}
