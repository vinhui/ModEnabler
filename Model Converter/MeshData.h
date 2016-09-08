#pragma once
#include <assimp/mesh.h>
#include "Matrix4x4.h"
#include "BoneWeight.h"
#include "Vector2.h"
#include "Vector3.h"
#include "Vector4.h"
#include "Color32.h"
#include <iostream>
#include <vector>
using byte = unsigned char;

class MeshData {
public:
	const byte* header = new byte[11]{ 0x76, 0x69, 0x6e, 0x68, 0x75, 0x69, 0x2d, 0x6d, 0x65, 0x73, 0x68 };
	const char* name;
	Matrix4x4* bindposes;
	BoneWeight* boneWeights;
	Color32* colors32;
	Vector3* normals;
	Vector4* tangents;
	int* triangles;
	Vector2* uv;
	Vector2* uv1;
	Vector2* uv2;
	Vector2* uv3;
	Vector2* uv4;
	Vector3* vertices;
	bool optimize;
	bool calculateNormals;
	std::vector<byte> Serialize();
	static MeshData* convert(const aiMesh* mesh, bool optimize, bool calculateNormals);
};