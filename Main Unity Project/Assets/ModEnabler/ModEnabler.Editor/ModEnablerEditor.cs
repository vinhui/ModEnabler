using ModEnabler.Editor.Utils;
using ModEnabler.Resource;
using ModEnabler.Resource.DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ModEnabler.Editor
{
    [InitializeOnLoad]
    internal class ModEnablerEditor : EditorWindow
    {
        internal ExportType exportType;
        private List<Object> selectedObjects;
        private Type exportT;

        private static MethodInfo normalmapImporter;

        internal enum ExportType
        {
            Mesh,
            Material,
            ParticleSystem,
            Normalmap,
            PhysicMaterial,
            AnimationClip,
        }

        #region Menu Items

        [MenuItem("Mod Enabler/Import Mesh", priority = 1)]
        internal static void ImportMeshWindow()
        {
            string loadPath = EditorUtility.OpenFilePanel(
                       "Import Mesh",
                       Application.dataPath,
                       "mesh");

            if (!string.IsNullOrEmpty(loadPath))
            {
                MeshData meshData = MeshData.Deserialize(File.ReadAllBytes(loadPath));
                Mesh mesh = meshData.ToUnity();
                AssetDatabase.CreateAsset(mesh, "Assets/" + Path.GetFileNameWithoutExtension(loadPath) + ".asset");

                DisposeModsManager();
            }
        }

        [MenuItem("Mod Enabler/Export Mesh", priority = 2)]
        internal static void ExportMeshWindow()
        {
            ModEnablerEditor window = (ModEnablerEditor)GetWindow(typeof(ModEnablerEditor), true, "Mesh Exporter", true);
            window.exportType = ExportType.Mesh;
            window.Show();
            window.OnEnable();
        }

        [MenuItem("Mod Enabler/Import Material", priority = 21)]
        internal static void ImportMaterial()
        {
            string loadPath = EditorUtility.OpenFilePanel(
                       "Import Material",
                       Application.dataPath,
                       "json");

            if (!string.IsNullOrEmpty(loadPath))
            {
                MaterialData jsonMat = ModsManager.serializer.Deserialize<MaterialData>(File.ReadAllText(loadPath));
                Material mat = jsonMat.ToUnity();
                AssetDatabase.CreateAsset(mat, "Assets/" + Path.GetFileNameWithoutExtension(loadPath) + ".mat");

                DisposeModsManager();
            }
        }

        [MenuItem("Mod Enabler/Export Material", priority = 22)]
        internal static void ExportMaterial()
        {
            ModEnablerEditor window = (ModEnablerEditor)GetWindow(typeof(ModEnablerEditor), true, "Material Exporter", true);
            window.exportType = ExportType.Material;
            window.Show();
            window.OnEnable();
        }

        [MenuItem("Mod Enabler/Import Particle System", priority = 41)]
        internal static void ImportParticleSystem()
        {
            string loadPath = EditorUtility.OpenFilePanel(
                       "Import Particle System",
                       Application.dataPath,
                       "json");

            if (!string.IsNullOrEmpty(loadPath))
            {
                ParticleSystemData jsonParticleSystem = ModsManager.serializer.Deserialize<ParticleSystemData>(File.ReadAllText(loadPath));
                ParticleSystem particleSystem = jsonParticleSystem.ToUnity(new GameObject(Path.GetFileNameWithoutExtension(loadPath)));
                Selection.activeObject = particleSystem;

                DisposeModsManager();
            }
        }

        [MenuItem("Mod Enabler/Export Particle System", priority = 42)]
        internal static void ExportParticleSystem()
        {
            ModEnablerEditor window = (ModEnablerEditor)GetWindow(typeof(ModEnablerEditor), true, "Particle System Exporter", true);
            window.exportType = ExportType.ParticleSystem;
            window.Show();
            window.OnEnable();
        }

        [MenuItem("Mod Enabler/Import Normalmap", priority = 61)]
        internal static void ImportNormalmap()
        {
            string loadPath = EditorUtility.OpenFilePanel(
                       "Import Normalmap",
                       Application.dataPath,
                       "norm");

            if (!string.IsNullOrEmpty(loadPath))
            {
                byte[] bytes = File.ReadAllBytes(loadPath);

                if (normalmapImporter == null)
                    normalmapImporter = typeof(ResourceManager).GetMethod("LoadNormalMap", BindingFlags.NonPublic | BindingFlags.Static);

                Texture2D tex = (Texture2D)normalmapImporter.Invoke(null, new object[] { bytes });
                bytes = tex.EncodeToPNG();
                File.WriteAllBytes(Path.Combine(Application.dataPath, Path.GetFileNameWithoutExtension(loadPath) + ".png"), bytes);
                AssetDatabase.ImportAsset("Assets/" + Path.GetFileNameWithoutExtension(loadPath) + ".png");

                DisposeModsManager();
            }
        }

        [MenuItem("Mod Enabler/Export Normalmap", priority = 62)]
        internal static void ExportNormalmap()
        {
            ModEnablerEditor window = (ModEnablerEditor)GetWindow(typeof(ModEnablerEditor), true, "Normalmap Exporter", true);
            window.exportType = ExportType.Normalmap;
            window.Show();
            window.OnEnable();
        }

        [MenuItem("Mod Enabler/Import Physic Material", priority = 81)]
        internal static void ImportPhysicMaterial()
        {
            string loadPath = EditorUtility.OpenFilePanel(
                       "Import Physic Material",
                       Application.dataPath,
                       "json");

            if (!string.IsNullOrEmpty(loadPath))
            {
                PhysicMaterialData jsonPhysicMaterial = ModsManager.serializer.Deserialize<PhysicMaterialData>(File.ReadAllText(loadPath));
                PhysicMaterial physicMaterial = jsonPhysicMaterial.ToUnity();
                AssetDatabase.CreateAsset(physicMaterial, "Assets/" + Path.GetFileNameWithoutExtension(loadPath) + ".asset");

                DisposeModsManager();
            }
        }

        [MenuItem("Mod Enabler/Export Physic Material", priority = 82)]
        internal static void ExportPhysicMaterial()
        {
            ModEnablerEditor window = (ModEnablerEditor)GetWindow(typeof(ModEnablerEditor), true, "Physic Material Exporter", true);
            window.exportType = ExportType.PhysicMaterial;
            window.Show();
            window.OnEnable();
        }

        [MenuItem("Mod Enabler/Import Animation Clip", priority = 101)]
        internal static void ImportAnimationClip()
        {
            string loadPath = EditorUtility.OpenFilePanel(
                       "Import Animation Clip",
                       Application.dataPath,
                       "json");

            if (!string.IsNullOrEmpty(loadPath))
            {
                AnimationClipData jsonClip = ModsManager.serializer.Deserialize<AnimationClipData>(File.ReadAllText(loadPath));
                AnimationClip animationClip = jsonClip.ToUnity();
                AssetDatabase.CreateAsset(animationClip, "Assets/" + Path.GetFileNameWithoutExtension(loadPath) + ".asset");

                DisposeModsManager();
            }
        }

        [MenuItem("Mod Enabler/Export Animation Clip", priority = 102)]
        internal static void ExportAnimationClip()
        {
            ModEnablerEditor window = (ModEnablerEditor)GetWindow(typeof(ModEnablerEditor), true, "Animation Clip Exporter", true);
            window.exportType = ExportType.AnimationClip;
            window.Show();
            window.OnEnable();
        }

        #endregion Menu Items

        #region UI

        private void OnEnable()
        {
            switch (exportType)
            {
                case ExportType.Mesh:
                    exportT = typeof(Mesh);
                    break;

                case ExportType.Material:
                    exportT = typeof(Material);
                    break;

                case ExportType.ParticleSystem:
                    exportT = typeof(ParticleSystem);
                    break;

                case ExportType.Normalmap:
                    exportT = typeof(Texture);
                    break;

                case ExportType.PhysicMaterial:
                    exportT = typeof(PhysicMaterial);
                    break;

                case ExportType.AnimationClip:
                    exportT = typeof(AnimationClip);
                    break;

                default:
                    throw new NotImplementedException();
            }
            selectedObjects = GetSelection();
        }

        private void OnGUI()
        {
            ShowObjectField(exportT);
            EditorGUILayout.Space();
            if (GUILayout.Button("Export"))
            {
                if (selectedObjects.Count < 1 || (selectedObjects.Count == 1 && selectedObjects[0] == null))
                    return;

                string ext;
                switch (exportType)
                {
                    case ExportType.Normalmap:
                        ext = "norm";
                        break;

                    case ExportType.Mesh:
                        ext = "mesh";
                        break;

                    default:
                        ext = "json";
                        break;
                }

                string savePath = EditorUtility.SaveFolderPanel(
                    "Save " + exportType,
                    Application.dataPath,
                    "");

                if (!string.IsNullOrEmpty(savePath))
                {
                    switch (exportType)
                    {
                        case ExportType.Mesh:
                            foreach (var i in selectedObjects.Where(x => x != null))
                                Write(new MeshData((Mesh)i).Serialize(), Path.Combine(savePath, i.name + "." + ext));
                            break;

                        case ExportType.Material:
                            foreach (var i in selectedObjects.Where(x => x != null))
                                Write(ModsManager.serializer.Serialize(MaterialUtils.ExportMaterial((Material)i)), Path.Combine(savePath, i.name + "." + ext));
                            break;

                        case ExportType.ParticleSystem:
                            foreach (var i in selectedObjects.Where(x => x != null))
                                Write(ModsManager.serializer.Serialize(new ParticleSystemData((ParticleSystem)i)), Path.Combine(savePath, i.name + "." + ext));
                            break;

                        case ExportType.Normalmap:
                            foreach (var i in selectedObjects.Where(x => x != null))
                                Write(NormalmapUtils.ToNormapMap((Texture)i), Path.Combine(Path.GetDirectoryName(savePath), i.name + "." + ext));
                            break;

                        case ExportType.PhysicMaterial:
                            foreach (var i in selectedObjects.Where(x => x != null))
                                Write(ModsManager.serializer.Serialize(new PhysicMaterialData((PhysicMaterial)i)), Path.Combine(savePath, i.name + "." + ext));
                            break;

                        case ExportType.AnimationClip:
                            foreach (var i in selectedObjects.Where(x => x != null))
                                Write(ModsManager.serializer.Serialize(AnimationUtils.ExportAnimationClip((AnimationClip)i)), Path.Combine(savePath, i.name + "." + ext));
                            break;

                        default:
                            throw new NotImplementedException();
                    }
                }

                DisposeModsManager();
            }
        }

        private void ShowObjectField(Type type)
        {
            if (selectedObjects.Count == 0)
                selectedObjects.Add(null);

            for (int i = 0; i < selectedObjects.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                selectedObjects[i] = EditorGUILayout.ObjectField(exportType.ToString() + " " + i, selectedObjects[i], type, true);
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    selectedObjects.RemoveAt(i);
                    if (selectedObjects.Count == 0)
                        selectedObjects.Add(null);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("Add", GUILayout.Width(50)))
                selectedObjects.Add(null);
            if (GUILayout.Button("Add Selection", GUILayout.Width(100)))
                AddUnique(selectedObjects, GetSelection());
            EditorGUILayout.EndHorizontal();
        }

        private void ExportSuccessDialog(string path)
        {
            EditorUtility.DisplayDialog(
                "Exported",
                "Successfully exported to " + path,
                "Ok");
        }

        private List<Object> GetSelection()
        {
            List<Object> objs = Selection.objects.Where(x => x.GetType().IsAssignableFrom(exportT)).ToList();

            if (exportT == typeof(Mesh))
                AddUnique(objs, Selection.gameObjects.Select(x => x.GetComponent<MeshFilter>()).Where(x => x != null).Select(x => x.sharedMesh).Cast<Object>());
            else if (exportT == typeof(ParticleSystem))
                AddUnique(objs, Selection.gameObjects.Select(x => x.GetComponent<ParticleSystem>()).Cast<Object>());
            return objs;
        }

        private static void AddUnique(List<Object> l, IEnumerable<Object> l2)
        {
            foreach (var item in l2)
            {
                if (!l.Contains(item))
                    l.Add(item);
            }
        }

        #endregion UI

        private void Write(string data, string path)
        {
            File.WriteAllText(path, data.Trim(), ModsManager.settings.encoding);

            ExportSuccessDialog(path);
            DisposeModsManager();
        }

        private void Write(byte[] data, string path)
        {
            File.WriteAllBytes(path, data);

            ExportSuccessDialog(path);
        }

        private static void DisposeModsManager()
        {
            typeof(ModsManager).GetMethod("Dispose", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
        }
    }
}