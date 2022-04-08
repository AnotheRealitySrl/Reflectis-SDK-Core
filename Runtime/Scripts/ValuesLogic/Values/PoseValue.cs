using UnityEngine;
using UnityEngine.Events;
using SPACS.Utilities;

namespace SPACS.Utilities
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A pose wrapper. A "pose" is a combination of a position and a rotation.
    /// This component will control the current GameObject transform pose.
    /// </summary>
    public class PoseValue : BaseValue
    {
        [Header("References")]
        [Tooltip("A transform to use as root (optional). This will affect the world-referenced value of this component")]
        [SerializeField]
        private Transform rootTransform = default;

        [Header("Events")]
        [Tooltip("Update event fired when the local pose changes")]
        [SerializeField]
        internal UnityEvent<Pose> OnLocalPoseUpdate = default;

        [Tooltip("Update event fired when the world pose changes")]
        [SerializeField]
        internal UnityEvent<Pose> OnWorldPoseUpdate = default;

        private Pose initialPose = default;


        ///////////////////////////////////////////////////////////////////////////
        // Awake method.
        private void Awake()
        {
            initialPose = LocalPose;
        }


        ///////////////////////////////////////////////////////////////////////////
        // Start is called before the first frame update
        private new void Start()
        {
            base.Start();
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copies this local pose values into a transform
        /// </summary>
        /// <param name="t">The transform to modify</param>
        public void ApplyLocalPoseTo(Transform t)
        {
            t.SetPose(LocalPose);
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copies this world pose values into a transform
        /// </summary>
        /// <param name="t">The transform to modify</param>
        public void ApplyWorldPoseTo(Transform t)
        {
            Pose worldPose = WorldPose;
            t.SetPose(worldPose, Space.World);
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Copies the local pose of this transform
        /// </summary>
        /// <param name="t">The transform to get the pose from</param>
        public void SetPoseFrom(Transform t)
        {
            LocalPose = t.GetPose();
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The local pose of this PoseValue
        /// </summary>
        public Pose LocalPose
        {
            get => transform.GetPose(Space.Self);
            set
            {
                if (isActiveAndEnabled)
                {
                    transform.SetPose(value, Space.Self);
                    if (IsUpdateMode(UpdateEventMode.OnSet) || IsUpdateMode(UpdateEventMode.OnChange))
                        FireEvent();
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The world pose of this PoseValue
        /// </summary>
        public Pose WorldPose
        {
            get
            {
                Pose worldPose = transform.GetPose(Space.World);
                if (rootTransform != null)
                {
                    Pose rootPose = rootTransform.GetPose(Space.World);
                    worldPose = rootPose.Transform(worldPose);
                }

                return worldPose;
            }
            set
            {
                if (isActiveAndEnabled)
                {
                    if (rootTransform != null)
                    {
                        Pose rootPose = rootTransform.GetPose(Space.World);
                        value = rootPose.Transform(value);
                    }

                    transform.SetPose(value, Space.World);

                    if (IsUpdateMode(UpdateEventMode.OnSet) || IsUpdateMode(UpdateEventMode.OnChange))
                        FireEvent();
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The local position X value
        /// </summary>
        public float X
        {
            get => LocalPose.position.x;
            set
            {
                Pose pose = LocalPose;
                pose.position.x = value;
                LocalPose = pose;
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The local position Y value
        /// </summary>
        public float Y
        {
            get => LocalPose.position.y;
            set
            {
                Pose pose = LocalPose;
                pose.position.y = value;
                LocalPose = pose;
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The local position Z value
        /// </summary>
        public float Z
        {
            get => LocalPose.position.z;
            set
            {
                Pose pose = LocalPose;
                pose.position.z = value;
                LocalPose = pose;
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The local rotation pitch value
        /// </summary>
        public float Pitch
        {
            get => LocalPose.rotation.eulerAngles.x;
            set
            {
                Pose pose = LocalPose;
                Vector3 angles = pose.rotation.eulerAngles;
                angles.x = value;
                pose.rotation.eulerAngles = angles;
                LocalPose = pose;
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The local rotation yaw value
        /// </summary>
        public float Yaw
        {
            get => LocalPose.rotation.eulerAngles.y;
            set
            {
                Pose pose = LocalPose;
                Vector3 angles = pose.rotation.eulerAngles;
                angles.y = value;
                pose.rotation.eulerAngles = angles;
                LocalPose = pose;
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The local rotation roll value
        /// </summary>
        public float Roll
        {
            get => LocalPose.rotation.eulerAngles.z;
            set
            {
                Pose pose = LocalPose;
                Vector3 angles = pose.rotation.eulerAngles;
                angles.z = value;
                pose.rotation.eulerAngles = angles;
                LocalPose = pose;
            }
        }


        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Translates the local position using a translation vector
        /// </summary>
        /// <param name="translation">The translation vector</param>
        public void Translate(Vector3 translation)
        {
            Pose pose = LocalPose;
            pose.position += translation;
            LocalPose = pose;
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary> Translates the pose forward by a certain amount </summary>
        /// <param name="increment">The amount of translation</param>
        public void TranslateForward(float increment) => Translate(LocalPose.forward * increment);

        ///////////////////////////////////////////////////////////////////////////
        /// <summary> Translates the pose backward by a certain amount </summary>
        /// <param name="increment">The amount of translation</param>
        public void TranslateBackward(float increment) => Translate(-LocalPose.forward * increment);

        ///////////////////////////////////////////////////////////////////////////
        /// <summary> Translates the pose left by a certain amount </summary>
        /// <param name="increment">The amount of translation</param>
        public void TranslateLeft(float increment) => Translate(-LocalPose.right * increment);

        ///////////////////////////////////////////////////////////////////////////
        /// <summary> Translates the pose right by a certain amount </summary>
        /// <param name="increment">The amount of translation</param>
        public void TranslateRight(float increment) => Translate(LocalPose.right * increment);

        ///////////////////////////////////////////////////////////////////////////
        /// <summary> Translates the pose up by a certain amount </summary>
        /// <param name="increment">The amount of translation</param>
        public void TranslateUp(float increment) => Translate(LocalPose.up * increment);

        ///////////////////////////////////////////////////////////////////////////
        /// <summary> Translates the pose down by a certain amount </summary>
        /// <param name="increment">The amount of translation</param>
        public void TranslateDown(float increment) => Translate(-LocalPose.up * increment);


        ///////////////////////////////////////////////////////////////////////////
        /// <summary> Rotates the pose around the X axis by a certain amount </summary>
        /// <param name="increment">The amount of rotation</param>
        public void RotatePitch(float increment) => Pitch += increment;

        ///////////////////////////////////////////////////////////////////////////
        /// <summary> Rotates the pose around the Y axis by a certain amount </summary>
        /// <param name="increment">The amount of rotation</param>
        public void RotateYaw(float increment) => Yaw += increment;

        ///////////////////////////////////////////////////////////////////////////
        /// <summary> Rotates the pose around the Z axis by a certain amount </summary>
        /// <param name="increment">The amount of rotation</param>
        public void RotateRoll(float increment) => Roll += increment;


        ///////////////////////////////////////////////////////////////////////////
        /// <summary>Resets the pose of the Transform.</summary>
        public void ResetPose()
        {
            LocalPose = initialPose;
        }


        ///////////////////////////////////////////////////////////////////////////
        protected override void FireEvent()
        {
            OnLocalPoseUpdate.Invoke(LocalPose);
            OnWorldPoseUpdate.Invoke(WorldPose);
        }

    }
}