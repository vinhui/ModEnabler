---
uid: resources.animations.md
title: Animations
---

# Animations

It is possible to load animation clips and animation curves through the `ResourceManager`. You can export animation clips through the Mod Enabler toolbar. You can't export curves since they aren't actually an asset in Unity. The following example creates an ease-in and out curve.

``` json
{
	"postWrapMode" : "Loop",
	"preWrapMode" : "Once",
	"keys" : [{
			"time" : 0,
			"value" : 0,
			"inTangent" : 0,
			"outTangent" : 0
		}, {
			"time" : 1,
			"value" : 1,
			"inTangent" : 0,
			"outTangent" : 0
		}
	]
}
```

You can load an [AnimationClip] with `ResourceManager.LoadAnimationClip()` and an [AnimationCurve] with `ResourceManager.LoadAnimationCurve()`.

  [AnimationClip]: https://docs.unity3d.com/ScriptReference/AnimationClip.html
  [AnimationCurve]: https://docs.unity3d.com/ScriptReference/AnimationCurve.html