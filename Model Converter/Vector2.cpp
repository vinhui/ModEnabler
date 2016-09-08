#include "stdafx.h"
#include "Vector2.h"
#include "Utils.h"

byte * Vector2::toBytes()
{
	byte* bytes = new byte[byteCount];
	Utils::copyArr(Utils::floatToBytes(x), bytes, 0);
	Utils::copyArr(Utils::floatToBytes(y), bytes, 4);
	return bytes;
}
