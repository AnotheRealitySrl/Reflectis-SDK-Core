#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace Reflectis.SDK.Core.Utilities
{
    public static class BrowserStorage
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    // Importa le funzioni dal file .jslib
    [DllImport("__Internal")]
    private static extern void SaveDataToLocalStorage(string key, string value);

    [DllImport("__Internal")]
    private static extern string LoadDataFromLocalStorage(string key);

    [DllImport("__Internal")]
    private static extern void FreeMemory(string ptr);
#endif

        public static void Save(string key, string value)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        SaveDataToLocalStorage(key, value);
#endif
        }

        public static string Load(string key)
        {
            string data = null;

#if UNITY_WEBGL && !UNITY_EDITOR
        // Nota: la gestione della memoria è complessa.
        // Il codice .jslib sopra alloca memoria che C# deve liberare.
        // Un approccio più semplice è quando JS ritorna una stringa
        // come nel nostro "LoadDataFromLocalStorage".
        // Unity gestisce la conversione del puntatore (buffer) in stringa.
        return LoadDataFromLocalStorage(key);
        
        /* // Se il metodo JS ritornasse un puntatore (ptr), faresti:
        System.IntPtr ptr = LoadDataFromLocalStorage(key);
        if (ptr == System.IntPtr.Zero) return null;
        string data = Marshal.PtrToStringUTF8(ptr);
        FreeMemory(ptr); // Libera la memoria allocata da JS
        return data;
        */
#endif

            return data;
        }
    }

}
