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
        public static PhysicMaterial LoadPhysicMaterial(string name)
        {
            return GetResource(name, ModsManager.settings.meshesDirectory, (bytes) =>
            {
                PhysicMaterialData mat = ModsManager.serializer.Deserialize<PhysicMaterialData>(BytesToString(bytes));
                return mat.ToUnity();
            }, "Physic Material");
        }
    }
}