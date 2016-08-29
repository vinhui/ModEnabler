---
uid: resources.text.md
title: Text
---

# Text

You can load files as text with `ResourceManager.LoadText()`. If you don't want the loaded text to be cached, you should use `ModsManager.GetFileContentsString()` instead. The encoding of the text files should be the same as defined in the [settings], the default is [UTF-8].

When the text is actually an serialized object, you can deserialize it with `ModsManager.serializer.Deserialize<>()`.

  [settings]: xref:settings.md
  [UTF-8]: https://en.wikipedia.org/wiki/UTF-8