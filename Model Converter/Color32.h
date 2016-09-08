#pragma once
using byte = unsigned char;

struct Color32 {
public:
	const int byteCount = 4;
	byte r;
	byte g;
	byte b;
	byte a;
	byte* toBytes();
};
