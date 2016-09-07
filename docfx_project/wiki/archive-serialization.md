---
uid: archive-serialization.md
title: Archives & Serialization
---

# Archives & Serialization

To parse the mod archives, by default we use [SharpCompress] since it has support for multiple formats. The default [serializer](https://en.wikipedia.org/wiki/Serialization) is FullSerializer. When you want a different archive parser or serializer, you can create one to your liking. You can do this by creating your own class and let it derive from either `ModEnabler.Archives.Archive` or `ModEnabler.Serialization.Serializer`. After that you can select it from the drop-down in the [settings] \(you may have to hit the "Reload Types" button if the settings show an error\).
The following example shows how you can use the Unity [JsonUtility] for serialization.

``` c#
using ModEnabler.Serialization;
using System;
using UnityEngine;

public class UnityJsonSerializer : Serializer
{
    public override object Deserialize(Type t, string obj)
    {
        return JsonUtility.FromJson(obj, t);
    }

    public override T Deserialize<T>(string obj)
    {
        return JsonUtility.FromJson<T>(obj);
    }

    public override string Serialize<T>(T obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public override string Serialize<T>(T obj, bool pretty)
    {
        return JsonUtility.ToJson(obj, pretty);
    }
}
```

  [settings]: xref:settings.md
  [JsonUtility]: https://docs.unity3d.com/Manual/JSONSerialization.html
  [SharpCompress]: https://github.com/adamhathcock/sharpcompress
  [FullSerializer]: https://github.com/jacobdufault/fullserializer