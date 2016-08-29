---
uid: resources.md
title: Resources
---

# Resources

Importing Resources
-------------------

There are several components to load a resource onto the object the component is on. For example the `LoadMeshResource` component. The following components are available:

-   LoadAudioClipResource
-   LoadImageResource
-   LoadMaterialResource
-   LoadMeshColliderResource
-   LoadMeshResource
-   LoadParticleSystemResource
-   LoadPhysicMaterialResource
-   LoadSpriteResource

### Supported Types

A list of currently supported resource types:

-   [Audio]
-   [Textures, Sprites and Normal Maps]
-   [Materials]
-   [Meshes]
-   [Particle Systems]
-   [Physic Materials]
-   [Text]
-   [Raw bytes]

### Caching

When loading any resource via the ResourceManager, the resource will be cached. This will reduce loading time significantly when trying to get the same resource multiple times. You can clear the cache manually using `ResourceCache.Clear()` but this is most likely not necessary since the entire cache gets cleared when a mod is activated, or when all the mods get reloaded, and some files when a mod is deactivated (only the files that are cached that were in that mod will get cleared).

  [Audio]: xref:resources.audio.md
  [Textures, Sprites and Normal Maps]: xref:resources.textures.md
  [Materials]: xref:resources.materials.md
  [Meshes]: xref:resources.meshes.md
  [Particle Systems]: xref:resources.particle-systems.md
  [Physic Materials]: xref:resources.physic-materials.md
  [Text]: xref:resources.text.md
  [Raw bytes]: xref:resources.raw-bytes.md