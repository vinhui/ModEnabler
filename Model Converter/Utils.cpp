#include "stdafx.h"
#include "Utils.h"
#include <iostream>
using namespace std;

byte * Utils::intToBytes(int i)
{
	byte* bytes = new byte[4];
	memcpy(bytes, &i, 4);
	return bytes;
}

byte * Utils::uintToBytes(unsigned int i)
{
	byte* bytes = new byte[4];
	memcpy(bytes, &i, 4);
	return bytes;
}

byte * Utils::shortToBytes(short i)
{
	byte* bytes = new byte[2];
	memcpy(bytes, &i, 2);
	return bytes;
}

byte * Utils::ushortToBytes(unsigned short i)
{
	byte* bytes = new byte[2];
	memcpy(bytes, &i, 2);
	return bytes;
}

byte * Utils::floatToBytes(float i)
{
	byte* bytes = new byte[4];
	memcpy(bytes, &i, 4);
	return bytes;
}

byte * Utils::boolToByte(bool i)
{
	byte* bytes = new byte[1];
	memcpy(bytes, &i, 4);
	return bytes;
}

void Utils::copyArr(byte* source, byte* dest, int start)
{
	copy(source, source + sizeof(dest), dest + start);
}