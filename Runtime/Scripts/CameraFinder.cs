using UnityEngine;

namespace SPACS.Extra.Runtime
{
    /// <summary>
    /// This component search for a main camera and link it to the World Canvas
    /// </summary>
    public class CameraFinder : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }

}

