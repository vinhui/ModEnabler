using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [HelpURL("http://modenabler.greenzonegames.com/wiki/resources.particle-systems.html")]
    public class LoadParticleSystemResource : LoadResourceComponent<ParticleSystem>
    {
        public override void Set()
        {
            ResourceManager.LoadParticleSystem(fileName, gameObject);
        }
    }
}