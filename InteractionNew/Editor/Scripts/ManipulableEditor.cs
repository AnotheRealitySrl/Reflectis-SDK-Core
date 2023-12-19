using Reflectis.SDK.InteractionNew;

using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(Manipulable))]
public class ManipulableEditor : Editor
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
            EditorGUILayout.LabelField($"<b>Can manipulate:</b> id: {manipulable.CanManipulate}", style);
        }
    }
}