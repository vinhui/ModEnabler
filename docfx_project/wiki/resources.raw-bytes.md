---
uid: resources.raw-bytes.md
title: Raw Bytes
---

# Raw Bytes

You cannot load raw bytes of files through the `ResourceManager`. If you want to do this, you will have to use `ModsManager.GetFileContents()`. Do note that this won't cache the data, so if you want to use it multiple times, it's best to keep a reference to it.