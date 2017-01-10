using ModEnabler.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

namespace ModEnabler
{
    /// <summary>
    /// Main class that brings it all together
    /// </summary>
    public static class ModsManager
    {
        public const string version = "1.1";

        private static List<Mod> _modsList;

        private static int loadingQueue;

        /// <summary>
        /// Full path to the mods folder
        /// </summary>
        public static string modsFolder { get; private set; }

        /// <summary>
        /// All the mods settings
        /// </summary>
        public static ModsSettingsAsset settings { get; set; }

        /// <summary>
        /// A full list of all the mods loaded (active AND inactive)
        /// </summary>
        public static IEnumerable<Mod> modsList { get { return _modsList; } }

        /// <summary>
        /// Event for when all the mods are finished loading.
        /// Won't be called after <see cref="modsLoaded"/> is set to true.
        /// Example of how to use:
        /// <code>
        /// public class Test : MonoBehaviour
        /// {
        ///     private void Start()
        ///     {
        ///         if(!ModsManager.modsLoaded)
        ///         {
        ///             ModsManager.onModsLoaded.AddListener(Start);
        ///             return;
        ///         }
        ///
        ///         Debug.Log("All mods are loaded at this point");
        ///     }
        /// }
        /// </code>
        /// </summary>
        public static UnityEvent onModsLoaded { get; private set; }

        /// <summary>
        /// Event for when all the mods get reloaded
        /// </summary>
        public static UnityEvent onPreModsLoaded { get; private set; }

        /// <summary>
        /// Event for when a mod gets activated
        /// </summary>
        public static ModEvent onModActivate { get; private set; }

        /// <summary>
        /// Event for when a mod gets deactivated
        /// </summary>
        public static ModEvent onModDeactivate { get; private set; }

        /// <summary>
        /// Serializer for all the text
        /// </summary>
        public static Serializer serializer { get { return settings.serializer; } }

        /// <summary>
        /// Check this to see if all the mods are already loaded
        /// </summary>
        public static bool modsLoaded { get; private set; }

        static ModsManager()
        {
            // Load all the settings
            settings = Resources.Load<ModsSettingsAsset>("Settings");

            if (settings == null)
            {
                Debug.LogError("Failed to load settings asset, something is not right!");
                return;
            }

            onModsLoaded = new UnityEvent();
            onPreModsLoaded = new UnityEvent();
            onModActivate = new ModEvent();
            onModDeactivate = new ModEvent();

            // Create the mods folder
            modsFolder = Path.Combine(Directory.GetParent(Application.dataPath).FullName, settings.modsDirectory.Replace('/', Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar);
            if (!Directory.Exists(modsFolder))
            {
                if (settings.debugLogging)
                    Debug.Log("Mods folder doesn't exist yet, creating it (" + modsFolder + ")");
                Directory.CreateDirectory(modsFolder);
            }

            _modsList = new List<Mod>();
            if (settings.load[(int)LoadingType.BuiltInAtInit])
                LoadBuiltInMods();
            if (settings.load[(int)LoadingType.ExternalAtInit])
                LoadExternalMods();

            new GameObject("ModsManager", typeof(DisposeOnQuit));

            if (settings.debugLogging)
                Debug.Log("Mod Manager is initialized");
        }

        /// <summary>
        /// Reload all available mods
        /// </summary>
        public static void ReloadAllMods()
        {
            if (settings.debugLogging)
                Debug.Log("Reloading all mods");

            modsLoaded = false;

            onPreModsLoaded.Invoke();
            Dispose();

            _modsList = new List<Mod>();

            if (settings.load[(int)LoadingType.BuiltInAtInit])
                LoadBuiltInMods();
            if (settings.load[(int)LoadingType.ExternalAtInit])
                LoadExternalMods();
        }

        /// <summary>
        /// Load the built in mods
        /// </summary>
        private static void LoadBuiltInMods()
        {
            TextAsset[] archiveData = Resources.LoadAll<TextAsset>(settings.modsDirectory + "/");

            if (archiveData != null)
            {
                foreach (TextAsset item in archiveData)
                {
                    new Mod(item.name, LoadModDone, item.bytes);
                    loadingQueue++;
                }
            }
            else
                Debug.LogWarning("Failed to load any built in archives");
        }

        /// <summary>
        /// Load external mods
        /// </summary>
        private static void LoadExternalMods()
        {
            string[] allFiles = Directory.GetFiles(modsFolder, settings.modsSearchPattern, SearchOption.TopDirectoryOnly);

            if (allFiles.Length > 0)
            {
                foreach (string file in allFiles)
                {
                    new Mod(Path.GetFileNameWithoutExtension(file), LoadModDone, new FileStream(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None));
                    loadingQueue++;
                }
            }
            else
            {
                if (loadingQueue <= 0)
                    LoadModDone(null);
            }
        }

        private static void LoadModDone(Mod m)
        {
            if (m != null)
            {
                if (m.builtIn)
                    m.active = settings.load[(int)LoadingType.BuiltInActivated];
                else
                    m.active = settings.load[(int)LoadingType.ExternalActivated];

                _modsList.Add(m);
                loadingQueue--;
            }

            if (loadingQueue <= 0)
            {
                if (m != null)
                    _modsList.Sort(m);

                if (settings.debugLogging)
                    Debug.Log("All mods should now be loaded");

                modsLoaded = true;
                onModsLoaded.Invoke();
            }
        }

        /// <summary>
        /// Get the contents of a file
        /// </summary>
        /// <param name="path">Full path to the file (within the archives)</param>
        /// <returns>Returns the contents of the file in bytes, null if it doesn't exist</returns>
        public static byte[] GetFileContents(string path)
        {
            Mod m;
            return GetFileContents(path, out m);
        }

        /// <summary>
        /// Get the contents of a file
        /// </summary>
        /// <param name="path">Full path of the file (within the archives)</param>
        /// <param name="mod">The mod that the returned file is in</param>
        /// <returns>Returns the contents of the file in bytes, null if it doesn't exist</returns>
        public static byte[] GetFileContents(string path, out Mod mod)
        {
            mod = null;
            foreach (Mod item in _modsList)
            {
                if (item.active)
                {
                    byte[] file = item.GetFile(path);
                    if (file != null)
                    {
                        mod = item;
                        return file;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get the contents of a file, in UTF8 format
        /// </summary>
        /// <param name="path">Full path of the file</param>
        /// <returns>Returns the contents of the file as string, null if it doesn't exist</returns>
        public static string GetFileContentsString(string path)
        {
            Mod m;
            return GetFileContentsString(path, out m);
        }

        /// <summary>
        /// Get the contents of a file, in UTF8 format
        /// </summary>
        /// <param name="path">Full path of the file</param>
        /// <param name="mod">The mod that the returned file is in</param>
        /// <returns>Returns the contents of the file as string, null if it doesn't exist</returns>
        public static string GetFileContentsString(string path, out Mod mod)
        {
            mod = null;
            foreach (Mod item in _modsList)
            {
                if (item.active)
                {
                    string file = item.GetFileAsString(path);
                    if (file != null)
                    {
                        mod = item;
                        return file;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get all the files from a folder
        /// </summary>
        /// <param name="folder">Full path to the folder</param>
        /// <returns>Returns a dictionary of file paths and the <see cref="Mod"/> the file is in</returns>
        public static Dictionary<Mod, Archives.ArchiveEntry[]> GetFilesInFolder(string folder)
        {
            Dictionary<Mod, List<Archives.ArchiveEntry>> entries = new Dictionary<Mod, List<Archives.ArchiveEntry>>();

            foreach (Mod mod in modsList)
            {
                if (!mod.active)
                    continue;

                List<Archives.ArchiveEntry> modEntries = new List<Archives.ArchiveEntry>(mod.GetFilesInFolder(folder));
                if (modEntries.Count > 0)
                {
                    foreach (var m in entries)
                    {
                        // Removes entries from the modEntries if there are already archive entries with the same name
                        modEntries.RemoveAll(entry => m.Value.Any(n => n.fullName == entry.fullName));
                    }
                    entries.Add(mod, modEntries);
                }
            }
            return entries.ToDictionary(key => key.Key, value => value.Value.ToArray());
        }

        /// <summary>
        /// Activate a mod
        /// </summary>
        /// <param name="mod">Mod to activate</param>
        public static void ActivateMod(Mod mod)
        {
            if (mod.active)
                return;

            mod.active = true;
            onModActivate.Invoke(mod);
        }

        /// <summary>
        /// Deactivate a mod
        /// </summary>
        /// <param name="mod">Mod to deactivate</param>
        public static void DeactivateMod(Mod mod)
        {
            if (!mod.active)
                return;

            mod.active = false;
            onModDeactivate.Invoke(mod);
        }

        /// <summary>
        /// This releases all locked archives
        /// </summary>
        private static void Dispose()
        {
            foreach (var item in _modsList)
                item.Dispose();
        }

        [ExecuteInEditMode]
        private class DisposeOnQuit : MonoBehaviour
        {
            private void Awake()
            {
                if (Application.isEditor && !Application.isPlaying)
                    DestroyImmediate(gameObject);
                else
                    DontDestroyOnLoad(gameObject);
            }

            private void OnApplicationQuit()
            {
                Dispose();
            }

#if FALSE
            private void OnGUI()
            {
                GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label("Mod Enabler Debug, Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                GUILayout.EndArea();
            }
#endif
        }

        /// <summary>
        /// Mod related event type
        /// </summary>
        [System.Serializable]
        public class ModEvent : UnityEvent<Mod> { }
    }
}