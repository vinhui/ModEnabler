from pyassimp import *
from MeshData import MeshData
import sys
import os

optimize = False
calculateNormals = False

def parseArgs():
	global optimize
	global calculateNormals

	for index, arg in enumerate(sys.argv):
		if index == 0:
			continue

		if arg == "-optimize":
			optimize = True
		elif arg == "-calculateNormals":
			calculateNormals = True
		elif os.path.isfile(arg):
			parseFile(arg)
		else:
			print("Argument '" + arg + "' is not valid")
		pass

def parseFile(file):
	print("Loading file '" + file + "'")
	scene = load(file, postprocess.aiProcess_Triangulate)

	for i, mesh in enumerate(scene.meshes):
		print("Parsing mesh '" + mesh.name + "' from file '" + file + "'")
		m = MeshData.Convert(mesh)
		bytes = m.Serialize()
		writeFile(parseName(file, mesh.name), bytes)

def parseName(baseFile, meshName):
	return os.path.splitext(baseFile)[0] + "-" + meshName + ".mesh"

def writeFile(file, bytes):
	if bytes is None:
		print("Not saving '" + file + "', file would be empty")
		return

	print("Saving to '" + file + "'")
	f = open(file, 'wb')
	f.write(bytes)
	f.close()

print("Starting program")

parseArgs()
print("Finished")