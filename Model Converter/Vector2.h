#pragma once
using byte = unsigned char;

struct Vector2 {
public:
	const int byteCount = 8;
	float x;
	float y;
	byte* toBytes();
};