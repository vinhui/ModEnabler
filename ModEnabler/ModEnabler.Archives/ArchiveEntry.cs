using System.IO;

namespace ModEnabler.Archives
{
    /// <summary>
    /// Am entry of an archive
    /// </summary>
    public struct ArchiveEntry
    {
        private bool _isNull;

        /// <summary>
        /// The name of the entry (without path, including extention)
        /// </summary>
        public string name
        {
            get
            {
                return Path.GetFileName(fullName);
            }
        }

        /// <summary>
        /// Return a null entry
        /// </summary>
        public static ArchiveEntry Null
        {
            get
            {
                ArchiveEntry e = new ArchiveEntry();
                e._isNull = true;
                return e;
            }
        }

        /// <summary>
        /// Is the entry null
        /// </summary>
        public bool isNull
        {
            get
            {
                return _isNull || string.IsNullOrEmpty(fullName) || bytes == null;
            }
        }

        /// <summary>
        /// Full path and name of the entry
        /// </summary>
        public string fullName;

        /// <summary>
        /// The file in byte format
        /// </summary>
        public byte[] bytes;

        /// <summary>
        /// If the file a link to another file
        /// </summary>
        internal Link fileLink;

        /// <summary>
        /// If the file is a link, this field should contain the file it's referring to
        /// </summary>
        internal string fileLinkName;

        internal enum Link : byte
        {
            NoLink = 0,
            HardLink = 1,
            SymLink = 2,
            Reserved = 7
        }

        public override string ToString()
        {
            return "ArchiveEntry (" + fullName + ")";
        }
    }
}