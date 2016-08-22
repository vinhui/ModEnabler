using UnityEngine;

namespace ModEnabler.Resource.Components
{
    internal class LoadParticleSystemResource : LoadResourceComponent<ParticleSystem>
    {
        protected override void Set()
        {
            ResourceManager.LoadParticleSystem(fileName, gameObject);
        }
    }
}