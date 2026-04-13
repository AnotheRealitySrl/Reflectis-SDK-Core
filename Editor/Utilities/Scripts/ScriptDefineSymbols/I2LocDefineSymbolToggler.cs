// ============================================================
// I2LocDefineSymbolToggler.cs  (Editor only)
//
// Automatically adds or removes the scripting define symbol
// "I2LOC" depending on whether the I2Loc package is present
// in the project. Runs every time scripts are recompiled.
// ============================================================
using System;
using UnityEditor;
using UnityEngine;
using Reflectis.SDK.Core.Editor;

[InitializeOnLoad]
public static class I2LocDefineSymbolToggler
{
    const string DEFINE = "I2LOC";
    const string I2LOC_TYPE = "I2.Loc.LocalizationManager";

    static I2LocDefineSymbolToggler()
    {
        bool present = IsI2LocPresent();
        bool hasDefine = ScriptDefineSymbolsUtilities.HasScriptingDefineSymbol(DEFINE);

        if (present == hasDefine)
            return;

        if (present)
        {
            ScriptDefineSymbolsUtilities.AddScriptingDefineSymbolToAllBuildTargetGroups(DEFINE);
            Debug.Log($"[I2LocDefineSymbolToggler] I2Loc detected — added scripting define '{DEFINE}'.");
        }
        else
        {
            ScriptDefineSymbolsUtilities.RemoveScriptingDefineSymbolFromAllBuildTargetGroups(DEFINE);
            Debug.Log($"[I2LocDefineSymbolToggler] I2Loc not found — removed scripting define '{DEFINE}'.");
        }
    }

    static bool IsI2LocPresent()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (assembly.GetType(I2LOC_TYPE) != null)
                return true;
        }
        return false;
    }
}
