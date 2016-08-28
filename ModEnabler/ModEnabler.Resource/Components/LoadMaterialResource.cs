using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(MeshRenderer))]
    public class LoadMaterialResource : LoadResourceComponent<MeshRenderer>
    {
        public override void Set()
        {
            var m = ResourceManager.LoadMaterial(fileName);
            if (m != null)
                (componentToSet as MeshRenderer).sharedMaterial = m;
        }
    }
}