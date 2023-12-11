using System.IO;
using UnityEditor;
using UnityEngine;
using WebSocketSharp;

namespace Reflectis.SDK.Utilities.Editor
{
    public class RestartProjectEditorWindow : EditorWindow
    {
        private const string DEFAULT_TEXT = "Are you sure you want to restart the project?";

        private static string displayText = "";
        [MenuItem("Tools/RestartProject")]
        public static void ShowRestartProjectWindow()
        {
            ShowRestartProjectWindow(DEFAULT_TEXT);
        }

        public static void ShowRestartProjectWindow(string text)
        {
            displayText = text.IsNullOrEmpty() ? DEFAULT_TEXT : text;
            var window = GetWindow<RestartProjectEditorWindow>();
            window.titleContent.text = "";
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField(new GUIContent(displayText));
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Restart Project"))
            {
                RestartProject();
            }
        }

        private void RestartProject()
        {
            EditorWindow window = GetWindow<RestartProjectEditorWindow>();
            window.Close();
            EditorApplication.OpenProject(Directory.GetCurrentDirectory());

        }
    }
}
