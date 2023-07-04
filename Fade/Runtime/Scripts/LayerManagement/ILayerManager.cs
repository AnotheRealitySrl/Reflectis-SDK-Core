using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.Fade
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