---
uid: resources.meshes.md
title: Meshes
---

# Meshes

[Meshes] are made in a custom format since they can become quite big in file size. You can export meshes from within Unity using the "Mod Enabler" option from the toolbar. You can also use [this](https://github.com/vinhui/ModEnabler-ModelConverter) simple [python](http://python.org/) program to export your meshes, it makes use of [Assimp](http://www.assimp.org/) for loading the models. The format of the file is the following:

| Type                            | Length (in bytes)      | Content           |
|---------------------------------|------------------------|-------------------|
| Bytes                           | 8                      | Header            |
| UShort                          | 2                      | File Version      |
| Bytes                           | 1                      | Name Length       |
| ASCII String                    |                        | Name of the mesh  |
| UShort                          | 2                      | Bind pose count   |
| UShort                          | 2                      | Bone weight count |
| UShort                          | 2                      | Color count       |
| UShort                          | 2                      | Normals count     |
| UShort                          | 2                      | Tangents count    |
| UInt32                          | 4                      | Triangles count   |
| UShort                          | 2                      | UVs count         |
| UShort                          | 2                      | UV2s count        |
| UShort                          | 2                      | UV3s count        |
| UShort                          | 2                      | UV4s count        |
| UShort                          | 2                      | Vertex count      |
| Bool                            | 1                      | Calculate normals |
| Matrix 4x4 (4 floats32)         | 8 bytes per matrix \*  | Bind poses        |
| BoneWeight (4 int32, 4 float32) | 16 bytes per weight \* | Bone weights      |
| Color32 (4 bytes)               | 4 bytes per color \*   | Vertex colors     |
| Vector3 (3 float32)             | 6 bytes per vector \*  | Vertex normals    |
| Vector4 (4 float32)             | 8 bytes per vector \*  | Tangents          |
| UShort                          | 2 bytes per item \*    | Triangles         |
| Vector2 (2 float32)             | 4 bytes per vector \*  | UVs               |
| Vector2 (2 float32)             | 4 bytes per vector \*  | UV2               |
| Vector2 (2 float32)             | 4 bytes per vector \*  | UV3               |
| Vector2 (2 float32)             | 4 bytes per vector \*  | UV4               |
| Vector3 (3 float32)             | 6 bytes per vector \*  | Vertices          |

## Old Version:

| Type                            | Length (in bytes)      | Content           |
|---------------------------------|------------------------|-------------------|
| Bytes                           | 11                     | Header            |
| Bytes                           | 1                      | Name Length       |
| ASCII String                    |                        | Name of the mesh  |
| UShort                          | 2                      | Bind pose count   |
| UShort                          | 2                      | Bone weight count |
| UShort                          | 2                      | Color count       |
| UShort                          | 2                      | Normals count     |
| UShort                          | 2                      | Tangents count    |
| UInt32                          | 4                      | Triangles count   |
| UShort                          | 2                      | UVs count         |
| UShort                          | 2                      | UV2s count        |
| UShort                          | 2                      | UV3s count        |
| UShort                          | 2                      | UV4s count        |
| UShort                          | 2                      | Vertex count      |
| Bool                            | 1                      | Optimize mesh     |
| Bool                            | 1                      | Calculate normals |
| Matrix 4x4 (4 floats32)         | 8 bytes per matrix \*  | Bind poses        |
| BoneWeight (4 int32, 4 float32) | 16 bytes per weight \* | Bone weights      |
| Color32 (4 bytes)               | 4 bytes per color \*   | Vertex colors     |
| Vector3 (3 float32)             | 6 bytes per vector \*  | Vertex normals    |
| Vector4 (4 float32)             | 8 bytes per vector \*  | Tangents          |
| UShort                          | 2 bytes per item \*    | Triangles         |
| Vector2 (2 float32)             | 4 bytes per vector \*  | UVs               |
| Vector2 (2 float32)             | 4 bytes per vector \*  | UV2               |
| Vector2 (2 float32)             | 4 bytes per vector \*  | UV3               |
| Vector2 (2 float32)             | 4 bytes per vector \*  | UV4               |
| Vector3 (3 float32)             | 6 bytes per vector \*  | Vertices          |

_ \* The amount of items is defined with the count above_

The header consists of the following bytes `0x47, 0x5a, 0x47, 0x2d, 0x6d, 0x65, 0x73, 0x68` (or for the old version: `0x76, 0x69, 0x6e, 0x68, 0x75, 0x69, 0x2d, 0x6d, 0x65, 0x73, 0x68`) and is always the same for meshes. All the values are [Little-Endian].

  [Meshes]: https://docs.unity3d.com/ScriptReference/Mesh.html
  [Little-Endian]: https://en.wikipedia.org/wiki/Endianness