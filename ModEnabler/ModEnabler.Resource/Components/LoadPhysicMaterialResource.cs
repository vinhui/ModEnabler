using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(Collider))]
    internal class LoadPhysicMaterialResource : LoadResourceComponent<Collider>
    {
        protected override void Set()
        {
            var m = ResourceManager.LoadPhysicMaterial(fileName);
            if (m != null)
                (componentToSet as Collider).sharedMaterial = m;
        }
    }
}