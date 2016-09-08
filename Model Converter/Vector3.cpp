#include "stdafx.h"
#include "Vector3.h"
#include "Utils.h"

byte * Vector3::toBytes()
{
	byte* bytes = new byte[byteCount];
	Utils::copyArr(Utils::floatToBytes(x), bytes, 0);
	Utils::copyArr(Utils::floatToBytes(y), bytes, 4);
	Utils::copyArr(Utils::floatToBytes(z), bytes, 8);
	return bytes;
}
