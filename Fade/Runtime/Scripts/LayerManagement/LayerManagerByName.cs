using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace SPACS.SDK.Fade
{
    [CreateAssetMenu(menuName = "SPACS/SDK-Fade/LayerManagerConfig", fileName = "LayerManagerByNameConfig")]
    public class LayerManagerByName : LayerManagerBase
    {
        [SerializeField] protected string unaffectedByFadeLayerName = "UnaffectedByFade";

        public override void UpdateObjsUnaffectedByFade(List<GameObject> objsUnaffectedByFade)
        {
            ObjsUnaffectedByFade = objsUnaffectedByFade.Select(x => (x, x.layer)).ToList();
        }

        public override void MoveObjectsToLayer()
        {
            foreach (var go in ObjsUnaffectedByFade)
            {
                go.Item1.layer = LayerMask.NameToLayer(unaffectedByFadeLayerName);
            }
        }
    }
}
