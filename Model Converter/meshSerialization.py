import struct

def serialize(file, mesh, optimize, calculateNormals):
	if mesh.contents.mNumFaces > 65535:
		print("Mesh has more than 65535 vertices. This is not supported, skipping this file")
		return None

	asciiName = mesh.name.encode('ascii')			if mesh.name is not None else "ModdedMesh".encode('ascii')
	nameLength = len(asciiName)
	bindposeCount = 0
	boneWeightCount = 0
	colorCount = mesh.contents.mNumVertices			if mesh.colors else 0
	normalCount = mesh.contents.mNumVertices		if mesh.normals else 0
	tangentCount = mesh.contents.mNumVertices		if mesh.tangents else 0
	triangleCount = mesh.contents.mNumFaces * 3		if mesh.faces else 0
	uvCount = len(mesh.texturecoords[0])			if len(mesh.texturecoords) > 0 else 0
	uv2Count = len(mesh.texturecoords[1])			if len(mesh.texturecoords) > 1 else 0
	uv3Count = len(mesh.texturecoords[2])			if len(mesh.texturecoords) > 2 else 0
	uv4Count = len(mesh.texturecoords[3])			if len(mesh.texturecoords) > 3 else 0
	vertexCount = mesh.contents.mNumVertices		if mesh.vertices else 0

	file.write(b'\x76\x69\x6e\x68\x75\x69\x2d\x6d\x65\x73\x68')

	file.write(struct.pack("<B", nameLength))
	file.write(asciiName)
	file.write(struct.pack("<H", bindposeCount))
	file.write(struct.pack("<H", boneWeightCount))
	file.write(struct.pack("<H", colorCount))
	file.write(struct.pack("<H", normalCount))
	file.write(struct.pack("<H", tangentCount))
	file.write(struct.pack("<I", triangleCount))
	file.write(struct.pack("<H", uvCount))
	file.write(struct.pack("<H", uv2Count))
	file.write(struct.pack("<H", uv3Count))
	file.write(struct.pack("<H", uv4Count))
	file.write(struct.pack("<H", vertexCount))
	file.write(struct.pack("<?", optimize))
	file.write(struct.pack("<?", calculateNormals))

	#if mesh.bindposes is not None:
	#	appendVector(fileBytes, "f", mesh.bindposes, 16)
	#if mesh.boneWeights is not None:
	#	appendVector(fileBytes, "f", mesh.boneWeights, 8)
	appendVector(file, "B", mesh.colors, 4)
	appendVector(file, "f", mesh.normals, 3)
	appendVector(file, "f", mesh.tangents, 4)

	for x in sum(mesh.faces, []):
		file.write(struct.pack("<H", x))

	if mesh.texturecoords is not None:
		for x, i in enumerate(mesh.texturecoords):
			if i > 3:
				break
			else:
				appendVector(file, "f", x, 2)

	appendVector(file, "f", mesh.vertices, 3)

def appendVector(file, type, list, size):
	for x in list:
		for i in range(0, size):
			if x[i] is not None:
				file.write(struct.pack("<" + type, x[i]))
			else:
				file.write(struct.pack("<" + type, 0))
