using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(MeshFilter))]
    public class LoadMeshResource : LoadResourceComponent<MeshFilter>
    {
        public override void Set()
        {
            var m = ResourceManager.LoadMesh(fileName);
            if (m != null)
                (componentToSet as MeshFilter).mesh = m;
        }
    }
}