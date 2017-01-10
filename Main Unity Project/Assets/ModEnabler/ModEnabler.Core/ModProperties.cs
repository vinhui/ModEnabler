using System;
using UnityEngine;

namespace ModEnabler
{
    [Serializable]
    public struct ModProperties
    {
        [SerializeField]
        private string displayName;

        /// <summary>
        /// Readable name of the mod
        /// </summary>
        public string DisplayName { get { return displayName; } }

        [SerializeField]
        private string details;

        /// <summary>
        /// Some extra information about the mod
        /// </summary>
        public string Details { get { return details; } }

        [SerializeField]
        private string author;

        /// <summary>
        /// Author of the mod
        /// </summary>
        public string Author { get { return author; } }

        [SerializeField]
        private float version;

        /// <summary>
        /// Version of this mod
        /// </summary>
        public float Version { get { return version; } }
    }
}