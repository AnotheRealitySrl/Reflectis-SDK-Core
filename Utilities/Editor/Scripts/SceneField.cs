using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SPACS.SDK.Utilities.Editor
{
    /// <summary>
    /// Useful field to avoid using strings when referencing scenes in the inspector
    /// </summary>
    [System.Serializable]
    public class SceneField
    {
        [SerializeField]
        private UnityEngine.Object sceneAsset;

        [SerializeField]
        private string name = "";
        public string Name { get { return name; } }

        public SceneField(string name)
        {
            this.name = name;
        }

        // Makes it work with the existing Unity methods (LoadLevel/LoadScene)
        public static implicit operator string(SceneField sceneField) => sceneField.Name;
        public static implicit operator SceneField(string name) => new SceneField(name);
        public override string ToString() => name;

        public override bool Equals(object obj)
        {
            var scene2 = obj as SceneField;
            if (scene2 == null)
                return false;
            return Name.Equals(scene2.Name);
        }

        public override int GetHashCode() => Name.GetHashCode();
    }

#if UNITY_EDITOR
    /// <summary>
    /// Custom property drawer for the SceneField
    /// </summary>
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            EditorGUI.BeginProperty(_position, GUIContent.none, _property);
            SerializedProperty sceneAsset = _property.FindPropertyRelative("sceneAsset");
            SerializedProperty sceneName = _property.FindPropertyRelative("name");
            _position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);
            if (sceneAsset != null)
            {
                EditorGUI.indentLevel = 0;
                sceneAsset.objectReferenceValue = EditorGUI.ObjectField(_position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
                if (sceneAsset.objectReferenceValue != null)
                    sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
            }
            EditorGUI.EndProperty();
        }
    }
#endif
}