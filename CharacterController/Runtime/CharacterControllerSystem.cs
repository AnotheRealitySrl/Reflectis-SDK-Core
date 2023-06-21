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

        /// <summary>
        /// Characte controller instance
        /// </summary>
        public CharacterControllerBase CharacterControllerInstance { get; protected set; }

        #endregion

        #region Unity Events

        /// <summary>
        /// Invoked when the character controller setup is completed
        /// </summary>
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

        /// <summary>
        /// Creates the character controller instance. 
        /// A character controller can be referenced as a prefab through the serialized field `characterControllerPrefab`, or can be already in scene
        /// </summary>
        public virtual void CreateCharacterControllerInstance() 
        {
            CharacterControllerInstance = characterControllerPrefab 
                ? Instantiate(characterControllerPrefab, spawnPose.position, spawnPose.rotation).GetComponent<CharacterControllerBase>()
                : CharacterControllerInstance = FindObjectOfType<CharacterControllerBase>();
        }

        /// <summary>
        /// Destroys the character controller instance
        /// </summary>
        public virtual void DestroyCharacterControllerInstance()
        {
            if (CharacterControllerInstance != null)
                Destroy(CharacterControllerInstance.gameObject);
        }

        /// <summary>
        /// Moves the character controller to a destination position and rotation
        /// </summary>
        /// <param name="newPose">The destination pose of the character</param>
        public virtual void MoveCharacter(Pose newPose)
        {
            CharacterControllerInstance.transform.SetPositionAndRotation(newPose.position, newPose.rotation);
        }


        #endregion
    }

}
