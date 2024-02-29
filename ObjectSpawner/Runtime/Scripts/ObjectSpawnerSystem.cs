using Reflectis.SDK.Core;
using Reflectis.SDK.CharacterController;

using UnityEngine;
using UnityEngine.AddressableAssets;
using System;
using UnityEngine.InputSystem;
using Reflectis.SDK.CreatorKit;
using System.Threading.Tasks;

namespace Reflectis.SDK.ObjectSpawner
{
    [CreateAssetMenu(menuName = "Reflectis/SDK-ObjectSpawner/ObjectSpawnerSystemConfig", fileName = "ObjectSpawnerSystemConfig")]

    public class ObjectSpawnerSystem : BaseSystem
    {
        private Transform origin;

        public override void Init()
        {
            //Get origin
            origin = SM.GetSystem<CharacterControllerSystem>().CharacterControllerInstance.HeadReference;
        }

        public GameObject CheckEntireFovAndSpawn(SpawnableData data, Transform origin = null) 
        {
            if(origin == null)
            {
                origin = this.origin;
            }
            //Calculate how many fov cones I can check with the given values
            int cycles = (360 / data.FovAngle);
            Debug.Log($"Cycles {cycles}");

            float angle = data.StartingAngle - origin.eulerAngles.y;
            for (int i = 0; i < cycles; i++)
            {
                if (IsFovFree(data, angle, origin))
                {
                    Debug.Log("Fov libera, spawna pawn");

                    //Calculate the free point in the space
                    Vector3 freePos = origin.position + data.OriginOffset + GetVectorFromAngle(angle - (data.FovAngle / 2)) * data.ViewDistance;

                    //Instantiate npc
                    GameObject obj = Instantiate(data.ObjPrefab, freePos, Quaternion.identity);
                    obj.transform.LookAt(origin.position + data.OriginOffset);
                    return obj;
                }
                //Decrease the starting angle by the value of the fov cone
                angle -= data.FovAngle;
            }
            Debug.LogError($"No Empty space! Unable to istantiate {data.ObjPrefab.name}");
            return null;
        }

        private bool IsFovFree(SpawnableData data, float angle, Transform origin)
        {
            Vector3 v3origin = origin.position + data.OriginOffset;
            float angleIncrease = data.FovAngle / data.RayCount;


            for (int i = 0; i <= data.RayCount; i++)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(v3origin, GetVectorFromAngle(angle), out raycastHit, data.ViewDistance, data.LayerMask))
                {
                    Debug.Log("hit");
                    Debug.DrawRay(v3origin, raycastHit.point, Color.green, 2.5f);
                    return false;
                }
                Debug.DrawRay(v3origin, GetVectorFromAngle(angle) * data.ViewDistance, Color.green, 2.5f);

                angle -= angleIncrease;
            }
            return true;
        }

        private Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
        }
    }

}
