using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace ModEnabler.Resource.DataObjects
{
    [Serializable]
    public class MeshData
    {
        private static readonly byte[] headerOld = new byte[] { 0x76, 0x69, 0x6e, 0x68, 0x75, 0x69, 0x2d, 0x6d, 0x65, 0x73, 0x68 };
        private static readonly byte[] header = new byte[] { 0x47, 0x5a, 0x47, 0x2d, 0x6d, 0x65, 0x73, 0x68 };
        private const ushort latestVersion = 2;

        public ushort fileVersion;
        public string name = "ModdedMesh";
        public Matrix4x4[] bindposes;
        public BoneWeight[] boneWeights;
        public Color32[] colors32;
        public Vector3[] normals;
        public Vector4[] tangents;
        public int[] triangles;
        public Vector2[] uv;
        public Vector2[] uv2;
        public Vector2[] uv3;
        public Vector2[] uv4;
        public Vector3[] vertices;
#if !UNITY_5_5_OR_NEWER
        public bool optimize = true;
#endif
        public bool calculateNormals = false;

        /// <summary>
        /// Create a new mesh from a Unity mesh
        /// </summary>
        /// <param name="mesh">Mesh to convert</param>
        public MeshData(Mesh mesh)
        {
            name = mesh.name;
            bindposes = mesh.bindposes;
            boneWeights = mesh.boneWeights;
            colors32 = mesh.colors32;
            normals = mesh.normals;
            tangents = mesh.tangents;
            triangles = mesh.triangles;
            uv = mesh.uv;
            uv2 = mesh.uv2;
            uv3 = mesh.uv3;
            uv4 = mesh.uv4;
            vertices = mesh.vertices;
#if !UNITY_5_5_OR_NEWER
            optimize = true;
#endif

            calculateNormals = normals == null || normals.Length == 0;
        }

        private MeshData()
        {
        }

        public static MeshData Deserialize(byte[] bytes)
        {
            bool headerMatch = false;
            bool oldVersion = false;


            if (Utils.CompareByteArr(bytes, header, 0, header.Length))
                headerMatch = true;
            else if (Utils.CompareByteArr(bytes, headerOld, 0, headerOld.Length))
            {
                headerMatch = true;
                oldVersion = true;
            }

            if (headerMatch)
            {
                MeshData meshData = new MeshData();

                #region Count vars init

                byte nameLength = 0;
                ushort bindPoseCount = 0;
                ushort boneWeightCount = 0;
                ushort colorCount = 0;
                ushort normalCount = 0;
                ushort tangentCount = 0;
                uint triangleCount = 0;
                ushort uvCount = 0;
                ushort uv2Count = 0;
                ushort uv3Count = 0;
                ushort uv4Count = 0;
                ushort vertexCount = 0;

                #endregion Count vars init

                using (MemoryStream memStream = new MemoryStream(bytes))
                {
                    using (BinaryReader reader = new BinaryReader(memStream))
                    {
                        #region Read Header

                        if (oldVersion)
                            reader.BaseStream.Position = headerOld.Length;
                        else
                        {
                            reader.BaseStream.Position = header.Length;
                            meshData.fileVersion = reader.ReadByte();
                        }

                        nameLength = reader.ReadByte();
                        meshData.name = Encoding.ASCII.GetString(reader.ReadBytes(nameLength));
                        bindPoseCount = reader.ReadUShort();
                        boneWeightCount = reader.ReadUShort();
                        colorCount = reader.ReadUShort();
                        normalCount = reader.ReadUShort();
                        tangentCount = reader.ReadUShort();
                        triangleCount = reader.ReadUInt();
                        uvCount = reader.ReadUShort();
                        uv2Count = reader.ReadUShort();
                        uv3Count = reader.ReadUShort();
                        uv4Count = reader.ReadUShort();
                        vertexCount = reader.ReadUShort();
                        if (oldVersion)
#if !UNITY_5_5_OR_NEWER
                            meshData.optimize = reader.ReadBoolean();
#else
                            reader.ReadBoolean();
#endif
                        meshData.calculateNormals = reader.ReadBoolean();

                        #endregion Read Header

                        #region Creating Arrays

                        meshData.bindposes = new Matrix4x4[bindPoseCount];
                        meshData.boneWeights = new BoneWeight[boneWeightCount];
                        meshData.colors32 = new Color32[colorCount];
                        meshData.normals = new Vector3[normalCount];
                        meshData.tangents = new Vector4[tangentCount];
                        meshData.triangles = new int[triangleCount];
                        meshData.uv = new Vector2[uvCount];
                        meshData.uv2 = new Vector2[uv2Count];
                        meshData.uv3 = new Vector2[uv3Count];
                        meshData.uv4 = new Vector2[uv4Count];
                        meshData.vertices = new Vector3[vertexCount];

                        #endregion Creating Arrays

                        #region Filling Arrays

                        for (int i = 0; i < bindPoseCount; i++)
                            meshData.bindposes[i] = reader.ReadMatrix();

                        for (int i = 0; i < boneWeightCount; i++)
                            meshData.boneWeights[i] = reader.ReadBoneWeight();

                        for (int i = 0; i < colorCount; i++)
                            meshData.colors32[i] = reader.ReadColor32();

                        for (int i = 0; i < normalCount; i++)
                            meshData.normals[i] = reader.ReadVector3();

                        for (int i = 0; i < tangentCount; i++)
                            meshData.tangents[i] = reader.ReadVector4();

                        for (int i = 0; i < triangleCount; i++)
                            meshData.triangles[i] = reader.ReadUShort();

                        for (int i = 0; i < uvCount; i++)
                            meshData.uv[i] = reader.ReadVector2();

                        for (int i = 0; i < uv2Count; i++)
                            meshData.uv2[i] = reader.ReadVector2();

                        for (int i = 0; i < uv3Count; i++)
                            meshData.uv3[i] = reader.ReadVector2();

                        for (int i = 0; i < uv4Count; i++)
                            meshData.uv4[i] = reader.ReadVector2();

                        for (int i = 0; i < vertexCount; i++)
                            meshData.vertices[i] = reader.ReadVector3();

                        #endregion Filling Arrays
                    }
                }
                return meshData;
            }
            else
            {
                return ModsManager.serializer.Deserialize<MeshData>(ModsManager.settings.encoding.GetString(bytes));
            }
        }

        public byte[] Serialize()
        {
            #region Count vars init

            byte nameLength = (byte)Mathf.Clamp(name.Length, 0, byte.MaxValue);
            ushort bindPoseCount = (ushort)bindposes.Length;
            ushort boneWeightCount = (ushort)boneWeights.Length;
            ushort colorCount = (ushort)colors32.Length;
            ushort normalCount = (ushort)normals.Length;
            ushort tangentCount = (ushort)tangents.Length;
            uint triangleCount = (uint)triangles.Length;
            ushort uvCount = (ushort)uv.Length;
            ushort uv2Count = (ushort)uv2.Length;
            ushort uv3Count = (ushort)uv3.Length;
            ushort uv4Count = (ushort)uv4.Length;
            ushort vertexCount = (ushort)vertices.Length;

            #endregion Count vars init

            using (MemoryStream memStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(memStream))
                {
                    #region Write Header

#if UNITY_5_5_OR_NEWER
                    writer.Write(header);
                    writer.Write(latestVersion);
#else
                    writer.Write(headerOld);
#endif

                    writer.Write(nameLength);
                    if (name.Length > byte.MaxValue)
                        writer.Write(Encoding.ASCII.GetBytes(name.Substring(0, byte.MaxValue)));
                    else
                        writer.Write(Encoding.ASCII.GetBytes(name));
                    writer.WriteUShort(bindPoseCount);
                    writer.WriteUShort(boneWeightCount);
                    writer.WriteUShort(colorCount);
                    writer.WriteUShort(normalCount);
                    writer.WriteUShort(tangentCount);
                    writer.WriteUInt(triangleCount);
                    writer.WriteUShort(uvCount);
                    writer.WriteUShort(uv2Count);
                    writer.WriteUShort(uv3Count);
                    writer.WriteUShort(uv4Count);
                    writer.WriteUShort(vertexCount);
#if !UNITY_5_5_OR_NEWER
                    writer.Write(optimize);
#endif
                    writer.Write(calculateNormals);

                    #endregion Write Header

                    #region Write Data

                    for (int i = 0; i < bindPoseCount; i++)
                        writer.WriteMatrix(bindposes[i]);

                    for (int i = 0; i < boneWeightCount; i++)
                        writer.WriteBoneWeight(boneWeights[i]);

                    for (int i = 0; i < colorCount; i++)
                        writer.WriteColor32(colors32[i]);

                    for (int i = 0; i < normalCount; i++)
                        writer.WriteVector3(normals[i]);

                    for (int i = 0; i < tangentCount; i++)
                        writer.WriteVector4(tangents[i]);

                    for (int i = 0; i < triangleCount; i++)
                        writer.WriteUShort((ushort)triangles[i]);

                    for (int i = 0; i < uvCount; i++)
                        writer.WriteVector2(uv[i]);

                    for (int i = 0; i < uv2Count; i++)
                        writer.WriteVector2(uv2[i]);

                    for (int i = 0; i < uv3Count; i++)
                        writer.WriteVector2(uv3[i]);

                    for (int i = 0; i < uv4Count; i++)
                        writer.WriteVector2(uv4[i]);

                    for (int i = 0; i < vertexCount; i++)
                        writer.WriteVector3(vertices[i]);

                    #endregion Write Data

                    return memStream.ToArray();
                }
            }
        }

        /// <summary>
        /// Convert the mesh to unity format
        /// </summary>
        /// <returns></returns>
        public Mesh ToUnity()
        {
            Mesh mesh = new Mesh();
            mesh.name = name;
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.uv2 = uv2;
            mesh.uv3 = uv3;
            mesh.uv4 = uv4;
            mesh.triangles = triangles;
            mesh.normals = normals;
            mesh.bindposes = bindposes;
            mesh.boneWeights = boneWeights;
            mesh.colors32 = colors32;
            mesh.tangents = tangents;

            mesh.RecalculateBounds();

            if (calculateNormals)
                mesh.RecalculateNormals();

#if !UNITY_5_5_OR_NEWER
            if (optimize)
                mesh.Optimize();
#endif

            return mesh;
        }
    }
}