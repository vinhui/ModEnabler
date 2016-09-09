from array import array
import struct

class MeshData(object):
	header = [0x76, 0x69, 0x6e, 0x68, 0x75, 0x69, 0x2d, 0x6d, 0x65, 0x73, 0x68]

	def __init__(self, *args, **kwargs):
		self.name = "ModdedMesh"
		self.bindposes = []				# Matrix4x4[]
		self.boneWeights = []			# BoneWeight[]
		self.colors32 = []				# Color32[]
		self.normals = []				# Vector3[]
		self.tangents = []				# Vector4[]
		self.triangles = []				# int[]
		self.uv = []					# Vector2[]
		self.uv2 = []					# Vector2[]
		self.uv3 = []					# Vector2[]
		self.uv4 = []					# Vector2[]
		self.vertices = []				# Vector3[]
		self.optimize = True			# bool
		self.calculateNormals = False	# bool

	def Serialize(self):
		asciiName = self.name.encode('ascii')			if self.name is not None else "ModdedMesh".encode('ascii')
		nameLength = len(asciiName)
		bindposeCount = len(self.bindposes)				if self.bindposes is not None else 0
		boneWeightCount = len(self.boneWeights)				if self.boneWeights is not None else 0
		colorCount = len(self.colors32)				if self.colors32 is not None else 0
		normalCount = len(self.normals)					if self.normals is not None else 0
		tangentCount = len(self.tangents)				if self.tangents is not None else 0
		triangleCount = len(self.triangles)				if self.triangles is not None else 0
		uvCount = len(self.uv)						if self.uv is not None else 0
		uv2Count = len(self.uv2)						if self.uv2 is not None else 0
		uv3Count = len(self.uv3)						if self.uv3 is not None else 0
		uv4Count = len(self.uv4)						if self.uv4 is not None else 0
		vertexCount = len(self.vertices)				if self.vertices is not None else 0

		fileBytes = bytes(b'\x76\x69\x6e\x68\x75\x69\x2d\x6d\x65\x73\x68')

		fileBytes += (struct.pack("<B", nameLength))
		fileBytes += (asciiName)
		fileBytes += (struct.pack("<H", bindposeCount))
		fileBytes += (struct.pack("<H", boneWeightCount))
		fileBytes += (struct.pack("<H", colorCount))
		fileBytes += (struct.pack("<H", normalCount))
		fileBytes += (struct.pack("<H", tangentCount))
		fileBytes += (struct.pack("<I", triangleCount))
		fileBytes += (struct.pack("<H", uvCount))
		fileBytes += (struct.pack("<H", uv2Count))
		fileBytes += (struct.pack("<H", uv3Count))
		fileBytes += (struct.pack("<H", uv4Count))
		fileBytes += (struct.pack("<H", vertexCount))
		fileBytes += (struct.pack("<?", self.optimize))
		fileBytes += (struct.pack("<?", self.calculateNormals))

		for x in self.colors32:
			fileBytes += (struct.pack("<B", x.r))
			fileBytes += (struct.pack("<B", x.g))
			fileBytes += (struct.pack("<B", x.b))
			fileBytes += (struct.pack("<B", 0))

		for x in self.normals:
			fileBytes += (struct.pack("<f", x[0]))
			fileBytes += (struct.pack("<f", x[1]))
			fileBytes += (struct.pack("<f", x[2]))

		for x in self.triangles:
			fileBytes += (struct.pack("<H", x))

		for x in self.uv:
			fileBytes += (struct.pack("<f", x[0]))
			fileBytes += (struct.pack("<f", x[1]))

		for x in self.uv2:
			fileBytes += (struct.pack("<f", x[0]))
			fileBytes += (struct.pack("<f", x[1]))

		for x in self.uv3:
			fileBytes += (struct.pack("<f", x[0]))
			fileBytes += (struct.pack("<f", x[1]))

		for x in self.uv4:
			fileBytes += (struct.pack("<f", x[0]))
			fileBytes += (struct.pack("<f", x[1]))

		for x in self.vertices:
			fileBytes += (struct.pack("<f", x[0]))
			fileBytes += (struct.pack("<f", x[1]))
			fileBytes += (struct.pack("<f", x[2]))

		return fileBytes

	@staticmethod
	def Convert(mesh):
		meshData = MeshData()
		meshData.name = mesh.name
		meshData.colors32 = mesh.colors
		meshData.normals = mesh.normals
		meshData.triangles = sum(mesh.faces, [])
		if mesh.texturecoords:
			meshData.uv = mesh.texturecoords[0]
		meshData.vertices = mesh.vertices

		return meshData