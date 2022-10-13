using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace SPACS.Utilities.FadeSystem.Runtime
{
    public abstract class LayerManagerBase : ScriptableObject, ILayerManager
    {
        public List<(GameObject, int)> ObjsUnaffectedByFade { get; set; }

        public abstract void UpdateObjsUnaffectedByFade(List<GameObject> objsUnaffectedByFade);

        public virtual void ResetObjsUnaffectedByFade()
        {
            ObjsUnaffectedByFade.Clear();
        }

        public abstract void MoveObjectsToLayer();

        public virtual void ResetObjectsLayer()
        {
            foreach (var go in ObjsUnaffectedByFade)
            {
                go.Item1.layer = go.Item2;
            }
        }
    }
}