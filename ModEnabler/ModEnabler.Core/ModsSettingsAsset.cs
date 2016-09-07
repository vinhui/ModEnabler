using ModEnabler.Serialization;
using System;
using System.Text;
using UnityEngine;

namespace ModEnabler
{
    /// <summary>
    /// Settings class for the ModEnabler
    /// </summary>
    [Serializable]
    [HelpURL("http://modenabler.greenzonegames.com/wiki/settings.html")]
    public class ModsSettingsAsset : ScriptableObject
    {
        /// <summary>
        /// Extention of the external mod archives
        /// </summary>
        public string modsSearchPattern = "*.zip";

        /// <summary>
        /// Name of the mod properties file
        /// </summary>
        public string modPropertiesFile = "mod.properties";

        /// <summary>
        /// The name of the directory that contains the mods
        /// </summary>
        public string modsDirectory = "Mods";

        /// <summary>
        /// Name of the directory that contains all the textures
        /// </summary>
        public string texturesDirectory = "Textures";

        /// <summary>
        /// Name of the directory that contains all the materials
        /// </summary>
        public string materialsDirectory = "Materials";

        /// <summary>
        /// Name of the directory that contains all the meshes
        /// </summary>
        public string meshesDirectory = "Meshes";

        /// <summary>
        /// Name of the directory that contains all the audio
        /// </summary>
        public string audioDirectory = "Audio";

        /// <summary>
        /// Name of the directory that contains all the physic materials
        /// </summary>
        public string physicMaterialsDirectory = "PhysicMaterials";

        /// <summary>
        /// Name of the directory that contains all the particle systems
        /// </summary>
        public string particleSystemsDirectory = "ParticleSystems";

        /// <summary>
        /// Name of the directory that contains all the animation clips
        /// </summary>
        public string animationClipsDirectory = "AnimationClips";

        /// <summary>
        /// See <see cref="LoadingType"/>
        /// </summary>
        public bool[] load = new bool[4] { true, true, true, false };

        /// <summary>
        /// Should we show debugging messages
        /// </summary>
        public bool debugLogging = true;

        /// <summary>
        /// Get or set the type of text encoding
        /// </summary>
        public Encoding encoding
        {
            get
            {
                if (encodingCache == null)
                    encodingCache = Encoding.GetEncoding(encodingText);

                return encodingCache;
            }
            set { encodingCache = value; }
        }

        private Encoding encodingCache;
        public string encodingText = "utf-8";

        [SerializeField]
        internal string archiveAssembly = "ModEnabler.Archives, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        [SerializeField]
        internal string archiveTypeString = "ModEnabler.Archives.SevenZipCompatibleArchives";

        private Type archiveTypeCache;

        /// <summary>
        /// The format all the archives should be in
        /// </summary>
        public Type archiveType
        {
            get
            {
                if (archiveTypeCache == null)
                    archiveTypeCache = Type.GetType(archiveTypeString + ", " + archiveAssembly);

                if (archiveTypeCache == null)
                    throw new DataMisalignedException("Failed to get type '" + archiveTypeString + ", " + archiveAssembly + "'");

                return archiveTypeCache;
            }
        }

        [SerializeField]
        internal string serializerAssembly = "ModEnabler.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        [SerializeField]
        internal string serializerTypeString = "ModEnabler.Serialization.JsonSerializer";

        private Serializer serializerCache;

        internal Serializer serializer
        {
            get
            {
                if (serializerCache == null)
                    serializerCache = (Serializer)Activator.CreateInstance(serializerAssembly, serializerTypeString).Unwrap();

                if (serializerCache == null)
                    throw new DataMisalignedException("Failed to get type " + serializerTypeString);

                return serializerCache;
            }
        }

        internal void ClearCache()
        {
            archiveTypeCache = null;
            serializerCache = null;
        }
    }

    /// <summary>
    /// How should mods be loaded
    /// </summary>
    public enum LoadingType : byte
    {
        BuiltInAtInit = 0,
        ExternalAtInit = 1,
        BuiltInActivated = 2,
        ExternalActivated = 3
    }
}