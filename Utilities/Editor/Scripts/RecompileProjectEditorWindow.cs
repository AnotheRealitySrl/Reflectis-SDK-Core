using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace Reflectis.SDK.Utilities.Editor
{
    public class RecompileProjectEditorWindow : EditorWindow
    {
        private const string DEFAULT_TEXT = "Are you sure you want to recompile the project?";

        private static string displayText = "";

        private static RecompileProjectEditorWindow window;
        [MenuItem("Tools/RecompileProject")]
        public static void ShowRecompileProjectWindow()
        {
            ShowRecompileProjectWindow(DEFAULT_TEXT);
        }

        public static void ShowRecompileProjectWindow(string text)
        {
            displayText = string.IsNullOrEmpty(text) ? DEFAULT_TEXT : text;
            if (window == null)
            {
                window = GetWindow<RecompileProjectEditorWindow>();
            }
            window.Show();
            window.titleContent.text = "Recompile Project";
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField(new GUIContent(displayText));
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Recompile Project"))
            {
                RecompileProject();
            }
        }

        public static void RecompileProject()
        {
            window.Close();
#if UNITY_2019_3_OR_NEWER
            CompilationPipeline.RequestScriptCompilation();
#elif UNITY_2017_1_OR_NEWER
            var editorAssembly = Assembly.GetAssembly(typeof(Editor));
            var editorCompilationInterfaceType = editorAssembly.GetType("UnityEditor.Scripting.ScriptCompilation.EditorCompilationInterface");
            var dirtyAllScriptsMethod = editorCompilationInterfaceType.GetMethod("DirtyAllScripts", BindingFlags.Static | BindingFlags.Public);
            dirtyAllScriptsMethod.Invoke(editorCompilationInterfaceType, null);
#endif

        }
    }
}
