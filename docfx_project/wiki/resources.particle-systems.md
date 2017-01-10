---
uid: resources.particle-systems.md
title: Particle Systems
---

# Particle Systems

## Sub Emitters

### Unity 5.5 >

If you want to add sub emitters to your particle system, you have to add them manually to the exported file. 

The format for the sub emitters is as following:
``` json
"subEmitters": {
	"enabled": true,
	"emitters": [{
		"name": "birthEmitter.json",
		"type": "Birth",
		"properties": "InheritEverything"
	}, {
		"name": "deathEmitter.json",
		"type": "Death",
		"properties": "InheritEverything"
	}]
}
```
The name should refer to another particle system file.
See https://docs.unity3d.com/ScriptReference/ParticleSystemSubEmitterType.html for all the available types and https://docs.unity3d.com/ScriptReference/ParticleSystemSubEmitterProperties.html for all the available properties.

### Unity 5.4

The amount sub emitters for Unity 5.4 are limited, there are 2 for birth, 2 for collision and 2 for death. The strings refer to other particle system files.