using SPACS.Core;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    public class CharacterControllerSystem : BaseSystem
    {
        #region Inspector variables

        [Header("Character controller")]
        [SerializeField] protected GameObject characterControllerPrefab;
        [SerializeField] protected Pose spawnPose;

        #endregion

        #region Properties

        public ICharacterController CharacterControllerInstance { get; protected set; }

        #endregion

        #region Interface implementation

        public override void Init()
        {
            if (!Instantiate(characterControllerPrefab, spawnPose.position, spawnPose.rotation).TryGetComponent(out ICharacterController characterController))
            {
                throw new Exception("Character controller not found");
            }
            CharacterControllerInstance = characterController;
        }

        #endregion
    }

}
