using System;

using UnityEngine;

namespace Reflectis.SDK.Utilities
{
    ///////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Simple component that allow to move, rotate and scale an object throw different waypoints.
    /// Waypoints can be editetd in scene <see cref="WayPointPositionerEditor"/>.
    /// </summary>
    public class WaypointPositioner : MonoBehaviour
    {
        //Public
        public Transform objectToMove;
        public FixedTransform[] waypoints;

        //Editor
        public EditorParameters editorParameters = new EditorParameters(Color.green, Color.white, 0.5f, true);

        //Private
        private int lastWaypointIndex = 0;
        private Vector3 initialScale = Vector3.zero;
        private bool initialized = false;

        ///////////////////////////////////////////////////////////////////////////
        //Structs
        [Serializable]
        public struct FixedTransform
        {
            [Tooltip("Target position")]
            public Vector3 pos;
            [Tooltip("Target Rotation")]
            public Vector3 rot;
            [Tooltip("Scale multiplier. If 0, reset to initial scale")]
            public float scaleMultiplier;
        }

        /// <summary>
        /// Parameters for the custom editor
        /// </summary>
        [Serializable]
        public struct EditorParameters
        {
            public Color firstHandleColor;
            public Color handleColor;
            public float handleSize;
            public bool linkWaypoints;

            public EditorParameters(Color _firstHandleColor, Color _handleColor, float _handleSize, bool _linkWaypoints)
            {
                firstHandleColor = _firstHandleColor;
                handleColor = _handleColor;
                handleSize = _handleSize;
                linkWaypoints = _linkWaypoints;
            }
        }

        ///////////////////////////////////////////////////////////////////////////
        private void Init()
        {
            if (initialized) return;
            initialScale = objectToMove.localScale;
            lastWaypointIndex = 0;
            initialized = true;
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the referenced object's posistion and rotation according to the next waypoint
        /// </summary>
        public void MoveToNextWaypoint()
        {
            var index = lastWaypointIndex + 1;
            if (index >= waypoints.Length)
                Debug.LogError("There is no next waypoint");
            else
                MoveIn(index);
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the referenced object's posistion and rotation according to the previous waypoint
        /// </summary>
        public void MoveToPreviousWaypoint()
        {
            var index = lastWaypointIndex - 1;
            if (index < 0)
                Debug.LogError("There is no previous waypoint");
            else
                MoveIn(index);
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the referenced object's posistion and rotation according to the fist waypoint
        /// </summary>
        public void MoveToFirstWaypoint()
        {
            MoveIn(0);
        }

        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the referenced object's posistion and rotation according to the last waypoint
        /// </summary>
        public void MoveToLastWaypoint()
        {
            MoveIn(waypoints.Length - 1);
        }


        ///////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the referenced object's posistion and rotation according to the waypoint
        /// </summary>
        /// <param name="wayPointIndex">index of the waypoint the object has to move to</param>
        public void MoveIn(int wayPointIndex)
        {
            if (!initialized)
                Init();

            lastWaypointIndex = wayPointIndex;
            objectToMove.position = waypoints[wayPointIndex].pos;
            objectToMove.eulerAngles = waypoints[wayPointIndex].rot;
            var scaleMultiplier = waypoints[wayPointIndex].scaleMultiplier;
            if (scaleMultiplier == 0)
                objectToMove.localScale = initialScale;
            else
                objectToMove.localScale *= scaleMultiplier;
        }
    }
}
