using UnityEngine;

namespace ModEnabler.Resource.Components
{
    [RequireComponent(typeof(MeshRenderer))]
    [HelpURL("http://modenabler.greenzonegames.com/wiki/resources.materials.html")]
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