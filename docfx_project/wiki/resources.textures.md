---
uid: resources.textures.md
title: Textures
---

# Textures

Since [sprites] are not that different from textures on the importing side, everything below about textures also applies for sprites. Textures must be PNG or JPG. Textures can also have some properties (optional), these properties must be saved in the same directory with the same name as the file but with '.properties' added at the end (e.g.: 'isle\_bk.jpg.properties' for texture 'isle\_bk.jpg'). The properties are as following:

-   anisoLevel (int)
-   filterMode ([FilterMode] as string or int, e.g.: 'Point')
-   mipMapBias (float)
-   wrapMode ([WrapMode] as string or int, e.g.: 'ClampForever')
-   normalMap (bool)

None of these properties are required and will have their default values if not provided. Example of a properties file:

``` json
{
    "anisoLevel" : 1,
    "filterMode" : "Point",
    "mipMapBias" : 0,
    "wrapMode" : 1,
    "normalMap" : false,
}
```

Normal Maps
-----------

Normal maps can be loaded just like textures with `ResourceManager.LoadTexture()`

but they need to have "normalMap" set to true in the properties file. You can have normal maps in 2 formats. One is a custom format optimized for Unity. The other is just an normal image. When you use an normal image, be sure to switch the red channel to alpha and then the red and blue channels can be left empty.

The format of the custom normal maps is as following:

| Type   | Length (in bytes)                                   | Content                                           |
|--------|-----------------------------------------------------|---------------------------------------------------|
| Bytes  | 9                                                   | Header                                            |
| UInt16 | 2                                                   | Width                                             |
| UInt16 | 2                                                   | Height                                            |
| Bytes  | 2 bytes per pixel, Width \* Height amount of pixels | X and Y values of the normal, each their own byte |

The header consists of the following bytes

    0x76, 0x69, 0x6e, 0x68, 0x75, 0x69, 0x2d, 0x6e, 0x6d

and is always the same for normal maps. All the values are [Little-Endian].

  [sprites]: https://docs.unity3d.com/ScriptReference/Sprite.html
  [FilterMode]: http://docs.unity3d.com/ScriptReference/FilterMode.html
  [WrapMode]: http://docs.unity3d.com/ScriptReference/WrapMode.html
  [Little-Endian]: https://en.wikipedia.org/wiki/Endianness