using System.Collections.Generic;
using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public class InteractableSubmesh : MonoBehaviour
    {
        [SerializeField]
        private List<Collider> colliders;
        [SerializeField]
        private MeshRenderer renderer;

        public List<Collider> Colliders { get => colliders; set => colliders = value; }
        public MeshRenderer Renderer { get => renderer; set => renderer = value; }
    }
}
