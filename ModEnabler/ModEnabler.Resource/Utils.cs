using System;
using System.IO;
using UnityEngine;

namespace ModEnabler.Resource
{
    internal static class Utils
    {
        #region Unity Type Readers

        public static Vector4 ReadVector4(this BinaryReader reader)
        {
            Vector3 v = reader.ReadVector3();
            float w = reader.ReadFloat();
            return new Vector4(v.x, v.y, v.z, w);
        }

        public static Vector3 ReadVector3(this BinaryReader reader)
        {
            Vector2 v = reader.ReadVector2();
            float z = reader.ReadFloat();
            return new Vector3(v.x, v.y, z);
        }

        public static Vector2 ReadVector2(this BinaryReader reader)
        {
            float x = reader.ReadFloat();
            float y = reader.ReadFloat();
            return new Vector2(x, y);
        }

        public static Color ReadColor(this BinaryReader reader)
        {
            return reader.ReadColor32();
        }

        public static Color32 ReadColor32(this BinaryReader reader)
        {
            byte r = reader.ReadByte();
            byte g = reader.ReadByte();
            byte b = reader.ReadByte();
            byte a = reader.ReadByte();
            return new Color32(r, g, b, a);
        }

        public static Matrix4x4 ReadMatrix(this BinaryReader reader)
        {
            Matrix4x4 m = new Matrix4x4();
            m.m00 = reader.ReadFloat();
            m.m01 = reader.ReadFloat();
            m.m02 = reader.ReadFloat();
            m.m03 = reader.ReadFloat();
            m.m10 = reader.ReadFloat();
            m.m11 = reader.ReadFloat();
            m.m12 = reader.ReadFloat();
            m.m13 = reader.ReadFloat();
            m.m20 = reader.ReadFloat();
            m.m21 = reader.ReadFloat();
            m.m22 = reader.ReadFloat();
            m.m23 = reader.ReadFloat();
            m.m30 = reader.ReadFloat();
            m.m31 = reader.ReadFloat();
            m.m32 = reader.ReadFloat();
            m.m33 = reader.ReadFloat();
            return m;
        }

        public static BoneWeight ReadBoneWeight(this BinaryReader reader)
        {
            BoneWeight b = new BoneWeight();
            b.boneIndex0 = reader.ReadInt();
            b.boneIndex1 = reader.ReadInt();
            b.boneIndex2 = reader.ReadInt();
            b.boneIndex3 = reader.ReadInt();
            b.weight0 = reader.ReadFloat();
            b.weight1 = reader.ReadFloat();
            b.weight2 = reader.ReadFloat();
            b.weight3 = reader.ReadFloat();
            return b;
        }

        #endregion Unity Type Readers

        #region Unity Type Writers

        public static void WriteVector4(this BinaryWriter writer, Vector4 vector)
        {
            writer.WriteVector3(vector);
            writer.WriteFloat(vector.w);
        }

        public static void WriteVector3(this BinaryWriter writer, Vector3 vector)
        {
            writer.WriteVector2(vector);
            writer.WriteFloat(vector.z);
        }

        public static void WriteVector2(this BinaryWriter writer, Vector2 vector)
        {
            writer.WriteFloat(vector.x);
            writer.WriteFloat(vector.y);
        }

        public static void WriteColor(this BinaryWriter writer, Color color)
        {
            writer.WriteColor32(color);
        }

        public static void WriteColor32(this BinaryWriter writer, Color32 color)
        {
            writer.Write(color.r);
            writer.Write(color.g);
            writer.Write(color.b);
            writer.Write(color.a);
        }

        public static void WriteMatrix(this BinaryWriter writer, Matrix4x4 matrix)
        {
            writer.WriteFloat(matrix.m00);
            writer.WriteFloat(matrix.m01);
            writer.WriteFloat(matrix.m02);
            writer.WriteFloat(matrix.m03);
            writer.WriteFloat(matrix.m10);
            writer.WriteFloat(matrix.m11);
            writer.WriteFloat(matrix.m12);
            writer.WriteFloat(matrix.m13);
            writer.WriteFloat(matrix.m20);
            writer.WriteFloat(matrix.m21);
            writer.WriteFloat(matrix.m22);
            writer.WriteFloat(matrix.m23);
            writer.WriteFloat(matrix.m30);
            writer.WriteFloat(matrix.m31);
            writer.WriteFloat(matrix.m32);
            writer.WriteFloat(matrix.m33);
        }

        public static void WriteBoneWeight(this BinaryWriter writer, BoneWeight boneWeight)
        {
            writer.WriteInt(boneWeight.boneIndex0);
            writer.WriteInt(boneWeight.boneIndex1);
            writer.WriteInt(boneWeight.boneIndex2);
            writer.WriteInt(boneWeight.boneIndex3);
            writer.WriteFloat(boneWeight.weight0);
            writer.WriteFloat(boneWeight.weight1);
            writer.WriteFloat(boneWeight.weight2);
            writer.WriteFloat(boneWeight.weight3);
        }

        #endregion Unity Type Writers

        #region Default Type Readers

        public static float ReadFloat(this BinaryReader reader)
        {
            return BitConverter.ToSingle(reader.ReadLittleEndian(4), 0);
        }

        public static int ReadInt(this BinaryReader reader)
        {
            return BitConverter.ToInt32(reader.ReadLittleEndian(4), 0);
        }

        public static uint ReadUInt(this BinaryReader reader)
        {
            return BitConverter.ToUInt32(reader.ReadLittleEndian(4), 0);
        }

        public static ushort ReadUShort(this BinaryReader reader)
        {
            return BitConverter.ToUInt16(reader.ReadLittleEndian(2), 0);
        }

        #endregion Default Type Readers

        #region Default Type Writers

        public static void WriteFloat(this BinaryWriter writer, float value)
        {
            writer.WriteLittleEndian(BitConverter.GetBytes(value));
        }

        public static void WriteInt(this BinaryWriter writer, int value)
        {
            writer.WriteLittleEndian(BitConverter.GetBytes(value));
        }

        public static void WriteUInt(this BinaryWriter writer, uint value)
        {
            writer.WriteLittleEndian(BitConverter.GetBytes(value));
        }

        public static void WriteUShort(this BinaryWriter writer, ushort value)
        {
            writer.WriteLittleEndian(BitConverter.GetBytes(value));
        }

        #endregion Default Type Writers



        #region Endianness

        public static byte[] ReadLittleEndian(this BinaryReader reader, int length)
        {
            byte[] arr = reader.ReadBytes(length);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(arr);
            return arr;
        }

        public static void WriteLittleEndian(this BinaryWriter writer, byte[] arr)
        {
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(arr);
            writer.Write(arr);
        }

        #endregion Endianness

        public static bool CompareByteArr(byte[] arr1, byte[] arr2, int start, int length)
        {
            if (arr1.Length >= start + length && arr2.Length >= start + length)
            {
                for (int i = start; i < length; i++)
                {
                    if (arr1[i] != arr2[i])
                        return false;
                }
            }
            else
                return false;

            return true;
        }
    }
}