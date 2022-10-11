using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.Utilities.FadeSystem.Runtime
{
    public interface IFadeManager
    {
        string UnaffectedByFadeLayerName { get; set; }
        List<GameObject> ObjsUnaffectedByFade { get; set; }
        float FadeTime { get; set; }

        void FadeToBlack(System.Action onEnd = null);
        void FadeFromBlack(System.Action onEnd = null);
        void FadeToDesaturated(System.Action onEnd = null);
        void FadeFromDesaturated(System.Action onEnd = null);
    }
}