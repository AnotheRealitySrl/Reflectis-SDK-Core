using Reflectis.SDK.Core;
using UnityEngine;

namespace Reflectis.SDK.Fade
{
    public class DestroyOnFadeInit : MonoBehaviour
    {

        private void Update()
        {
            if (SM.GetSystem<IFadeSystem>() != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
