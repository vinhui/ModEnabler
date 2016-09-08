#include "stdafx.h"
#include "BoneWeight.h"
#include <iostream>
#include "Utils.h"
using namespace std;

byte * BoneWeight::toBytes()
{
	byte* bytes = new byte[byteCount];
	Utils::copyArr(Utils::intToBytes(boneIndex0), bytes, 0);
	Utils::copyArr(Utils::intToBytes(boneIndex1), bytes, 4);
	Utils::copyArr(Utils::intToBytes(boneIndex2), bytes, 8);
	Utils::copyArr(Utils::intToBytes(boneIndex3), bytes, 12);
	Utils::copyArr(Utils::floatToBytes(weight0), bytes, 16);
	Utils::copyArr(Utils::floatToBytes(weight1), bytes, 20);
	Utils::copyArr(Utils::floatToBytes(weight2), bytes, 24);
	Utils::copyArr(Utils::floatToBytes(weight3), bytes, 28);
	return bytes;
}
