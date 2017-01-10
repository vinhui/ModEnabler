using System;
using UnityEditor;
using UnityEngine;

namespace ModEnabler.Editor.Utils
{
    public static class NormalmapUtils
    {
        public static byte[] ToNormapMap(Texture tex)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(path);
            bool wasReadable = importer.isReadable;
#if UNITY_5_5_OR_NEWER
            bool wasNormalmap = importer.textureType == TextureImporterType.NormalMap;
#else
            bool wasNormalmap = importer.normalmap;
#endif

            if (!wasReadable || wasNormalmap)
            {
#if UNITY_5_5_OR_NEWER
                importer.textureType = TextureImporterType.Default;
#else
                importer.normalmap = false;
#endif
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
#if UNITY_5_5_OR_NEWER
                if (wasNormalmap)
                    importer.textureType = TextureImporterType.NormalMap;
#else
                importer.normalmap = wasNormalmap;
#endif
                importer.SaveAndReimport();
            }

            return allBytes;
        }

        public static void ColorToDXT5(Color32 c, ref byte[] arr, int start)
        {
            if (start + 1 < arr.Length)
            {
                arr[start] = c.r;
                arr[start + 1] = c.g;
            }
        }
    }
}