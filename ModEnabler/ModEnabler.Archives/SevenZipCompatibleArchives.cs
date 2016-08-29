using SharpCompress.Archive;
using System.Collections.Generic;
using System.IO;

namespace ModEnabler.Archives
{
    /// <summary>
    /// Archive type that supports what seven zip supports.
    /// It's a wrapper for https://github.com/adamhathcock/sharpcompress
    /// </summary>
    public class SevenZipCompatibleArchives : Archive
    {
        /// <summary>
        /// The file stream
        /// </summary>
        protected Stream stream;

        /// <summary>
        /// Reference to the sharpcompress archive
        /// </summary>
        protected IArchive archive;

        /// <summary>
        /// Amount of entries inside this archive
        /// </summary>
        protected int _entryCount = 0;

        /// <summary>
        /// Get the amount of entries in the archive
        /// </summary>
        public override int entryCount { get { return _entryCount; } }

        /// <summary>
        /// Load an archive
        /// </summary>
        /// <param name="stream">The archive stream</param>
        /// <param name="debug">Should we show debug messages?</param>
        public SevenZipCompatibleArchives(Stream stream)
            : base(stream)
        {
            this.stream = stream;
            archive = ArchiveFactory.Open(stream);

            foreach (var item in archive.Entries)
                _entryCount++;
        }

        /// <summary>
        /// Get all the files that are inside a folder
        /// </summary>
        /// <param name="path">Full path to the folder</param>
        /// <returns>Returns all the archives that are inside the folder</returns>
        public override IEnumerable<ArchiveEntry> GetEntriesInFolder(string path)
        {
            List<ArchiveEntry> entries = new List<ArchiveEntry>();
            foreach (var item in archive.Entries)
            {
                if (item.Key.StartsWith(path))
                {
                    ArchiveEntry e = GetEntry(item);
                    if (!e.isNull)
                        entries.Add(e);
                }
            }

            return entries;
        }

        /// <summary>
        /// Get an entry from this archive
        /// </summary>
        /// <param name="path">The full path to the file</param>
        /// <returns>Returns ArchiveEntry.Null if it doesn't exist</returns>
        public override ArchiveEntry GetEntry(string path)
        {
            foreach (var item in archive.Entries)
            {
                if (item.Key == path)
                {
                    return GetEntry(item);
                }
            }
            return ArchiveEntry.Null;
        }

        /// <summary>
        /// Convert the sharpcompress entry to our <see cref="ArchiveEntry"/>
        /// </summary>
        /// <param name="sharpEntry">SharpCompress entry item</param>
        /// <returns>Returns the converted type</returns>
        protected ArchiveEntry GetEntry(IArchiveEntry sharpEntry)
        {
            if (sharpEntry != null && !sharpEntry.IsDirectory)
            {
                ArchiveEntry archiveEntry = new ArchiveEntry();
                archiveEntry.fullName = sharpEntry.Key;
                using (MemoryStream entryMemStream = new MemoryStream())
                {
                    var a = sharpEntry.OpenEntryStream();
                    archiveEntry.bytes = Utils.StreamToByteArray(a);
                }
                archiveEntry.fileLink = ArchiveEntry.Link.NoLink;
                return archiveEntry;
            }

            return ArchiveEntry.Null;
        }

        /// <summary>
        /// Dispose of the archive
        /// </summary>
        public override void Dispose()
        {
            archive.Dispose();
            stream.Dispose();
        }
    }
}