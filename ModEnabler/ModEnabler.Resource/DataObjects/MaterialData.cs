using System;
using UnityEngine;

namespace ModEnabler.Resource
{
    /// <summary>
    /// A material that is in json format
    /// </summary>
    [Serializable]
    public struct MaterialData
    {
        public string shader;
        public Color color;
        public string mainTexture;
        public Vector2 mainTextureOffset;
        public Vector2 mainTextureScale;
        public int renderQueue;
        public string[] shaderKeywords;
        public int globalIlluminationFlags;
        public string[] enableKeywords;
        public string[] disableKeywords;
        public ShaderProperty[] shaderProperties;

        public MaterialData(Material mat)
        {
            shader = mat.shader.name;
            if (mat.HasProperty("_Color"))
                color = mat.color;
            else
                color = Color.white;

            if (mat.mainTexture != null)
            {
                mainTexture = mat.mainTexture.name;
                Debug.LogWarning("There is a texture referenced for 'mainTexture' but we can only guess the name for it is '" + mat.mainTexture.name + "', be sure to check it!");
            }
            else
                mainTexture = string.Empty;
            mainTextureOffset = mat.mainTextureOffset;
            mainTextureScale = mat.mainTextureScale;
            renderQueue = mat.renderQueue;
            shaderKeywords = mat.shaderKeywords;
            globalIlluminationFlags = (int)mat.globalIlluminationFlags;

            if (!Application.isEditor)
                Debug.LogWarning("The fields 'enableKeywords', 'disableKeywords' and 'shaderProperties' will be set to empty arrays.");

            enableKeywords = new string[0];
            disableKeywords = new string[0];
            shaderProperties = new ShaderProperty[0];
        }

        /// <summary>
        /// Convert this object to a Unity material
        /// </summary>
        /// <returns>A Unity material</returns>
        public Material ToUnity()
        {
            // Try and find the shader
            Shader s = Shader.Find(shader);

            // Make sure the shader actually exists
            if (s != null)
            {
                Material mat = new Material(s);
                // Set some properties of the material

                if (mat.HasProperty("_Color"))
                    mat.color = color;

                if (!string.IsNullOrEmpty(mainTexture))
                    mat.mainTexture = ResourceManager.LoadTexture(mainTexture);
                if (mat.HasProperty("_MainTex"))
                    mat.mainTextureOffset = mainTextureOffset;
                if (mat.HasProperty("_MainTex"))
                    mat.mainTextureScale = mainTextureScale;
                mat.renderQueue = renderQueue;
                mat.shaderKeywords = shaderKeywords;
                mat.globalIlluminationFlags = (MaterialGlobalIlluminationFlags)globalIlluminationFlags;

                // Handle the enable keywords
                if (enableKeywords != null)
                {
                    foreach (string keyword in enableKeywords)
                    {
                        mat.EnableKeyword(keyword);
                    }
                }
                // Handle the enable keywords
                if (disableKeywords != null)
                {
                    foreach (string keyword in disableKeywords)
                    {
                        mat.DisableKeyword(keyword);
                    }
                }
                // If there are shader properties, handle them
                if (shaderProperties != null)
                {
                    foreach (ShaderProperty property in shaderProperties)
                    {
                        ParseShaderProperty(ref mat, property);
                    }
                }

                return mat;
            }
            else
            {
                Debug.LogError("Shader '" + shader + "' does not exist!");
#if DEBUG
                // This message isn't needed for non debug builds since they probably don't need to know how to do it
                Debug.LogError("Is it included in the 'Always Included Shaders' list in Edit > Project Settings > Graphics?");
#endif
                return null;
            }
        }

        private void ParseShaderProperty(ref Material mat, ShaderProperty property)
        {
            // Make sure the property you want to set is actually a property
            if (!mat.HasProperty(property.name))
            {
                Debug.LogError("There is no property '" + property.name + "' in shader " + shader);
                return;
            }
            try
            {
                // Each type has its own way to set it
                switch (property.type)
                {
                    case "color":
                        mat.SetColor(property.name, property.ValueAsColor());
                        break;

                    case "float":
                        mat.SetFloat(property.name, (float)Convert.ChangeType(property.value, typeof(float)));
                        break;

                    case "int":
                        mat.SetInt(property.name, (int)Convert.ChangeType(property.value, typeof(int)));
                        break;

                    case "matrix":
                        mat.SetMatrix(property.name, property.ValueAsMatrix());
                        break;

                    case "texture":
                        if (!string.IsNullOrEmpty(property.value))
                            mat.SetTexture(property.name, ResourceManager.LoadTexture(property.value));
                        break;

                    case "textureOffset":
                        mat.SetTextureOffset(property.name, property.ValueAsVector2());
                        break;

                    case "textureScale":
                        mat.SetTextureScale(property.name, property.ValueAsVector2());
                        break;

                    case "vector":
                        mat.SetVector(property.name, property.ValueAsVector4());
                        break;

                    default:
                        // If the type is unknown, it may be quite usefull to know
                        Debug.LogError("Unknown type '" + property.type + "' for property '" + property.name + "' in a material");
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to set property '" + property.name + "' to '" + property.value + "'");
                Debug.LogError(e.Message);
            }
        }

        /// <summary>
        /// Simple struct to contain all the info for a shader property
        /// </summary>
        [Serializable]
        public struct ShaderProperty
        {
            public string name;
            public string type;
            public string value;

            /// <summary>
            /// Return the value of this property as a Color
            /// </summary>
            internal Color ValueAsColor()
            {
                string[] values = value.Replace(" ", "").Split(',');

                if (values.Length != 3 && values.Length != 4)
                    ThrowValueFormatError("r,g,b[,a]");

                if (values.Length == 4)
                    return new Color(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]));
                else
                    return new Color(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
            }

            /// <summary>
            /// Return the value of this property as a Matrix4x4
            /// </summary>
            internal Matrix4x4 ValueAsMatrix()
            {
                string[] values = value.Replace(" ", "").Split(';');

                if (values.Length != 3)
                    ThrowValueFormatError("pos:x,y,x; rot:x,y,z,w; scale:x,y,z");

                Vector3 pos = Vector3.zero;
                Quaternion rot = Quaternion.identity;
                Vector3 scale = Vector3.zero;

                for (int i = 0; i < values.Length; i++)
                {
                    string[] subValues = values[i].Split(',');

                    if (values[i].StartsWith("pos"))
                    {
                        if (subValues.Length != 3)
                            ThrowValueFormatError("pos:x,y,x; rot:x,y,z,w; scale:x,y,z");

                        subValues[0] = subValues[0].Replace("pos:", "");
                        pos.x = float.Parse(subValues[0]);
                        pos.y = float.Parse(subValues[1]);
                        pos.z = float.Parse(subValues[2]);
                    }
                    else if (values[i].StartsWith("rot"))
                    {
                        if (subValues.Length != 4)
                            ThrowValueFormatError("pos:x,y,x; rot:x,y,z,w; scale:x,y,z");

                        subValues[0] = subValues[0].Replace("rot:", "");
                        rot.x = float.Parse(subValues[0]);
                        rot.y = float.Parse(subValues[1]);
                        rot.z = float.Parse(subValues[2]);
                        rot.w = float.Parse(subValues[3]);
                    }
                    else if (values[i].StartsWith("scale"))
                    {
                        if (subValues.Length != 3)
                            ThrowValueFormatError("pos:x,y,x; rot:x,y,z,w; scale:x,y,z");

                        subValues[0] = subValues[0].Trim().Replace("scale:", "");
                        scale.x = float.Parse(subValues[0]);
                        scale.y = float.Parse(subValues[1]);
                        scale.z = float.Parse(subValues[2]);
                    }
                }

                return Matrix4x4.TRS(pos, rot, scale);
            }

            /// <summary>
            /// Return the value of this property as a Vector2
            /// </summary>
            internal Vector2 ValueAsVector2()
            {
                string[] values = value.Replace(" ", "").Split(',');

                if (values.Length != 2)
                    ThrowValueFormatError("x,y");

                return new Vector2(float.Parse(values[0]), float.Parse(values[1]));
            }

            /// <summary>
            /// Return the value of this property as a Vector4
            /// </summary>
            internal Vector4 ValueAsVector4()
            {
                string[] values = value.Replace(" ", "").Split(',');

                if (values.Length != 4)
                    ThrowValueFormatError("x,y,z,w");

                return new Vector4(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]));
            }

            private void ThrowValueFormatError(string example)
            {
                throw new FormatException("The value '" + value + "' doesn't match the predefined structure. ('" + example + "')");
            }
        }
    }
}