using ModEnabler.Resource.DataObjects;
using UnityEngine;

namespace ModEnabler.Resource
{
    public static partial class ResourceManager
    {
        /// <summary>
        /// Load a particle system
        /// </summary>
        /// <param name="name">Full name of the particle system</param>
        /// <param name="go">The gameobject to put the particle system on</param>
        /// <returns>Returns null if the file doesn't exist or failed to load</returns>
        public static ParticleSystem LoadParticleSystem(string name, GameObject go)
        {
            return GetResource(name, ModsManager.settings.particleSystemsDirectory, (bytes) =>
            {
                ParticleSystemData psd = ModsManager.serializer.Deserialize<ParticleSystemData>(BytesToString(bytes));
                return psd.ToUnity(go);
            }, "Particle System", false);
        }
    }
}