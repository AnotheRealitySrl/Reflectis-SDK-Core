using SPACS.Core;

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

        public ICharacterController CharacterControllerInstance { get; private set; }

        #endregion

        #region Interface implementation

        public override void Init()
        {
            if (Instantiate(characterControllerPrefab, spawnPose.position, spawnPose.rotation).TryGetComponent(out ICharacterController CharacterControllerInstance))
            {
                this.CharacterControllerInstance = CharacterControllerInstance;
            }
        }

        #endregion
    }

}
