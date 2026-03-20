// ============================================================
// UIToolkitDefineSymbolToggler.cs  (Editor only)
//
// Automatically adds or removes the scripting define symbol
// "UITOOLKIT" depending on whether the UI Toolkit package
// (UnityEngine.UIElements) is available in the project.
// Runs every time scripts are recompiled.
// ============================================================
using System;
using UnityEditor;
using UnityEngine;

namespace Reflectis.SDK.Core.Editor
{
    [InitializeOnLoad]
    public static class UIToolkitDefineSymbolToggler
    {
        const string DEFINE = "UITOOLKIT";
        const string UITOOLKIT_TYPE = "UnityEngine.UIElements.VisualElement";

        static UIToolkitDefineSymbolToggler()
        {
            bool present = IsUIToolkitPresent();
            bool hasDefine = ScriptDefineSymbolsUtilities.HasScriptingDefineSymbol(DEFINE);

            if (present && !hasDefine)
            {
                ScriptDefineSymbolsUtilities.AddScriptingDefineSymbolToAllBuildTargetGroups(DEFINE);
                Debug.Log($"[UIToolkitDefineSymbolToggler] UI Toolkit detected — added scripting define '{DEFINE}'.");
            }
            else if (!present && hasDefine)
            {
                ScriptDefineSymbolsUtilities.RemoveScriptingDefineSymbolFromAllBuildTargetGroups(DEFINE);
                Debug.Log($"[UIToolkitDefineSymbolToggler] UI Toolkit not found — removed scripting define '{DEFINE}'.");
            }
        }

        static bool IsUIToolkitPresent()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetType(UITOOLKIT_TYPE) != null)
                    return true;
            }
            return false;
        }
    }
}
