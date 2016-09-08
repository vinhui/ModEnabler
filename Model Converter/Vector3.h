#pragma once
using byte = unsigned char;

struct Vector3 {
public:
	const int byteCount = 12;
	float x;
	float y;
	float z;
	byte* toBytes();
};