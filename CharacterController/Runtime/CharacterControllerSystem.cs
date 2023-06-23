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

        [Header("Character controller settings")]
        [SerializeField] protected bool spawnCharacterOnInit = false;
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
                CreateCharacterControllerInstance();
        }

        #endregion

        #region Public API

        public virtual void CreateCharacterControllerInstance() 
        {
            CharacterControllerInstance = characterControllerPrefab 
                ? Instantiate(characterControllerPrefab, spawnPose.position, spawnPose.rotation).GetComponent<CharacterControllerBase>()
                : CharacterControllerInstance = FindObjectOfType<CharacterControllerBase>();
        }

        public virtual void DestroyCharacterControllerInstance()
        {
            if (CharacterControllerInstance != null)
                Destroy(CharacterControllerInstance.gameObject);
        }

        public virtual void MoveCharacter(Pose newPose)
        {
            CharacterControllerInstance.transform.SetPositionAndRotation(newPose.position, newPose.rotation);
        }

        #endregion
    }

}
