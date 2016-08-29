---
uid: resources.audio.md
title: Audio
---

# Audio

The audio clips must be in [Ogg] format (internally [NVorbis] is used). These files might take a bit longer to load since audio files tend to be quite large compared to other assets. Because of this, it might be useful to pre-load them at the start of the game.

You can load an [AudioClip] with `ResourceManager.LoadAudioClip()`

  [Ogg]: https://en.wikipedia.org/wiki/Vorbis
  [NVorbis]: https://github.com/ioctlLR/NVorbis
  [AudioClip]: https://docs.unity3d.com/ScriptReference/AudioClip.html