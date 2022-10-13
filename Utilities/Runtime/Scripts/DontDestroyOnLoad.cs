using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.Toolkit.Utilities.Runtime
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
