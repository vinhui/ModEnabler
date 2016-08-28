using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(Collider))]
    public class LoadPhysicMaterialResource : LoadResourceComponent<Collider>
    {
        public override void Set()
        {
            var m = ResourceManager.LoadPhysicMaterial(fileName);
            if (m != null)
                (componentToSet as Collider).sharedMaterial = m;
        }
    }
}