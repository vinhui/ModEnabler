using SharpCompress.Compressor;
using SharpCompress.Compressor.Deflate;
using System;
using System.IO;

namespace ModEnabler.Archives
{
    internal static class Utils
    {
        public static byte[] Gzip(byte[] input, CompressionMode mode)
        {
            using (MemoryStream inputStream = new MemoryStream(input))
            {
                GZipStream a = new GZipStream(inputStream, mode);
                return StreamToByteArray(a);
            }
        }

        public static byte[] StreamToByteArray(Stream stream)
        {
            byte[] buffer = new byte[1024];
            using (MemoryStream outputStream = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, read);
                }
                return outputStream.ToArray();
            }
        }

        public static class Float16
        {
            public static float[] ByteToFloat(byte[] arr)
            {
                float[] floatArr = new float[arr.Length / 2];
                for (int i = 0; i < arr.Length; i += 2)
                {
                    floatArr[i / 2] = FloatDecompress(arr, i, 10000);
                }
                return floatArr;
            }

            public static byte[] FloatToByte(float[] arr)
            {
                byte[] byteArr = new byte[arr.Length * 2];
                for (int i = 0; i < byteArr.Length; i += 2)
                {
                    FloatCompression(arr[i / 2], byteArr, i, 10000);
                }
                return byteArr;
            }

            public static void FloatCompression(float value, byte[] arr, int offset, int precision)
            {
                if (precision > short.MaxValue)
                    throw new OverflowException();

                short v = (short)(value * precision);
                byte[] b = ShortToLittleEndian(v);
                arr[offset] = b[0];
                arr[offset + 1] = b[1];
            }

            public static float FloatDecompress(byte[] bytes, int offset, int precision)
            {
                short a = LittleEndianToShort(bytes, offset);
                return (float)a / precision;
            }
        }

        public static byte[] IntToLittleEndian(int data)
        {
            byte[] b = new byte[4];
            b[0] = (byte)data;
            b[1] = (byte)(((uint)data >> 8) & 0xFF);
            b[2] = (byte)(((uint)data >> 16) & 0xFF);
            b[3] = (byte)(((uint)data >> 24) & 0xFF);
            return b;
        }

        public static byte[] ShortToLittleEndian(short data)
        {
            byte[] b = new byte[4];
            b[0] = (byte)data;
            b[1] = (byte)(((uint)data >> 8) & 0xFF);
            return b;
        }

        public static int LittleEndianToInt(byte[] data, int startIndex)
        {
            return (data[startIndex + 3] << 24)
                 | (data[startIndex + 2] << 16)
                 | (data[startIndex + 1] << 8)
                 | data[startIndex];
        }

        public static short LittleEndianToShort(byte[] data, int startIndex)
        {
            return (short)((data[startIndex + 1] << 8)
                  | data[startIndex]);
        }
    }
}