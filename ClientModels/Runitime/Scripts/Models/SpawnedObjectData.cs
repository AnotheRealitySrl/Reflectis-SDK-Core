using System;

using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]

    [Newtonsoft.Json.JsonObject(Newtonsoft.Json.MemberSerialization.Fields)]
    public class SpawnedObjectData
    {
        [SerializeField] private string objectKey;
        [SerializeField] private int ownerId;
        [SerializeField] private Vector3? position;
        [SerializeField] private Quaternion? rotation;
        [SerializeField] private Vector3? scale;
        [SerializeField] private Params parameters;

        // The parameters field can be missing or take different forms, according to the specific SceneObj
        // It could be handled as a free Dictionary, but in order to provide type-safe serialization/deserialization
        // this class includes all possible fields
        public class Params
        {
            // SceneObj: Downloaded3DModel
            public int id;

            // SceneObj: Downloaded3DModel---ColorPicker
            public string modelColor = "";

            // SceneObj: Downloaded3DModel---Explodable
            public float modelExplosionValue;
            public int modelExplosionType;

            // SceneObj: Downloaded3DPickables
            public string addressableString;

            // SceneObj: Drawing
            public float width;
            public Color color;
            public Vector3[] points;
            public bool attached;

            // SceneObj: InstantiatedMediaPlayer
            public int mediaType;
            public float pauseTime;
            public int slideIndex;

            // SceneObj: Downloaded3DModel---ObjctLocker
            public bool? isLocked;
        }

        public string ObjectKey { get => objectKey; set => objectKey = value; }
        public int OwnerId { get => ownerId; set => ownerId = value; }
        public Vector3? Position { get => position; set => position = value; }
        public Quaternion? Rotation { get => rotation; set => rotation = value; }
        public Vector3? Scale { get => scale; set => scale = value; }
        public Params Parameters { get => parameters; set => parameters = value; }
    }

}
