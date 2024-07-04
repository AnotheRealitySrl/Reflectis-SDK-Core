using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;

namespace Reflectis.SDK.Utilities.Editor
{
    public class PrefabUtilitiesWindow : EditorWindow
    {
        private class PrefabWithInstances
        {
            public GameObject prefab;
            public List<PrefabInstancesWithModifications> prefabInstances;
        }

        private class PrefabInstancesWithModifications
        {
            public GameObject prefabInstance;
            public GameObject prefabAsset;
            public List<(PropertyModification, SerializedProperty)> modifications;
        }

        #region Inspector variables

        [SerializeField] private List<GameObject> prefabs;

        #endregion

        #region Private variables

        private List<PrefabWithInstances> prefabsWithInstances = new();
        private List<PrefabWithInstances> prefabsWithModifications = new();

        Dictionary<int, bool> prefabsFoldouts = new();
        Dictionary<int, bool> prefabInstancesFoldouts = new();
        Dictionary<int, bool> prefabModificationsFoldouts = new();
        private Vector2 scrollPosition = Vector2.zero;

        #endregion

        #region Unity callbacks

        [MenuItem("Reflectis/Prefabs utilities window (experimental)")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            GetWindow(typeof(PrefabUtilitiesWindow));
        }

        private void OnGUI()
        {
            PrefabUtilities();
        }

        #endregion

        #region Private methods

        private void PrefabUtilities()
        {
            GUIStyle style = new(EditorStyles.label)
            {
                richText = true,
            };

            ScriptableObject soTarget = this;
            SerializedObject so = new(soTarget);
            SerializedProperty prefabsProperty = so.FindProperty("prefabs");

            EditorGUILayout.PropertyField(prefabsProperty, true);
            so.ApplyModifiedProperties();

            if (GUILayout.Button("Retrieve Prefabs redundant overrids"))
            {
                ShowPrefabsInfo();
            }

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);

            foreach (var prefab in prefabsWithInstances)
            {
                int prefabIndex = prefabsWithInstances.IndexOf(prefab);
                prefabsFoldouts.TryAdd(prefabIndex, false);
                prefabsFoldouts[prefabIndex] = EditorGUILayout.Foldout(prefabsFoldouts[prefabIndex], $"[{prefab.prefabInstances.Count}] {prefab.prefab.name}");
                if (prefabsFoldouts[prefabIndex])
                {
                    foreach (var prefabInstance in prefab.prefabInstances)
                    {
                        int instanceIndex = prefab.prefabInstances.IndexOf(prefabInstance);
                        prefabInstancesFoldouts.TryAdd(instanceIndex, false);
                        prefabInstancesFoldouts[instanceIndex] = EditorGUILayout.Foldout(prefabInstancesFoldouts[instanceIndex], prefabInstance.prefabInstance.name);

                        EditorGUILayout.BeginVertical();
                        EditorGUILayout.LabelField($"<b>Root asset</b>: {prefabInstance.prefabAsset.name}", style);
                        EditorGUILayout.EndVertical();

                        if (prefabInstancesFoldouts[instanceIndex])
                        {
                            foreach (var modification in prefabInstance.modifications)
                            {
                                int modificationIndex = prefabInstance.modifications.IndexOf(modification);
                                prefabModificationsFoldouts.TryAdd(modificationIndex, false);
                                prefabModificationsFoldouts[modificationIndex] = EditorGUILayout.Foldout(prefabModificationsFoldouts[modificationIndex], "Modifications");

                                if (prefabInstancesFoldouts[instanceIndex])
                                {
                                    EditorGUILayout.BeginVertical();
                                    EditorGUILayout.LabelField($"<b>Modifications</b>: {modification.Item1.propertyPath} {modification.Item1.objectReference} {modification.Item2}", style);
                                    EditorGUILayout.EndVertical();
                                }
                            }
                        }
                    }
                }
            }


            if (GUILayout.Button("Clean Prefabs redundant overrides"))
            {
                CleanPrefabsOverrides();
            }

            GUILayout.EndScrollView();
        }

        private void ShowPrefabsInfo()
        {
            prefabsWithModifications.Clear();

            foreach (GameObject prefab in prefabs)
            {
                List<GameObject> prefabInstances = prefab.GetComponentsInChildren<Transform>(true)
                    .Select(x => x.gameObject)
                    .Where(x => x != prefab)
                    .Where(x => PrefabUtility.IsAnyPrefabInstanceRoot(x))
                    .ToList();


                List<PrefabInstancesWithModifications> prefabInstancesWithModifications = new();

                foreach (GameObject prefabInstance in prefabInstances)
                {
                    string prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefab);
                    GameObject prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

                    List<(PropertyModification, SerializedProperty)> modifications = new();

                    PropertyModification[] overrides = PrefabUtility.GetPropertyModifications(prefabInstance);
                    foreach (PropertyModification over in overrides)
                    {
                        Object target = over.target;

                        string propertyPath = over.propertyPath;

                        SerializedProperty serializedProperty = null;
                        if (target)
                        {
                            SerializedObject serializedObject = new(target);
                            serializedProperty = serializedObject.FindProperty(propertyPath);
                        }

                        string propertyValue = over.value;
                        Object objectReference = over.objectReference;

                        modifications.Add((over, serializedProperty));
                    }

                    PrefabInstancesWithModifications prefabInstanceWithModifications = new() { prefabInstance = prefabInstance, prefabAsset = prefabAsset, modifications = modifications };
                    prefabInstancesWithModifications.Add(prefabInstanceWithModifications);
                }
                PrefabWithInstances prefabWithInstances = new() { prefab = prefab, prefabInstances = prefabInstancesWithModifications };
                prefabsWithInstances.Add(prefabWithInstances);
            }

            EditorApplication.ExecuteMenuItem("File/Save Project");
        }

        private void CleanPrefabsOverrides()
        {
            foreach (var prefab in prefabsWithInstances)
            {
                foreach (var prefabInstance in prefab.prefabInstances)
                {
                    foreach (var modification in prefabInstance.modifications)
                    {
                        if (modification.Item2 != null && modification.Item2.boxedValue != null)
                        {
                            string boxedValueToString = modification.Item2.boxedValue.ToString();
                            if (boxedValueToString == modification.Item1.value)
                            {
                                PrefabUtility.SetPropertyModifications(prefab.prefab, new PropertyModification[] { modification.Item1 });
                            }
                        }
                    }

                }
            }
        }

        #endregion
    }
}
