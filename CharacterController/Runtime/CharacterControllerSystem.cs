using SPACS.Core;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SPACS.SDK.CharacterController
{
    [CreateAssetMenu(menuName = "AnotheReality/Systems/CharacterController/CharacterControllerBase", fileName = "CharacterControllerBaseConfig")]
    public class CharacterControllerSystem : BaseSystem
    {
        #region Inspector variables

        [Header("General")]
        [SerializeField] protected bool spawnCharacterOnInit = false;
        [Header("Character controller")]
        [SerializeField] protected CharacterControllerBase characterControllerPrefab;
        [SerializeField] protected Pose spawnPose;

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
            if (spawnCharacterOnInit)
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

        public virtual void MoveCharacter(Pose newPose)
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
