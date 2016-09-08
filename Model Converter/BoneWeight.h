#pragma once
using byte = unsigned char;

struct BoneWeight {
public :
	const int byteCount = 32;
	int boneIndex0;
	int boneIndex1;
	int boneIndex2;
	int boneIndex3;
	float weight0;
	float weight1;
	float weight2;
	float weight3;
	byte* toBytes();
};