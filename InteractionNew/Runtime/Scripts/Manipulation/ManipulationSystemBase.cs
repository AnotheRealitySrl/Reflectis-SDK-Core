using Reflectis.SDK.CharacterController;
using Reflectis.SDK.Core;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using static Reflectis.SDK.InteractionNew.Manipulable;

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

        public virtual void SetupManipulable(Manipulable manipulable)
        {
            manipulable.BoundingBox = manipulable.InteractableRef.GameObjectRef.GetComponentsInChildren<GenericHookComponent>()
                .FirstOrDefault(x => x.Id == "BoundingBox")?.TransformRef;

            if (manipulable.ManipulationMode.HasFlag(EManipulationMode.Scale))
            {
                SetScalingPoints(manipulable);
            }
        }

        public abstract void SetScalingPoints(Manipulable manipulable);
        public abstract void UpdateScalingPointsPosition(Manipulable manipulable);

        public override void Init()
        {

        }
    }
}
