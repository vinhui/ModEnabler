using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModEnabler.Archives
{
    /// <summary>
    /// Base class for archive types
    /// </summary>
    public abstract class Archive : IDisposable
    {
        /// <summary>
        /// All entries inside this archive
        /// If you don't want to use this, you will have to override <see cref="GetAllEntries"/>, <see cref="GetEntry(string)"/> and <see cref="GetEntriesInFolder(string)"/>
        /// </summary>
        protected List<ArchiveEntry> _entries;

        /// <summary>
        /// Get the amount of entries in the archive
        /// Override this when not using <see cref="_entries"/>
        /// </summary>
        public virtual int entryCount { get { return _entries != null ? _entries.Count : 0; } }

        /// <summary>
        /// If there are any error messages, they should be in here
        /// </summary>
        public Queue<string> errorMessages { get; protected set; }

        /// <summary>
        /// Load an archive
        /// </summary>
        /// <param name="stream">The archive as a stream</param>
        public Archive(Stream stream)
        {
            if (stream == null)
                errorMessages.Enqueue("Cannot read an empty archive!");

            errorMessages = new Queue<string>();
        }

        /// <summary>
        /// Get all the entries in this archive
        /// </summary>
        /// <returns>Returns a list of all the entries</returns>
        public IEnumerable<ArchiveEntry> GetAllEntries()
        {
            if (_entries != null)
                return _entries;

            return GetEntriesInFolder("");
        }

        /// <summary>
        /// Get an entry from this archive
        /// </summary>
        /// <param name="path">The full path to the file</param>
        /// <returns>Returns ArchiveEntry.Null if it doesn't exist</returns>
        public ArchiveEntry this[string path]
        {
            get
            {
                return GetEntry(path);
            }
        }

        /// <summary>
        /// Get an entry from this archive
        /// </summary>
        /// <param name="path">The full path to the file</param>
        /// <returns>Returns ArchiveEntry.Null if it doesn't exist</returns>
        public virtual ArchiveEntry GetEntry(string path)
        {
            if (_entries != null)
            {
                foreach (ArchiveEntry item in _entries)
                {
                    if (item.fullName == path)
                    {
                        if (item.fileLink == ArchiveEntry.Link.HardLink || item.fileLink == ArchiveEntry.Link.SymLink)
                            return GetEntry(item.fileLinkName);

                        return item;
                    }
                }
            }

            return ArchiveEntry.Null;
        }

        /// <summary>
        /// Get all the files that are inside a folder
        /// </summary>
        /// <param name="path">Full path to the folder</param>
        /// <returns>Returns all the entries that are inside the folder (and sub folders)</returns>
        public virtual IEnumerable<ArchiveEntry> GetEntriesInFolder(string path)
        {
            return _entries.Where(x => x.fullName.StartsWith(path));
        }

        /// <summary>
        /// Dispose of the archive
        /// </summary>
        public abstract void Dispose();
    }
}