using UnityEngine;

namespace Reflectis.SDK.Core.ApiSystem
{
    /// <summary>
    /// Generic base class for all static API classes.
    /// Provides automatic discovery and creation of the companion ScriptableObject.
    /// </summary>
    /// <typeparam name="TData">The ApiDataBase-derived ScriptableObject type</typeparam>
    public abstract class ApiBase<TData> where TData : ApiDataBase
    {
        protected static TData _data;

        /// <summary>
        /// Access the companion ScriptableObject data.
        /// In Editor: auto-finds or creates the SO asset if not initialized.
        /// In Runtime: must be initialized via Initialize() first.
        /// </summary>
        public static TData Data => _data ??= FindOrCreateData();

        /// <summary>
        /// Explicit initialization (used by SM wrapper at runtime or manual setup).
        /// </summary>
        public static void Initialize(TData data) => _data = data;

        /// <summary>
        /// Searches the project for an existing SO of type TData.
        /// If none exists, creates one in Assets/ApiData/.
        /// Only available in Editor; throws at runtime if not initialized.
        /// </summary>
        protected static TData FindOrCreateData()
        {
#if UNITY_EDITOR
            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(TData).Name);
            if (guids.Length > 0)
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                TData existing = UnityEditor.AssetDatabase.LoadAssetAtPath<TData>(path);
                if (existing != null)
                {
                    return existing;
                }
            }

            TData newData = ScriptableObject.CreateInstance<TData>();

            const string parentFolder = "Assets";
            const string subFolder = "ApiData";
            string folder = $"{parentFolder}/{subFolder}";

            if (!UnityEditor.AssetDatabase.IsValidFolder(folder))
            {
                UnityEditor.AssetDatabase.CreateFolder(parentFolder, subFolder);
            }

            string assetPath = $"{folder}/{typeof(TData).Name}.asset";
            UnityEditor.AssetDatabase.CreateAsset(newData, assetPath);
            UnityEditor.AssetDatabase.SaveAssets();
            Debug.Log($"[ApiBase] Created new {typeof(TData).Name} at {assetPath}");
            return newData;
#else
            throw new System.InvalidOperationException(
                $"{typeof(TData).Name} not initialized. Call {typeof(TData).Name}.Initialize() before use at runtime.");
#endif
        }
    }
}
