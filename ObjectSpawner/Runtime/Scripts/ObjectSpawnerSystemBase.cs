using Reflectis.SDK.CharacterController;
using Reflectis.SDK.Core;
using System.Threading.Tasks;
using UnityEngine;

namespace Reflectis.SDK.ObjectSpawner
{
    [CreateAssetMenu(menuName = "Reflectis/SDK-ObjectSpawner/ObjectSpawnerSystemConfig", fileName = "ObjectSpawnerSystemConfig")]

    public class ObjectSpawnerSystemBase : BaseSystem, IObjectSpawnerSystem
    {
        [Header("Spawn check position data")]
        [SerializeField]
        private int fovAngle = 90;
        [SerializeField]
        private int rayCount = 2;
        [SerializeField]
        private float viewDistance = 50f;
        [SerializeField]
        private float startingAngle = 0;
        [SerializeField]
        private Vector3 originOffset = Vector3.zero;
        [SerializeField]
        private LayerMask layerMask;
        [SerializeField]
        [Tooltip("Turn on to spawn objects rotate towards the camera instead of the character")]
        private bool rotateTowardCamera;

        private Transform origin;

        public override Task Init()
        {
            //Get origin
            origin = SM.GetSystem<CharacterControllerSystem>().CharacterControllerInstance.HeadReference;
            return base.Init();
        }

        public virtual async Task<GameObject> CheckEntireFovAndSpawn(GameObject prefab)
        {
            GameObject obj = await InstantiateSceneObj(prefab, GetFreePosition(), GetObjectRotation());
            obj.transform.LookAt(origin.position + originOffset);
            return obj;
        }

        protected Vector3 GetFreePosition()
        {
            //Calculate how many fov cones I can check with the given values
            int cycles = (360 / fovAngle);
            Debug.Log($"Cycles {cycles}");

            float angle = startingAngle - origin.eulerAngles.y;
            for (int i = 0; i < cycles; i++)
            {
                if (IsFovFree(angle))
                {
                    Debug.Log("Found free Fov");

                    //Calculate the free point in the space

                    return origin.position + originOffset + GetVectorFromAngle(angle - (fovAngle / 2)) * viewDistance;
                }
                //Decrease the starting angle by the value of the fov cone
                angle -= fovAngle;
            }
            Debug.LogError($"No Empty space!");
            return origin.position + originOffset + GetVectorFromAngle(startingAngle - origin.eulerAngles.y - (fovAngle / 2)) * viewDistance; // give up and return overlap position
        }


        protected bool IsFovFree(float angle)
        {
            Vector3 v3origin = origin.position + originOffset;
            float angleIncrease = fovAngle / rayCount;


            for (int i = 0; i <= rayCount; i++)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(v3origin, GetVectorFromAngle(angle), out raycastHit, viewDistance, layerMask))
                {
                    Debug.Log("hit");
                    Debug.DrawRay(v3origin, raycastHit.point, Color.green, 2.5f);
                    return false;
                }
                Debug.DrawRay(v3origin, GetVectorFromAngle(angle) * viewDistance, Color.green, 2.5f);

                angle -= angleIncrease;
            }
            return true;
        }

        protected Quaternion GetObjectRotation()
        {
            if (rotateTowardCamera)
            {
                return GetFaceCameraRotation();
            }
            else
            {
                return GetFaceCharacterRotation();
            }
        }

        protected Quaternion GetFaceCharacterRotation()
        {
            return Quaternion.LookRotation(origin.forward);
        }

        protected Quaternion GetFaceCameraRotation()
        {
            return Quaternion.LookRotation(Camera.main.transform.forward);
        }

        private Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
        }

        public virtual Task<GameObject> InstantiateSceneObj(GameObject prefab, Vector3? position, Quaternion? rotation)
        {
            return Task.FromResult(Instantiate(prefab, position.GetValueOrDefault(Vector3.zero), rotation.GetValueOrDefault(Quaternion.identity)));
        }


    }

}
