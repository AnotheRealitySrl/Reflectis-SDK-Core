using Reflectis.SDK.Core;
using UnityEngine;

namespace Reflectis.SDK.RadialMenuUtils
{
    [CreateAssetMenu(fileName = "New Radial Menu", menuName = "RadialMenu/Create New Radial Menu")]
    public class RadialMenuSystem : BaseSystem
    {
        public GameObject radialMenuPrefab;
        public GameObject radialMenuNetworkPrefab;

        public GameObject instantiatedRadialMenu;

        public GameObject InstantiateRadialMenu(bool network)
        {
            if (!network)
            {
                radialMenuPrefab.SetActive(false);
                instantiatedRadialMenu = Instantiate(radialMenuPrefab);
            }
            else
            {
                radialMenuNetworkPrefab.SetActive(false);
                //instantiatedRadialMenu = Instantiate(radialMenuNetworkPrefab);
                instantiatedRadialMenu = Instantiate(radialMenuPrefab);
                //instantiate radialRPCManager too... remember that it is a network element so maybe you have to instantiate it differently
            }

            return instantiatedRadialMenu;
        }
    }
}
