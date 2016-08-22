using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(MeshFilter))]
    internal class LoadMeshResource : LoadResourceComponent<MeshFilter>
    {
        protected override void Set()
        {
            var m = ResourceManager.LoadMesh(fileName);
            if (m != null)
                (componentToSet as MeshFilter).mesh = m;
        }
    }
}