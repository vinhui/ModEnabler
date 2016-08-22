using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(MeshRenderer))]
    internal class LoadMaterialResource : LoadResourceComponent<MeshRenderer>
    {
        protected override void Set()
        {
            var m = ResourceManager.LoadMaterial(fileName);
            if (m != null)
                (componentToSet as MeshRenderer).sharedMaterial = m;
        }
    }
}