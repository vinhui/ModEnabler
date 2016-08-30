using ModEnabler;
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
        PhysicMaterial
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
                            Write(ModsManager.serializer.Serialize(ExportMaterial((Material)i)), Path.Combine(savePath, i.name + "." + ext));
                        break;

                    case ExportType.ParticleSystem:
                        foreach (var i in selectedObjects.Where(x => x != null))
                            Write(ModsManager.serializer.Serialize(new ParticleSystemData((ParticleSystem)i)), Path.Combine(savePath, i.name + "." + ext));
                        break;

                    case ExportType.Normalmap:
                        foreach (var i in selectedObjects.Where(x => x != null))
                            Write(ToNormapMap((Texture)i), Path.Combine(Path.GetDirectoryName(savePath), i.name + "." + ext));
                        break;

                    case ExportType.PhysicMaterial:
                        foreach (var i in selectedObjects.Where(x => x != null))
                            Write(ModsManager.serializer.Serialize(new PhysicMaterialData((PhysicMaterial)i)), Path.Combine(savePath, i.name + "." + ext));
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

    private void SuccessDialog(string path)
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

        SuccessDialog(path);
        DisposeModsManager();
    }

    private void Write(byte[] data, string path)
    {
        File.WriteAllBytes(path, data);

        SuccessDialog(path);
    }

    #region Material Export

    private MaterialData ExportMaterial(Material mat)
    {
        MaterialData mData = new MaterialData(mat);
        List<MaterialData.ShaderProperty> shaderProperties = new List<MaterialData.ShaderProperty>();

        SerializedObject so = new SerializedObject(mat);
        mData.enableKeywords = so.FindProperty("m_ShaderKeywords").stringValue.Split(' ');

        SerializedProperty root = so.FindProperty("m_SavedProperties");

        shaderProperties.AddRange(GetTextures(root, mat));
        shaderProperties.AddRange(GetFloats(root, mat));
        shaderProperties.AddRange(GetColors(root, mat));

        mData.shaderProperties = shaderProperties.ToArray();
        return mData;
    }

    private List<MaterialData.ShaderProperty> GetColors(SerializedProperty root, Material mat)
    {
        SerializedProperty props = root.FindPropertyRelative("m_Colors");
        List<MaterialData.ShaderProperty> shaderProperties = new List<MaterialData.ShaderProperty>(props.arraySize);

        for (int i = 0; i < props.arraySize; i++)
        {
            MaterialData.ShaderProperty sp = new MaterialData.ShaderProperty();

            SerializedProperty val = props.GetArrayElementAtIndex(i);
            sp.name = val.FindPropertyRelative("first").FindPropertyRelative("name").stringValue;
            if (!mat.HasProperty(sp.name))
                continue;
            sp.type = "color";
            Color c = val.FindPropertyRelative("second").colorValue;
            sp.value = c.r + "," + c.g + "," + c.b + "," + c.a;
            shaderProperties.Add(sp);
        }

        return shaderProperties;
    }

    private List<MaterialData.ShaderProperty> GetFloats(SerializedProperty root, Material mat)
    {
        SerializedProperty props = root.FindPropertyRelative("m_Floats");
        List<MaterialData.ShaderProperty> shaderProperties = new List<MaterialData.ShaderProperty>(props.arraySize);

        for (int i = 0; i < props.arraySize; i++)
        {
            MaterialData.ShaderProperty sp = new MaterialData.ShaderProperty();

            SerializedProperty val = props.GetArrayElementAtIndex(i);
            sp.name = val.FindPropertyRelative("first").FindPropertyRelative("name").stringValue;
            if (!mat.HasProperty(sp.name))
                continue;
            sp.type = "float";
            sp.value = val.FindPropertyRelative("second").floatValue.ToString();
            shaderProperties.Add(sp);
        }

        return shaderProperties;
    }

    private List<MaterialData.ShaderProperty> GetTextures(SerializedProperty root, Material mat)
    {
        SerializedProperty props = root.FindPropertyRelative("m_TexEnvs");
        List<MaterialData.ShaderProperty> shaderProperties = new List<MaterialData.ShaderProperty>(props.arraySize * 3);

        for (int i = 0; i < props.arraySize; i++)
        {
            SerializedProperty val = props.GetArrayElementAtIndex(i);

            MaterialData.ShaderProperty sp = new MaterialData.ShaderProperty();
            Vector2 v;
            sp.name = val.FindPropertyRelative("first").FindPropertyRelative("name").stringValue;
            if (!mat.HasProperty(sp.name))
                continue;
            sp.type = "texture";
            Object o = val.FindPropertyRelative("second").FindPropertyRelative("m_Texture").objectReferenceValue;
            if (o != null)
            {
                sp.value = o.name;
                Debug.LogWarning("There is a texture referenced for '" + sp.name + "' but we can only guess the name for it is '" + o.name + "', be sure to check it!");
            }
            else
                sp.value = string.Empty;
            shaderProperties.Add(sp);

            sp.type = "textureScale";
            v = val.FindPropertyRelative("second").FindPropertyRelative("m_Scale").vector2Value;
            sp.value = v.x + "," + v.y;
            shaderProperties.Add(sp);

            sp.type = "textureOffset";
            v = val.FindPropertyRelative("second").FindPropertyRelative("m_Offset").vector2Value;
            sp.value = v.x + "," + v.y;
            shaderProperties.Add(sp);
        }

        return shaderProperties;
    }

    #endregion Material Export

    #region Normalmap Export

    private byte[] ToNormapMap(Texture tex)
    {
        string path = AssetDatabase.GetAssetPath(tex);
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);
        bool wasReadable = importer.isReadable;
        bool wasNormalmap = importer.normalmap;

        if (!wasReadable || wasNormalmap)
        {
            importer.normalmap = false;
            importer.isReadable = true;
            importer.SaveAndReimport();
        }

        Texture2D tex2D = (tex as Texture2D);

        if (tex2D == null)
            tex2D = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

        int width = tex.width;
        int height = tex.height;

        // Create header
        byte[] headerText = new byte[] { 118, 105, 110, 104, 117, 105, 45, 110, 109 };
        byte[] widthBytes = BitConverter.GetBytes((ushort)width);       // 2 bytes
        byte[] heightBytes = BitConverter.GetBytes((ushort)height);     // 2 bytes

        int headerSize = headerText.Length + widthBytes.Length + heightBytes.Length;

        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(widthBytes);
            Array.Reverse(heightBytes);
        }

        byte[] allBytes = new byte[headerSize + (width * height * 2)];
        Array.Copy(headerText, 0, allBytes, 0, headerText.Length);
        Array.Copy(widthBytes, 0, allBytes, headerText.Length, widthBytes.Length);
        Array.Copy(heightBytes, 0, allBytes, headerText.Length + widthBytes.Length, heightBytes.Length);

        // Convert the colors of the image to bytes, 2 bytes for the 2 colors needed
        Color32[] pixels = tex2D.GetPixels32();
        for (int i = 0; i < pixels.Length; i++)
            ColorToDXT5(pixels[i], ref allBytes, headerSize + (i * 2));

        if (!wasReadable || wasNormalmap)
        {
            importer.isReadable = wasReadable;
            importer.normalmap = wasNormalmap;
            importer.SaveAndReimport();
        }

        return allBytes;
    }

    private void ColorToDXT5(Color32 c, ref byte[] arr, int start)
    {
        if (start + 1 < arr.Length)
        {
            arr[start] = c.r;
            arr[start + 1] = c.g;
        }
    }

    #endregion Normalmap Export

    private static void DisposeModsManager()
    {
        typeof(ModsManager).GetMethod("Dispose", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
    }
}