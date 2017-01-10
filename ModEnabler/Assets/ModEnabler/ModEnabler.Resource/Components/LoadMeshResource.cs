using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(MeshFilter))]
    [HelpURL("http://modenabler.greenzonegames.com/wiki/resources.meshes.html")]
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