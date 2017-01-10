using ModEnabler.Resource.DataObjects;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ModEnabler.Editor.Utils
{
    public static class MaterialUtils
    {
        public static MaterialData ExportMaterial(Material mat)
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

        public static List<MaterialData.ShaderProperty> GetColors(SerializedProperty root, Material mat)
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

        public static List<MaterialData.ShaderProperty> GetFloats(SerializedProperty root, Material mat)
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

        public static List<MaterialData.ShaderProperty> GetTextures(SerializedProperty root, Material mat)
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
    }
}