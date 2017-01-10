namespace ModEnabler.Resource
{
    public static partial class ResourceManager
    {
        /// <summary>
        /// Load a file as plain text
        /// </summary>
        /// <param name="name">Full name and path of the text file</param>
        /// <returns>Returns null if the file doesn't exist or failed to load</returns>
        public static string LoadText(string name)
        {
            return GetResource(name, "", (bytes) =>
            {
                return BytesToString(bytes);
            }, "Text");
        }
    }
}