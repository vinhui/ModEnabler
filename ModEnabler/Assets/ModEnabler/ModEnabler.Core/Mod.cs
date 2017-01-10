using ModEnabler.Archives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ModEnabler
{
    /// <summary>
    /// A mod object
    /// </summary>
    public class Mod : Comparer<Mod>, IDisposable
    {
        /// <summary>
        /// Internal name of the mod, the filename
        /// </summary>
        public string internalName { get; private set; }

        /// <summary>
        /// Properties of the mod
        /// </summary>
        public ModProperties properties { get; private set; }

        /// <summary>
        /// Archive containing all the files of the mod
        /// </summary>
        internal Archive archive { get; private set; }

        /// <summary>
        /// Is this mod built in (inside the Resources folder)
        /// </summary>
        public bool builtIn { get; private set; }

        /// <summary>
        /// Is this mod currently active.
        /// You should use <see cref="ModsManager.ActivateMod(Mod)"/> or <see cref="ModsManager.DeactivateMod(Mod)"/> to enable or disable this mod, otherwise it won't send the events!
        /// </summary>
        public bool active { get; set; }

        private Stream stream;
        internal bool isDoneLoading { get; private set; }
        private Action<Mod> callback;
        private BackgroundWorker backgroundWorker;

        /// <summary>
        /// Create a new mod object
        /// </summary>
        /// <param name="archiveBytes">The archive that makes the mod</param>
        /// <param name="builtIn">Is the mod built in</param>
        internal Mod(string name, Action<Mod> callback, byte[] archiveBytes)
        {
            builtIn = true;
            MemoryStream ms = new MemoryStream(archiveBytes);
            Create(name, callback, ms);
        }

        /// <summary>
        /// Create a new mod object
        /// </summary>
        /// <param name="archiveBytes">The archive that makes the mod</param>
        /// <param name="builtIn">Is the mod built in</param>
        internal Mod(string name, Action<Mod> callback, FileStream fileStream)
        {
            builtIn = false;
            Create(name, callback, fileStream);
        }

        private void Create(string name, Action<Mod> callback, Stream stream)
        {
            this.internalName = name;
            this.stream = stream;
            this.callback = callback;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = false;
            backgroundWorker.WorkerSupportsCancellation = false;
            backgroundWorker.DoWork += (sender, args) => LoadMod();
            backgroundWorker.RunWorkerAsync();
            //LoadMod();

            ModThreadWaiter.Create(this, LoadingDone);
        }

        private void LoadMod()
        {
            try
            {
                Stopwatch s = null;
                if (ModsManager.settings.debugLogging)
                {
                    Debug.Log("Processing mod '" + internalName + "'");
                    s = Stopwatch.StartNew();
                }

                archive = (Archive)Activator.CreateInstance(ModsManager.settings.archiveType, stream);

                if (ModsManager.settings.debugLogging)
                {
                    Debug.Log("Finished loading mod '" + internalName + "', it took " + s.ElapsedMilliseconds + "ms to load and has " + archive.entryCount + " entries");
                    s.Stop();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load mod '" + internalName + "': ");
                Debug.LogException(e);
            }
            finally
            {
                isDoneLoading = true;
            }
        }

        private void LoadingDone()
        {
            if (archive != null)
            {
                while (archive.errorMessages.Count > 0)
                    Debug.Log(archive.errorMessages.Dequeue());
            }

            backgroundWorker = null;

            ArchiveEntry propertiesEntry = archive[ModsManager.settings.modPropertiesFile];
            if (!propertiesEntry.isNull)
                properties = ModsManager.serializer.Deserialize<ModProperties>(ModsManager.settings.encoding.GetString(propertiesEntry.bytes));
            else
                Debug.LogWarning("There's no mod properties file ('" + ModsManager.settings.modPropertiesFile + "')");

            callback(this);
        }

        /// <summary>
        /// Get a file from this mod
        /// </summary>
        /// <param name="path">Full path to the file</param>
        /// <returns>The array will be null if the file doesn't exist</returns>
        public byte[] GetFile(string path)
        {
            if (archive != null)
            {
                ArchiveEntry entry = archive[path];
                if (!entry.isNull)
                    return entry.bytes;
            }
            return null;
        }

        /// <summary>
        /// Get all the entries that are inside a folder
        /// </summary>
        /// <param name="path">Full path to the folder</param>
        /// <returns>Returns a list of entries</returns>
        public IEnumerable<ArchiveEntry> GetFilesInFolder(string path)
        {
            if (archive != null)
                return archive.GetEntriesInFolder(path);

            return new List<ArchiveEntry>();
        }

        /// <summary>
        /// Get the contents of a file as string
        /// </summary>
        /// <param name="path">Full path to the file</param>
        /// <returns>Will return null if the file doesn't exist</returns>
        public string GetFileAsString(string path)
        {
            byte[] bytes = GetFile(path);
            if (bytes != null)
                return ModsManager.settings.encoding.GetString(bytes);

            return null;
        }

        public override string ToString()
        {
            return "Mod (" + properties.DisplayName + ")";
        }

        public override int Compare(Mod x, Mod y)
        {
            if (x == null || y == null)
                return 0;

            if (x.builtIn && !y.builtIn)
                return 1;
            if (!x.builtIn && y.builtIn)
                return -1;

            return -x.internalName.CompareTo(y.internalName);
        }

        public void Dispose()
        {
            if (stream != null)
                stream.Close();

            if (archive != null)
                archive.Dispose();

            backgroundWorker = null;
        }

        /// <summary>
        /// Component to run the callback on the main thread
        /// </summary>
        [ExecuteInEditMode]
        public class ModThreadWaiter : MonoBehaviour
        {
            private Mod mod;
            private Action callback;

            /// <summary>
            /// Create a new instance of the thread waiter
            /// </summary>
            /// <param name="mod">The mod to wait for</param>
            /// <param name="callback">Function to call when it's done loading</param>
            /// <returns>Returns the instance of the created thread waiter</returns>
            public static ModThreadWaiter Create(Mod mod, Action callback)
            {
                GameObject go = new GameObject("Thread Waiter");
                go.hideFlags = HideFlags.HideAndDontSave;
                ModThreadWaiter tw = go.AddComponent<ModThreadWaiter>();
                tw.mod = mod;
                tw.callback = callback;
                return tw;
            }

            private void Update()
            {
                if (mod.isDoneLoading)
                {
                    callback();

                    if (Application.isEditor && !Application.isPlaying)
                        DestroyImmediate(gameObject);
                    else
                        Destroy(gameObject);
                }
            }
        }
    }
}