using SPACS.Extra.Runtime;
using UnityEditor;
using UnityEngine;

namespace SPACS.Extra
{
    /// <summary>
    /// Custom editor for an <see cref="WaypointPositioner"/>.
    /// </summary>
    [CustomEditor(typeof(WaypointPositioner))]
    public class WaypointPositionerEditor : Editor
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
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("objectToMove"));
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
                if (i == 0) Handles.color = waypointPositioner.editorParameters.firstHandleColor;
                else Handles.color = waypointPositioner.editorParameters.handleColor;

                EditorGUI.BeginChangeCheck();

                //Draw a line to connect handles
                if (i < waypointPositioner.waypoints.Length - 1 && waypointPositioner.editorParameters.linkWaypoints)
                {
                    Handles.DrawLine(waypointPositioner.waypoints[i].pos, waypointPositioner.waypoints[i + 1].pos);
                }

                //Move the handle and apply the position to the waypoint
                Handles.ConeHandleCap(1, waypointPositioner.waypoints[i].pos, Quaternion.Euler(waypointPositioner.waypoints[i].rot), waypointPositioner.editorParameters.handleSize, EventType.Repaint);
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
