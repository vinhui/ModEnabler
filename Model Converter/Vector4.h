#pragma once
using byte = unsigned char;

struct Vector4 {
public:
	const int byteCount = 16;
	float x;
	float y;
	float z;
	float w;
	byte* toBytes();
};