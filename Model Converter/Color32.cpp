#include "stdafx.h"
#include "Color32.h"
using byte = unsigned char;

byte * Color32::toBytes()
{
	return new byte[byteCount] { r, g, b, a };
}
