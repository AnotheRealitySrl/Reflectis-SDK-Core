using UnityEditor;

using UnityEngine;

namespace Reflectis.SDK.Core.Interaction.Editor
{
    [CustomEditor(typeof(Manipulable))]
    public class ManipulableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUIStyle style = new(EditorStyles.label)
            {
                richText = true
            };

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

            if (Application.isPlaying)
            {
                Manipulable manipulable = (Manipulable)target;
                EditorGUILayout.LabelField($"<b>Current state:</b> {manipulable.CurrentInteractionState}", style);
                //EditorGUILayout.LabelField($"<b>Can interact:</b> {manipulable.CanInteract}", style);
            }
        }
    }
}
