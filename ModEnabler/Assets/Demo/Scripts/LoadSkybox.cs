using ModEnabler.Resource;
using ModEnabler.Resource.Components;
using UnityEngine;

/// <summary>
/// Simple script to load a skybox
/// We use the type Transform here because it doesn't need any componen't to function
/// </summary>
public class LoadSkybox : LoadResourceComponent<Transform>
{
    public override void Set()
    {
        RenderSettings.skybox = ResourceManager.LoadMaterial(base.fileName);
    }
}