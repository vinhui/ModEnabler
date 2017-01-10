using System;
using UnityEngine;

namespace ModEnabler.Resource.DataObjects
{
    /// <summary>
    /// Properties of a texture
    /// </summary>
    [Serializable]
    internal class TextureData
    {
        [SerializeField]
        internal int anisoLevel;

        [SerializeField]
        internal FilterMode filterMode;

        [SerializeField]
        internal float mipMapBias;

        [SerializeField]
        internal TextureWrapMode wrapMode;

        [SerializeField]
        internal bool normalMap;

        public TextureData()
        {
            anisoLevel = 1;
            filterMode = FilterMode.Bilinear;
            mipMapBias = 0;
            wrapMode = TextureWrapMode.Repeat;
            normalMap = false;
        }
    }
}