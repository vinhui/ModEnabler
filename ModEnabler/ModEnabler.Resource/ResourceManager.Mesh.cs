using ModEnabler.Resource.DataObjects;
using UnityEngine;

namespace ModEnabler.Resource
{
    public static partial class ResourceManager
    {
        /// <summary>
        /// Load a mesh
        /// </summary>
        /// <param name="name">Full name of the mesh</param>
        /// <returns>Returns null if the file doesn't exist or failed to load</returns>
        public static Mesh LoadMesh(string name)
        {
            return GetResource(name, ModsManager.settings.meshesDirectory, (bytes) =>
            {
                MeshData meshData = MeshData.Deserialize(bytes);
                return meshData.ToUnity();
            }, "Mesh");
        }
    }
}