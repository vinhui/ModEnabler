using System;
using UnityEngine;

namespace ModEnabler
{
    [Serializable]
    public struct ModProperties
    {
        [SerializeField]
        private string displayName;

        public string DisplayName { get { return displayName; } }

        [SerializeField]
        private string details;

        public string Details { get { return details; } }

        [SerializeField]
        private string author;

        public string Author { get { return author; } }

        [SerializeField]
        private float version;

        public float Version { get { return version; } }
    }
}