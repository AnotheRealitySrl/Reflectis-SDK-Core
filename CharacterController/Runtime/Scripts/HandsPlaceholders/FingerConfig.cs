using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    [Serializable]
    public class FingerConfig
    {
        [SerializeField] private Quaternion[] minGripRotPose;
        [SerializeField] private Vector3[] minGripPosPose;
        [SerializeField] private Quaternion[] maxGripRotPose;
        [SerializeField] private Vector3[] maxGripPosPose;

        public Quaternion[] MinGripRotPose { get => minGripRotPose; set => minGripRotPose = value; }
        public Vector3[] MinGripPosPose { get => minGripPosPose; set => minGripPosPose = value; }
        public Quaternion[] MaxGripRotPose { get => maxGripRotPose; set => maxGripRotPose = value; }
        public Vector3[] MaxGripPosPose { get => maxGripPosPose; set => maxGripPosPose = value; }
    }
}

