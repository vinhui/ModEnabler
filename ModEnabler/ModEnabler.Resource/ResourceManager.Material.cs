using UnityEngine;

namespace ModEnabler.Resource
{
    public static partial class ResourceManager
    {
        /// <summary>
        /// Load a material
        /// </summary>
        /// <param name="name">Full name of the material</param>
        /// <returns>Returns null if the file doesn't exist or failed to load</returns>
        public static Material LoadMaterial(string name)
        {
            return GetResource(name, ModsManager.settings.materialsDirectory, (bytes) =>
            {
                MaterialData mat = ModsManager.serializer.Deserialize<MaterialData>(BytesToString(bytes));
                return mat.ToUnity();
            }, "Material");
        }
    }
}