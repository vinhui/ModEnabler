using ModEnabler.Resource.DataObjects;
using System;
using UnityEngine;

namespace ModEnabler.Resource
{
    public static partial class ResourceManager
    {
        /// <summary>
        /// Load the properties for a texture
        /// </summary>
        /// <param name="textureFile">Full name of the texture file</param>
        /// <returns>Returns a TextureProperties object with default values if the properties file doesn't exist</returns>
        private static TextureData LoadTextureProperties(string textureFile)
        {
            TextureData tp = new TextureData();
            string dir = ModsManager.settings.texturesDirectory;
            if (!dir.EndsWith("/"))
                dir += "/";

            string data = ModsManager.GetFileContentsString(dir + textureFile + ".properties");
            if (data != null)
                tp = ModsManager.serializer.Deserialize<TextureData>(data);

            return tp;
        }

        /// <summary>
        /// Load a texture
        /// </summary>
        /// <param name="name">Full name of the texture</param>
        /// <returns>Returns null if the file doesn't exist or failed to load</returns>
        public static Texture2D LoadTexture(string name)
        {
            return LoadTexture(name, true);
        }

        /// <summary>
        /// Load a texture
        /// </summary>
        /// <param name="name">Full name of the texture</param>
        /// <param name="mod">The mod that the file lives in</param>
        /// <returns>Returns null if the file doesn't exist or failed to load</returns>
        private static Texture2D LoadTexture(string name, bool useCache = true)
        {
            return GetResource(name, ModsManager.settings.texturesDirectory, (bytes) =>
            {
                TextureData tp = LoadTextureProperties(name);
                Texture2D tex = null;

                if (tp.normalMap)
                    tex = LoadNormalMap(bytes);
                else
                {
                    tex = new Texture2D(2, 2);
                    tex.LoadImage(bytes);
                }

                if (tex != null)
                {
                    tex.anisoLevel = tp.anisoLevel;
                    tex.filterMode = tp.filterMode;
                    tex.mipMapBias = tp.mipMapBias;
                    tex.wrapMode = tp.wrapMode;
                }

                return tex;
            }, "Texture");
        }

        private static Texture2D LoadNormalMap(byte[] arr)
        {
            byte[] headerText = new byte[] { 0x76, 0x69, 0x6e, 0x68, 0x75, 0x69, 0x2d, 0x6e, 0x6d };

            if (Utils.CompareByteArr(arr, headerText, 0, headerText.Length))
            {
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(arr, headerText.Length, 2);
                    Array.Reverse(arr, headerText.Length + 2, 2);
                }

                int width = BitConverter.ToUInt16(arr, headerText.Length);
                int height = BitConverter.ToUInt16(arr, headerText.Length + 2);

                int headerLength = headerText.Length + 4;

                Color32[] pixels = DXT5ToColor(arr, headerLength);

                Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, true);
                tex.SetPixels32(pixels);
                tex.Apply();
                return tex;
            }
            else
            {
                Debug.Log("Normalmap doesn't have the custom format header. Going to load it as a normal texture but this might not give the expected result.");
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(arr);
                return tex;
            }
        }

        private static Color32[] DXT5ToColor(byte[] arr, int start)
        {
            Color32[] colors = new Color32[(arr.Length - start) / 2];

            for (int i = 0; i < colors.Length; i++)
                colors[i] = new Color32(0, arr[start + (i * 2) + 1], 0, arr[start + (i * 2)]);

            return colors;
        }
    }
}
