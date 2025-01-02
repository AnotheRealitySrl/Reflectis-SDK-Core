using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.ClientModels
{
    [Serializable]
    public class CMWorldCCU
    {
        [SerializeField] private int id;
        [SerializeField] private int ccu;

        public int Id { get => id; set => id = value; }
        public int CCU { get => ccu; set => ccu = value; }
    }
}
