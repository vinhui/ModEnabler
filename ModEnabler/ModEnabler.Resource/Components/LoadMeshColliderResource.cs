using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(MeshCollider))]
    public class LoadMeshColliderResource : LoadResourceComponent<MeshCollider>
    {
        public override void Set()
        {
            Mesh mesh = ResourceManager.LoadMesh(fileName);

            // Just make sure the mesh doesn't have too many vertices, otherwise Unity will complain
            if (mesh != null && mesh.vertexCount > 255)
            {
                Debug.LogError("A collider mesh must have less than 256 vertices!");
                return;
            }

            (componentToSet as MeshCollider).sharedMesh = mesh;
        }
    }
}