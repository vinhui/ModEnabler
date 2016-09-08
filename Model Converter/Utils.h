#pragma once
using byte = unsigned char;

class Utils {
public:
	static byte* intToBytes(int i);
	static byte* uintToBytes(unsigned int i);
	static byte* shortToBytes(short i);
	static byte* ushortToBytes(unsigned short i);
	static byte* floatToBytes(float i);
	static byte * boolToByte(bool i);
	static void Utils::copyArr(byte* source, byte* dest, int start);
};
