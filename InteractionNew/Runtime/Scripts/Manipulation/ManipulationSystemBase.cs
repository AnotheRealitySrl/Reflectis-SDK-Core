using Reflectis.SDK.Core;

using System.Collections.Generic;

using UnityEngine;

namespace Reflectis.SDK.InteractionNew
{
    public abstract class ManipulationSystemBase : BaseSystem, IManipulationSystem
    {
        [SerializeField] protected GameObject scalablePointFacePrefab;
        [SerializeField] protected Vector2 targetScaleFaces = new Vector3(0.15f, 0.15f, 0.15f);

        [Header("Scriptable actions")]
        [SerializeField] protected List<AwaitableScriptableAction> onHoverEnterActions = new();
        [SerializeField] protected List<AwaitableScriptableAction> onHoverExitActions = new();

        public GameObject ScalablePointFacePrefab => scalablePointFacePrefab;

        public List<AwaitableScriptableAction> OnHoverEnterActions => onHoverEnterActions;
        public List<AwaitableScriptableAction> OnHoverExitActions => onHoverExitActions;

        public abstract ModelScaler AssignScaler(Manipulable manipulable);

        public override void Init()
        {

        }
    }
}
