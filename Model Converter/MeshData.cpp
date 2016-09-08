#include "MeshData.h"
#include <assimp/mesh.h>
#include <vector>
#include <iostream>
#include "Utils.h"

using namespace std;

vector<byte> MeshData::Serialize()
{
	vector<byte> fileData;
	vector<byte>::iterator it;

#pragma region Count vars init
	byte nameLength = sizeof(name);
	nameLength = (nameLength > 255 ? 255 : nameLength);
	unsigned short bindPoseCount = sizeof(bindposes);
	unsigned short boneWeightCount = sizeof(boneWeights);
	unsigned short colorCount = sizeof(colors32);
	unsigned short normalCount = sizeof(normals);
	unsigned short tangentCount = sizeof(tangents);
	unsigned int triangleCount = sizeof(triangles);
	unsigned short uvCount = sizeof(uv);
	unsigned short uv2Count = sizeof(uv2);
	unsigned short uv3Count = sizeof(uv3);
	unsigned short uv4Count = sizeof(uv4);
	unsigned short vertexCount = sizeof(vertices);
#pragma endregion

#pragma region Write header
	it = fileData.begin();
	it = fileData.insert(it, *header);
	it = fileData.insert(it, nameLength);

	for (size_t i = 0; i < nameLength; i++)
		it = fileData.insert(it, (byte)name[i]);

	it = fileData.insert(it, *Utils::ushortToBytes(bindPoseCount));
	it = fileData.insert(it, *Utils::ushortToBytes(boneWeightCount));
	it = fileData.insert(it, *Utils::ushortToBytes(colorCount));
	it = fileData.insert(it, *Utils::ushortToBytes(normalCount));
	it = fileData.insert(it, *Utils::ushortToBytes(tangentCount));
	it = fileData.insert(it, *Utils::uintToBytes(triangleCount));
	it = fileData.insert(it, *Utils::ushortToBytes(uvCount));
	it = fileData.insert(it, *Utils::ushortToBytes(uv2Count));
	it = fileData.insert(it, *Utils::ushortToBytes(uv3Count));
	it = fileData.insert(it, *Utils::ushortToBytes(uv4Count));
	it = fileData.insert(it, *Utils::ushortToBytes(vertexCount));
	it = fileData.insert(it, *Utils::boolToByte(optimize));
	it = fileData.insert(it, *Utils::boolToByte(calculateNormals));
#pragma endregion

#pragma region Write data
	for (size_t i = 0; i < bindPoseCount; i++)
		it = fileData.insert(it, *bindposes[i].toBytes());

	for (size_t i = 0; i < boneWeightCount; i++)
		it = fileData.insert(it, *boneWeights[i].toBytes());

	for (size_t i = 0; i < colorCount; i++)
		it = fileData.insert(it, *colors32[i].toBytes());

	for (size_t i = 0; i < normalCount; i++)
		it = fileData.insert(it, *normals[i].toBytes());

	for (size_t i = 0; i < tangentCount; i++)
		it = fileData.insert(it, *tangents[i].toBytes());

	for (size_t i = 0; i < triangleCount; i++)
		it = fileData.insert(it, *Utils::intToBytes(triangles[i]));

	for (size_t i = 0; i < uvCount; i++)
		it = fileData.insert(it, *uv[i].toBytes());

	for (size_t i = 0; i < uv2Count; i++)
		it = fileData.insert(it, *uv2[i].toBytes());

	for (size_t i = 0; i < uv3Count; i++)
		it = fileData.insert(it, *uv3[i].toBytes());

	for (size_t i = 0; i < uv4Count; i++)
		it = fileData.insert(it, *uv4[i].toBytes());

	for (size_t i = 0; i < vertexCount; i++)
		it = fileData.insert(it, *vertices[i].toBytes());
#pragma endregion

	return fileData;
}

MeshData * MeshData::convert(const aiMesh * mesh, bool optimize, bool calculateNormals)
{
	MeshData* meshData = new MeshData();
	meshData->name = mesh->mName.C_Str();

	meshData->colors32 = new Color32[mesh->mNumVertices];
	meshData->normals = new Vector3[mesh->mNumVertices];
	meshData->tangents = new Vector4[mesh->mNumVertices];
	if (mesh->HasTextureCoords(0))
		meshData->uv = new Vector2[mesh->mNumVertices];
	meshData->vertices = new Vector3[mesh->mNumVertices];
	meshData->optimize = optimize;
	meshData->calculateNormals = meshData->normals == NULL || sizeof(meshData->normals) == 0 || calculateNormals;
	return meshData;
}