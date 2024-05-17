#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using Reflectis.SDK.Core;

using System;

using UnityEngine;
using UnityEngine.Events;

using System.Threading.Tasks;

namespace Reflectis.SDK.CharacterController
{
    [CreateAssetMenu(menuName = "Reflectis/SDK-CharacterController/CharacterControllerBaseSystemConfig", fileName = "CharacterControllerBaseSystemConfig")]
    public class CharacterControllerSystem : BaseSystem, ICharacterControllerSystem
    {
        #region Inspector variables

        [Header("Initialization")]
        [SerializeField, Tooltip("Create a character controller instance on system init")]
        protected bool createCharacterControllerInstanceOnInit = true;

        [SerializeField, Tooltip("Is the character conroller already in scene or should be instantiated from a prefab?")]
        private bool characterControllerAlreadyInScene;

        [Header("Character controller instantiation")]
#if ODIN_INSPECTOR
        [HideIf(nameof(characterControllerAlreadyInScene))]
#endif
        [SerializeField, Tooltip("Reference to the character controller prefab")]
        protected CharacterControllerBase characterControllerPrefab;

#if ODIN_INSPECTOR
        [HideIf(nameof(characterControllerAlreadyInScene))]
#endif
        [SerializeField, Tooltip("Spawn position and rotation of the character controller")]
        protected Pose spawnPose;

        #endregion

        private int interactionCount = 0;

        #region Properties

        public CharacterControllerBase CharacterControllerInstance { get; protected set; }

        #endregion

        #region Unity Events

        public UnityEvent<CharacterBase> OnCharacterControllerSetupComplete { get; } = new();

        #endregion

        #region Interface implementation

        public override Task Init()
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
            return base.Init();
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
            //CharacterControllerInstance.transform.SetPositionAndRotation(newPose.position, newPose.rotation); //---> this was working with the oculus sdk but doesn't work well with OpenXR

            //position
            //Because of the OpenXR Plugin the main camera gets an offset at the start of the scene, so we can handle that offset by "removing" it from the character instance
            Vector3 offset = Camera.main.transform.position - CharacterControllerInstance.transform.position;
            offset.y = 0;
            CharacterControllerInstance.transform.position = newPose.position - offset;

            //rotation
            //Because of the OpenXRPlugin The camera also rotates, so we handle this by rotating our character by the angle offset
            Vector3 targetForward = newPose.forward;
            Vector3 cameraForward = Camera.main.transform.forward;

            //we don't care about up and down, so set the y to 0
            targetForward.y = 0;
            cameraForward.y = 0;

            float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);
            CharacterControllerInstance.transform.RotateAround(Camera.main.transform.position, Vector3.up, angle);
        }

        public virtual void ActivateReactionAnimation(string reactionName) { }

        public virtual void EnableCharacterMovement(bool value) { }

        public virtual void EnableCharacterJump(bool value) { }

        public virtual void EnableCameraRotation(bool value) { }

        public virtual void EnableCameraZoom(bool value) { }

        public virtual Task GoToInteractState(Transform targetTransform) => Task.CompletedTask;

        public virtual Task GoToSetMovementState() => Task.CompletedTask;

        public virtual void EnableCharacterGravity(bool enable) { }

        public virtual int ManageCounterCharacterInteraction(bool activate)
        {
            if (activate)
            {
                interactionCount++;
            }
            else
            {
                interactionCount--;
            }
            return interactionCount;
        }

        #endregion
    }

}
