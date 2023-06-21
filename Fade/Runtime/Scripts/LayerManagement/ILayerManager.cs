using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPACS.SDK.Fade
{
    public interface ILayerManager
    {
        List<(GameObject, int)> ObjsUnaffectedByFade { get; set; }

        void UpdateObjsUnaffectedByFade(List<GameObject> objsUnaffectedByFade);
        void ResetObjsUnaffectedByFade();
        void MoveObjectsToLayer();
        void ResetObjectsLayer();
    }
}