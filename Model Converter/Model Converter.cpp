// Model Converter.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <fstream>
#include <assimp/cimport.h>
#include <assimp/postprocess.h>
#include <assimp/scene.h>
#include <assimp/mesh.h>
#include "MeshData.h"
#include <vector>
using namespace std;
using byte = unsigned char;

bool calculateNormals = false;
bool optimize = false;

vector<byte> parseMesh(const aiMesh* mesh)
{
	MeshData* meshData = MeshData::convert(mesh, optimize, calculateNormals);

	return meshData->Serialize();
}

void parseFile(string file)
{
	string basename = file.substr(0, file.find_last_of("."));
	const aiScene* scene = aiImportFile(file.c_str(), aiProcess_Triangulate);

	for (size_t i = 0; i < scene->mNumMeshes; i++)
	{
		vector<byte> fileBytes = parseMesh(scene->mMeshes[i]);

		string fullname = basename + "-" + scene->mMeshes[i]->mName.C_Str() + ".mesh";
		cout << "Saving parsed file to " << fullname << endl;
		ofstream fstream;
		fstream.open(fullname, ios::binary | ios::out);
		fstream.write((char*)&fileBytes[0], fileBytes.size());
		fstream.close();

	}
}

bool startsWith(const char *pre, const char *str)
{
	size_t lenpre = strlen(pre),
		lenstr = strlen(str);
	return lenstr < lenpre ? false : strncmp(pre, str, lenpre) == 0;
}

int main(int argc, char* argv[])
{
	cout << "Starting program" << endl;

	for (int i = 0; i < argc; i++)
	{
		if (_stricmp(argv[i], "-calculateNormals") == 0)
		{
			cout << "Enabling 'calculate normals'" << endl;
			calculateNormals = true;
		}
		else if (_stricmp(argv[i], "-optimize") == 0)
		{
			cout << "Enabling 'optimize mesh'" << endl;
			optimize = true;
		}
		else
		{
			cout << "Parsing file " << argv[i] << endl;

			parseFile(argv[i]);
		}
	}

	return 0;
}