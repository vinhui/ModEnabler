using UnityEngine;

namespace ModEnabler.Resource.Components
{
    public class LoadParticleSystemResource : LoadResourceComponent<ParticleSystem>
    {
        public override void Set()
        {
            ResourceManager.LoadParticleSystem(fileName, gameObject);
        }
    }
}