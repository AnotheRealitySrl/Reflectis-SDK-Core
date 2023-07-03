#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using System.Collections.Generic;

using UnityEngine;

namespace SPACS.SDK.Core
{
    /// <summary>
    /// <see cref="SystemsManagerController"/>'s purpose is to reference the systems that need to be created and initialized by the <see cref="SM" (Systems Manager)/>. 
    /// During Awake, each <see cref="BaseSystem"/> scriptable that is added to the <see cref="systems"/> is sent to the <see cref="SM"/> for creation and initialziation.
    /// </summary>
    public class SystemsManagerController : MonoBehaviour
    {
#if ODIN_INSPECTOR
        [InlineEditor]
#endif
        [SerializeField] private List<BaseSystem> systems = new();

        private void Awake()
        {
            SM.LoadAndSetup(systems);
        }

    }
}