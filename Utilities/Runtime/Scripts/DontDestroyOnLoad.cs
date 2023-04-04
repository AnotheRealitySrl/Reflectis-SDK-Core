using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.SDK.Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
