using Reflectis.SDK.Core.ApplicationManagement;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Unity.VisualScripting;

using UnityEditor;

using UnityEngine;
using UnityEngine.Events;

namespace Reflectis.SDK.Core.Interaction
{
    //[RequireComponent(typeof(BaseInteractable))]
    public abstract class GenericInteractable : InteractableBehaviourBase, IInteractableBehaviour
    {
        [Flags]
        public enum EGenericInteractableState
        {
            Idle = 0,
            SelectEntering = 1,
            Selected = 2,
            Interacting = 3,
            SelectExiting = 4,
        }

        [Flags]
        public enum EAllowedGenericInteractableState
        {
            Selected = 1,
            Interacting = 2,
            Hovered = 4,
        }

        [Flags]
        public enum EVRGenericInteraction
        {
            RayInteraction = 1,
            Hands = 2
        }


        [SerializeField] private ScriptMachine interactionScriptMachine = null;

        [SerializeField] private ScriptMachine unselectOnDestroyScriptMachine = null;

        [Header("Allowed states")]
        [SerializeField] private EAllowedGenericInteractableState desktopAllowedStates = EAllowedGenericInteractableState.Selected | EAllowedGenericInteractableState.Interacting;
        [SerializeField] private EAllowedGenericInteractableState vrAllowedStates = EAllowedGenericInteractableState.Selected | EAllowedGenericInteractableState.Interacting;
        [SerializeField] private EVRGenericInteraction vrGenericInteraction = (EVRGenericInteraction)~0;

        public Action<GameObject> OnSelectedActionVisualScripting;

        public ScriptMachine InteractionScriptMachine { get => interactionScriptMachine; set => interactionScriptMachine = value; }
        public ScriptMachine UnselectOnDestroyScriptMachine { get => unselectOnDestroyScriptMachine; set => unselectOnDestroyScriptMachine = value; }

        public EAllowedGenericInteractableState DesktopAllowedStates { get => desktopAllowedStates; set => desktopAllowedStates = value; }
        public EAllowedGenericInteractableState VRAllowedStates { get => vrAllowedStates; set => vrAllowedStates = value; }
        public EVRGenericInteraction VrGenericInteraction { get => vrGenericInteraction; set => vrGenericInteraction = value; }

        public bool SkipSelectState => skipSelectState;

        public override bool IsIdleState => CurrentInteractionState == EGenericInteractableState.Idle;

        private EGenericInteractableState currentInteractionState;
        public EGenericInteractableState CurrentInteractionState
        {
            get => currentInteractionState;
            set
            {
                currentInteractionState = value;
                if (currentInteractionState == EGenericInteractableState.Idle)
                {
                    InteractableRef.InteractionState = IInteractable.EInteractionState.Idle;
                }
            }
        }



        protected bool hasHoveredState = false;
        private bool skipSelectState = false;
        private bool hasInteractState = false;

        public List<HoverEnterEventUnit> hoverEnterEventUnits = new List<HoverEnterEventUnit>();

        public List<HoverExitEventUnit> hoverExitEventUnits = new List<HoverExitEventUnit>();

        public List<SelectEnterEventUnit> selectEnterEventUnits = new List<SelectEnterEventUnit>();

        public List<SelectExitEventUnit> selectExitEventUnits = new List<SelectExitEventUnit>();

        public List<InteractEventUnit> interactEventUnits = new List<InteractEventUnit>();

        private List<UnselectOnDestroyUnit> unselectOnDestroyEventUnits = new List<UnselectOnDestroyUnit>();

        private GameObject unselectOnDestroyGameobject;
        private async void OnDestroy()
        {
            if (!IsIdleState && CurrentInteractionState != EGenericInteractableState.SelectExiting)
            {
                foreach (var unit in unselectOnDestroyEventUnits)
                {
                    await unit.AwaitableTrigger(unselectOnDestroyScriptMachine.GetReference().AsReference(), this);
                }
            }
            if (unselectOnDestroyGameobject != null)
            {
                Destroy(unselectOnDestroyGameobject);
            }
        }


        #region UnityEvents Callbacks
        [HideInInspector] public UnityEvent OnHoverGrabEnter = new UnityEvent();
        [HideInInspector] public UnityEvent OnHoverGrabExit = new UnityEvent();
        [HideInInspector] public UnityEvent OnHoverRayEnter = new UnityEvent();
        [HideInInspector] public UnityEvent OnHoverRayExit = new UnityEvent();
        [HideInInspector] public UnityEvent OnHoverMouseEnter = new UnityEvent();
        [HideInInspector] public UnityEvent OnHoverMouseExit = new UnityEvent();
        #endregion


        public override Task Setup()
        {
            switch (SM.GetSystem<IPlatformSystem>().RuntimePlatform)
            {
                case ESupportedPlatform.WebGL:
                    skipSelectState = !DesktopAllowedStates.HasFlag(EAllowedGenericInteractableState.Selected);
                    hasInteractState = DesktopAllowedStates.HasFlag(EAllowedGenericInteractableState.Interacting);
                    hasHoveredState = DesktopAllowedStates.HasFlag(EAllowedGenericInteractableState.Hovered);
                    break;
                case ESupportedPlatform.VR:
                    skipSelectState = !VRAllowedStates.HasFlag(EAllowedGenericInteractableState.Selected);
                    hasInteractState = VRAllowedStates.HasFlag(EAllowedGenericInteractableState.Interacting);
                    hasHoveredState = VRAllowedStates.HasFlag(EAllowedGenericInteractableState.Hovered);
                    break;
            }

            if (interactionScriptMachine != null)
            {
                foreach (var unit in interactionScriptMachine.graph.units)
                {
                    if (unit is HoverEnterEventUnit hoverEnterEventUnit)
                    {
                        hoverEnterEventUnits.Add(hoverEnterEventUnit);
                    }
                    if (unit is HoverExitEventUnit hoverExitEventUnit)
                    {
                        hoverExitEventUnits.Add(hoverExitEventUnit);
                    }
                    if (unit is SelectEnterEventUnit selectEnterEventUnit)
                    {
                        selectEnterEventUnits.Add(selectEnterEventUnit);
                    }
                    if (unit is SelectExitEventUnit selectExitEventUnit)
                    {
                        selectExitEventUnits.Add(selectExitEventUnit);
                    }
                    if (unit is InteractEventUnit interactEventUnit)
                    {
                        interactEventUnits.Add(interactEventUnit);
                    }
                }
            }

            if (unselectOnDestroyScriptMachine != null)
            {
                if (unselectOnDestroyScriptMachine.gameObject == gameObject)
                {
                    Debug.LogError("Unselect on destroy script machine inserted on interactable gameObject." +
                        " This is not allowed please insert the script machine on a different empty gameobject");
                }
                else
                {
                    bool foundUnit = false;
                    foreach (var unit in unselectOnDestroyScriptMachine.graph.units)
                    {
                        if (unit is UnselectOnDestroyUnit unselectOnDestroyEventUnit)
                        {
                            unselectOnDestroyEventUnits.Add(unselectOnDestroyEventUnit);
                            foundUnit = true;
                        }
                    }
                    if (foundUnit)
                    {
                        unselectOnDestroyGameobject = unselectOnDestroyScriptMachine.gameObject;
                        unselectOnDestroyGameobject.transform.parent = null;
                        DontDestroyOnLoad(unselectOnDestroyGameobject);
                    }
                }
            }
            return Task.CompletedTask;
        }

        public override async void OnHoverStateEntered()
        {
            //if (!CanInteract || !hasHoveredState)
            if (CurrentBlockedState != 0 || !hasHoveredState)
                return;

            IEnumerable<Task> hoverEnterUnitsTask = hoverEnterEventUnits.Select(async unit =>
            {
                await unit.AwaitableTrigger(interactionScriptMachine.GetReference().AsReference(), this);
            });

            await Task.WhenAll(hoverEnterUnitsTask);

        }

        public override async void OnHoverStateExited()
        {
            //if (!CanInteract || !hasHoveredState)
            if (CurrentBlockedState != 0 || !hasHoveredState ||
                (LockHoverDuringInteraction && currentInteractionState != EGenericInteractableState.Idle))
                return;

            IEnumerable<Task> hoverExitUnitsTask = hoverExitEventUnits.Select(async unit =>
            {
                if (unit == null || interactionScriptMachine == null)
                {
                    return;
                }
                await unit.AwaitableTrigger(interactionScriptMachine.GetReference().AsReference(), this);
            });



            await Task.WhenAll(hoverExitUnitsTask);

        }

        public override async Task EnterInteractionState()
        {
            //if (!CanInteract)
            if (CurrentBlockedState != 0)
                return;
            if (!SkipSelectState)
            {
                await base.EnterInteractionState();

                CurrentInteractionState = EGenericInteractableState.SelectEntering;


                IEnumerable<Task> selectEnterUnitsTasks = selectEnterEventUnits.Select(async unit =>
                {
                    await unit.AwaitableTrigger(interactionScriptMachine.GetReference().AsReference(), this);
                });

                await Task.WhenAll(selectEnterUnitsTasks);

                CurrentInteractionState = EGenericInteractableState.Selected;
            }
            else
            {
                await Interact();
            }
        }

        public override async Task ExitInteractionState()
        {
            //if (!CanInteract)
            if (CurrentBlockedState != 0)
                return;

            await base.ExitInteractionState();

            if (!SkipSelectState)
            {
                CurrentInteractionState = EGenericInteractableState.SelectExiting;

                IEnumerable<Task> selectExitUnitsTasks = selectExitEventUnits.Select(async unit =>
                {
                    await unit.AwaitableTrigger(interactionScriptMachine.GetReference().AsReference(), this);
                });

                await Task.WhenAll(selectExitUnitsTasks);
            }

            CurrentInteractionState = EGenericInteractableState.Idle;

        }

        public async Task Interact()
        {
            //if (!CanInteract)
            if (CurrentBlockedState != 0)
                return;

            if (CurrentInteractionState != EGenericInteractableState.Selected && hasInteractState && !SkipSelectState)
                return;

            CurrentInteractionState = EGenericInteractableState.Interacting;

            IEnumerable<Task> interactUnitsTasks = interactEventUnits.Select(async unit =>
            {
                await unit.AwaitableTrigger(interactionScriptMachine.GetReference().AsReference(), this);
            });

            await Task.WhenAll(interactUnitsTasks);

            CurrentInteractionState = SkipSelectState ? EGenericInteractableState.Idle : EGenericInteractableState.Selected;

        }


#if UNITY_EDITOR

        [CustomEditor(typeof(GenericInteractable))]
        public class GenericInteractableEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                GenericInteractable interactable = (GenericInteractable)target;

                GUIStyle style = new(EditorStyles.label)
                {
                    richText = true
                };

                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

                if (Application.isPlaying)
                {
                    EditorGUILayout.LabelField($"<b>Current state:</b> {interactable.CurrentInteractionState}", style);
                    //EditorGUILayout.LabelField($"<b>Can interact:</b> {interactable.CanInteract}", style);
                }
            }
        }

#endif
    }
}
