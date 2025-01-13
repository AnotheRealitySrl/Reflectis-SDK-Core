using UnityEditor;

using UnityEngine;

namespace Reflectis.SDK.Utilities.Editor
{
    /// <summary>
    /// Custom editor for an <see cref="WaypointPositioner"/>.
    /// </summary>
    [CustomEditor(typeof(WaypointPositioner))]
    public class WaypointPositionerEditor : UnityEditor.Editor
    {
        WaypointPositioner waypointPositioner;

        ///////////////////////////////////////////////////////////////////////////
        private void OnEnable()
        {
            waypointPositioner = (WaypointPositioner)target;
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Write the custom inspector
        /// </summary>
        public override async void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("objectToMove"));

            SerializedProperty lerpMovement = serializedObject.FindProperty("lerpMovement");
            EditorGUILayout.PropertyField(lerpMovement);
            if (lerpMovement.boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("lerpRotationSpeed"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("lerpTranslationSpeed"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("lerpMovementType"));
            }

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("waypoints"));
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Editor");
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("editorParameters"));

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();

                EditorUtility.SetDirty(waypointPositioner);
                PrefabUtility.RecordPrefabInstancePropertyModifications(waypointPositioner);
            }

            // Runtime buttons.
            if (Application.isPlaying)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Runtime");
                EditorGUILayout.Space();
                if (!waypointPositioner.Initialized)
                {
                    if (GUILayout.Button("Press to init!"))
                    {
                        await waypointPositioner.MoveToFirstWaypoint();
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("Current Waypoint", waypointPositioner.LastWaypointIndex.ToString());
                    EditorGUILayout.Space();

                    EditorGUILayout.BeginHorizontal();

                    GUI.enabled = waypointPositioner.LastWaypointIndex > 0;
                    if (GUILayout.Button("First <<"))
                    {
                        await waypointPositioner.MoveToFirstWaypoint();
                    }
                    if (GUILayout.Button("Previous <"))
                    {
                        await waypointPositioner.MoveToPreviousWaypoint();
                    }
                    GUI.enabled = waypointPositioner.LastWaypointIndex < waypointPositioner.waypoints.Length - 1;
                    if (GUILayout.Button("> Next"))
                    {
                        await waypointPositioner.MoveToNextWaypoint();
                    }
                    if (GUILayout.Button(">> Last"))
                    {
                        await waypointPositioner.MoveToLastWaypoint();
                    }
                    GUI.enabled = true;

                    EditorGUILayout.EndHorizontal();

                    if (GUILayout.Button("Do Full Trip"))
                    {
                        await waypointPositioner.DoFullTrip();
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Create an Handle for each waypoint, so the positions of the waypoints
        /// can be edited directly in the scene
        /// </summary>
        public void OnSceneGUI()
        {
            if (waypointPositioner.waypoints == null) return;

            for (int i = 0; i < waypointPositioner.waypoints.Length; ++i)
            {
                //Choose the correct color
                if (i == 0) Handles.color = waypointPositioner.editorParameters.firstWaypointColor;
                else Handles.color = waypointPositioner.editorParameters.waypointColor;

                //Draw a line to connect handles
                if (i < waypointPositioner.waypoints.Length - 1 && waypointPositioner.editorParameters.linkWaypoints)
                {
                    Handles.DrawLine(waypointPositioner.waypoints[i].pos, waypointPositioner.waypoints[i + 1].pos);
                }

                //Move the handle and apply the position to the waypoint
                Handles.ConeHandleCap(1, waypointPositioner.waypoints[i].pos, Quaternion.Euler(waypointPositioner.waypoints[i].rot), waypointPositioner.editorParameters.waypointIconSize, EventType.Repaint);

                if (waypointPositioner.editorParameters.showHandles)
                {
                    EditorGUI.BeginChangeCheck();

                    Vector3 newTargetPosition = Handles.PositionHandle(waypointPositioner.waypoints[i].pos, Quaternion.Euler(waypointPositioner.waypoints[i].rot));
                    Quaternion newTargetRotation = Handles.RotationHandle(Quaternion.Euler(waypointPositioner.waypoints[i].rot), waypointPositioner.waypoints[i].pos);
                    if (EditorGUI.EndChangeCheck())
                    {
                        waypointPositioner.waypoints[i].pos = newTargetPosition;
                        waypointPositioner.waypoints[i].rot = newTargetRotation.eulerAngles;
                        EditorUtility.SetDirty(waypointPositioner);
                        PrefabUtility.RecordPrefabInstancePropertyModifications(waypointPositioner);
                    }
                }
            }
        }
    }
}
