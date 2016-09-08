#include "stdafx.h"
#include "Matrix4x4.h"
#include "Utils.h"

byte * Matrix4x4::toBytes()
{
	byte* bytes = new byte[byteCount];
	Utils::copyArr(Utils::floatToBytes(m00), bytes, 0);
	Utils::copyArr(Utils::floatToBytes(m01), bytes, 4);
	Utils::copyArr(Utils::floatToBytes(m02), bytes, 8);
	Utils::copyArr(Utils::floatToBytes(m03), bytes, 12);
	Utils::copyArr(Utils::floatToBytes(m10), bytes, 16);
	Utils::copyArr(Utils::floatToBytes(m11), bytes, 20);
	Utils::copyArr(Utils::floatToBytes(m12), bytes, 24);
	Utils::copyArr(Utils::floatToBytes(m13), bytes, 28);
	Utils::copyArr(Utils::floatToBytes(m20), bytes, 32);
	Utils::copyArr(Utils::floatToBytes(m21), bytes, 36);
	Utils::copyArr(Utils::floatToBytes(m22), bytes, 40);
	Utils::copyArr(Utils::floatToBytes(m23), bytes, 44);
	Utils::copyArr(Utils::floatToBytes(m30), bytes, 48);
	Utils::copyArr(Utils::floatToBytes(m31), bytes, 52);
	Utils::copyArr(Utils::floatToBytes(m32), bytes, 56);
	Utils::copyArr(Utils::floatToBytes(m33), bytes, 60);
	return bytes;
}
