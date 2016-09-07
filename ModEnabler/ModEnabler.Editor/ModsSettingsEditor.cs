using ModEnabler.Archives;
using ModEnabler.Serialization;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ModEnabler
{
    [InitializeOnLoad]
    [CustomEditor(typeof(ModsSettingsAsset))]
    internal class ModsSettingsEditor : Editor
    {
        private static string[] encodingTypes;
        private static int selectedEncodingIndex;
        private static Type[] archiveTypes;
        private static int selectedArchiveIndex;
        private static Type[] serializerTypes;
        private static int selectedSerializerIndex;

        private bool firstRun = true;

        [MenuItem("Mod Enabler/Settings")]
        internal static void ShowSettings()
        {
            CheckFiles();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>("Assets/ModEnabler/Resources/Settings.asset");
        }

        private void OnEnable()
        {
        }

        /// <summary>
        /// Make sure all the files and folders are in the right place, if not, create them
        /// </summary>
        private static void CheckFiles()
        {
            var asset = AssetDatabase.LoadAssetAtPath<ModsSettingsAsset>("Assets/ModEnabler/Resources/Settings.asset");

            if (asset == null)
            {
                if (!AssetDatabase.IsValidFolder("Assets/ModEnabler/Resources"))
                    AssetDatabase.CreateFolder("Assets/ModEnabler", "Resources");

                AssetDatabase.CreateAsset(CreateInstance<ModsSettingsAsset>(), "Assets/ModEnabler/Resources/Settings.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }

        /// <summary>
        /// Get all the types that derive from Archive
        /// </summary>
        /// <param name="target"></param>
        private static void RefreshArchiveTypes(ModsSettingsAsset target)
        {
            Type targetAssetType = target.GetType();

            encodingTypes = Encoding.GetEncodings().Select(x => x.Name).ToArray();
            Array.Sort(encodingTypes);
            selectedEncodingIndex = Array.IndexOf(encodingTypes, target.encodingText);

            archiveTypes = typeof(Archive)
                .Assembly
                .GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Archive)) && !x.IsAbstract)
                //.Union(typeof(Cubem.GameManager)
                //    .Assembly
                //    .GetTypes()
                //    .Where(x => x.IsSubclassOf(typeof(Archive)) && !x.IsAbstract))
                .ToArray();
            serializerTypes = typeof(Serializer)
                .Assembly
                .GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Serializer)) && !x.IsAbstract)
                //.Union(typeof(Cubem.GameManager)
                //    .Assembly
                //    .GetTypes()
                //    .Where(x => x.IsSubclassOf(typeof(Serializer)) && !x.IsAbstract))
                .ToArray();

            selectedArchiveIndex = Array.IndexOf(archiveTypes.Select(x => x.FullName).ToArray(), target.archiveTypeString);
            selectedSerializerIndex = Array.IndexOf(serializerTypes.Select(x => x.FullName).ToArray(), target.serializerTypeString);

            if (selectedArchiveIndex == -1)
                selectedArchiveIndex = 0;
            if (selectedSerializerIndex == -1)
                selectedSerializerIndex = 0;
        }

        public override void OnInspectorGUI()
        {
            ModsSettingsAsset targetAsset = (ModsSettingsAsset)target;

            if (firstRun)
            {
                RefreshArchiveTypes(targetAsset);

                firstRun = false;
            }
            int prevSelectedEncoding = selectedEncodingIndex;
            int prevSelectedArchive = selectedArchiveIndex;
            int prevSelectedSerializer = selectedSerializerIndex;

            EditorGUIUtility.labelWidth = 150;

            EditorGUILayout.HelpBox("Search pattern for external archives.", MessageType.Info);
            targetAsset.modsSearchPattern = EditorGUILayout.TextField("Mods Search Pattern", targetAsset.modsSearchPattern);

            EditorGUILayout.HelpBox("The directory that will contain all the mods relative to the project root folder and in case of the built in mods, relative to the Resources folder.", MessageType.Info);
            targetAsset.modsDirectory = EditorGUILayout.TextField("Mods Directory", targetAsset.modsDirectory);

            EditorGUILayout.HelpBox("These are the directories that are inside the mod archives.", MessageType.Info);
            targetAsset.texturesDirectory = EditorGUILayout.TextField("Textures Directory", targetAsset.texturesDirectory);
            targetAsset.materialsDirectory = EditorGUILayout.TextField("Materials Directory", targetAsset.materialsDirectory);
            targetAsset.meshesDirectory = EditorGUILayout.TextField("Meshes Directory", targetAsset.meshesDirectory);
            targetAsset.audioDirectory = EditorGUILayout.TextField("Audio Directory", targetAsset.audioDirectory);
            targetAsset.particleSystemsDirectory = EditorGUILayout.TextField("Particle Systems Directory", targetAsset.particleSystemsDirectory);
            targetAsset.physicMaterialsDirectory = EditorGUILayout.TextField("Physic Materials Directory", targetAsset.physicMaterialsDirectory);
            targetAsset.animationClipsDirectory = EditorGUILayout.TextField("Animation Clips Directory", targetAsset.animationClipsDirectory);

            EditorGUILayout.HelpBox("These " + targetAsset.load.Length + " options only apply at the initialization.", MessageType.Info);
            for (int i = 0; i < targetAsset.load.Length; i++)
            {
                string label = ((LoadingType)i).ToString();
                switch ((LoadingType)i)
                {
                    case LoadingType.BuiltInAtInit:
                        label = "Load built in";
                        break;

                    case LoadingType.ExternalAtInit:
                        label = "Load external";
                        break;

                    case LoadingType.BuiltInActivated:
                        label = "Load built in activated";
                        break;

                    case LoadingType.ExternalActivated:
                        label = "Load external activated";
                        break;

                    default:
                        break;
                }
                targetAsset.load[i] = EditorGUILayout.Toggle(label, targetAsset.load[i]);
            }

            EditorGUILayout.HelpBox("Do you want to see debugging messages in the Unity Console?", MessageType.Info);
            targetAsset.debugLogging = EditorGUILayout.Toggle("Debug Logging", targetAsset.debugLogging);

            if (encodingTypes != null)
            {
                EditorGUILayout.HelpBox("The way all the text files should be encoded", MessageType.Info);
                selectedEncodingIndex = EditorGUILayout.Popup("Encoding", selectedEncodingIndex, encodingTypes);

                if (prevSelectedEncoding != selectedEncodingIndex)
                {
                    targetAsset.encodingText = encodingTypes[selectedEncodingIndex];
                }
            }

            if (archiveTypes != null)
            {
                EditorGUILayout.HelpBox("The archive format all the mods should be in. To create one, create a new class and inherit from ModEnabler.Archives.Archive", MessageType.Info);
                selectedArchiveIndex = EditorGUILayout.Popup("Archive Format", selectedArchiveIndex, archiveTypes.Select(x => x.FullName).ToArray());

                if (prevSelectedArchive != selectedArchiveIndex)
                {
                    targetAsset.archiveTypeString = archiveTypes[selectedArchiveIndex].FullName;
                    targetAsset.archiveAssembly = archiveTypes[selectedArchiveIndex].Assembly.FullName;
                    targetAsset.ClearCache();
                }
            }
            else
                EditorGUILayout.HelpBox("There are no archive formats! To create one, create a new class and inherit from ModEnabler.Archives.Archive", MessageType.Error);

            if (serializerTypes != null)
            {
                EditorGUILayout.HelpBox("The way to serialize and deserialize all objects. To create a new serializer, create a new class and inherit from ModEnabler.Serialization.Serializer", MessageType.Info);
                selectedSerializerIndex = EditorGUILayout.Popup("Archive Format", selectedSerializerIndex, serializerTypes.Select(x => x.FullName).ToArray());

                if (prevSelectedSerializer != selectedSerializerIndex)
                {
                    targetAsset.serializerTypeString = serializerTypes[selectedSerializerIndex].FullName;
                    targetAsset.serializerAssembly = serializerTypes[selectedSerializerIndex].Assembly.FullName;
                    targetAsset.ClearCache();
                }
            }
            else
                EditorGUILayout.HelpBox("There are no serializer formats! To create one, create a new class and inherit from ModEnabler.Serialization.Serializer", MessageType.Error);

            if (GUILayout.Button("Reload Types"))
                RefreshArchiveTypes(targetAsset);

            EditorUtility.SetDirty(targetAsset);
        }
    }
}