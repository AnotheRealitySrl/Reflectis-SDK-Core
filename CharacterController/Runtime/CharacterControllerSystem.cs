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
        [SerializeField] protected CharacterControllerBase characterControllerPrefab;
        [SerializeField] protected Pose spawnPose;

        #endregion

        #region Properties

        public CharacterControllerBase CharacterControllerInstance { get; private set; }

        #endregion

        #region Interface implementation

        public override void Init()
        {
            Instantiate(characterControllerPrefab.gameObject, spawnPose.position, spawnPose.rotation).GetComponent<CharacterControllerBase>();
        }

        #endregion
    }

}
