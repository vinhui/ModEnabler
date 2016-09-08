#pragma once

using byte = unsigned char;

struct Matrix4x4 {
public:
	const int byteCount = 64;
	float m00;
	float m01;
	float m02;
	float m03;
	float m10;
	float m11;
	float m12;
	float m13;
	float m20;
	float m21;
	float m22;
	float m23;
	float m30;
	float m31;
	float m32;
	float m33;
	byte* toBytes();
};
