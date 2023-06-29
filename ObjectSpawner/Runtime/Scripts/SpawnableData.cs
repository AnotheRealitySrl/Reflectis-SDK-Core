using UnityEngine;

namespace SPACS.SDK.ObjectSpawner
{
    [CreateAssetMenu(menuName = "SPACS/SDK-ObjectSpawner/SpawnableData", fileName = "SpawnableData")]
    public class SpawnableData : ScriptableObject
    {
        [SerializeField]
        private string objAddressable;
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
        private bool onlyOneNpc = false;

        public string ObjAddressable { get => objAddressable; }
        public int FovAngle { get => fovAngle; }
        public int RayCount { get => rayCount; }
        public float ViewDistance { get => viewDistance; }
        public float StartingAngle { get => startingAngle; }
        public Vector3 OriginOffset { get => originOffset; }
        public LayerMask LayerMask { get => layerMask; }
        public bool OnlyOneNpc { get => onlyOneNpc; }

    }
}
