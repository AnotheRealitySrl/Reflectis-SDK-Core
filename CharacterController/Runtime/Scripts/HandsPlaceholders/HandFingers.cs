using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.Toolkit.CharacterController.Runtime
{
    [Serializable, CreateAssetMenu(menuName = "AnotheReality/Systems/CharacterController/HandBendingConfig", fileName = "HandBendingConfig")]
    public class HandFingers : ScriptableObject
    {
        [SerializeField] private FingerConfig[] fingerBendingConfigs;

        public FingerConfig[] FingerBendingConfigs { get => fingerBendingConfigs; set => fingerBendingConfigs = value; }
    }
}