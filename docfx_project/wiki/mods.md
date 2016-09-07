---
uid: mods.md
title: Mods
---

# Mods

A mod is a single archive. A mod should have a "mod.properties" file (name can be changed in the [settings]. This file should look something like the following:

``` json
{
    "displayName" : "Test Mod",
    "details" : "This is a test mod that contains some random assets",
    "author" : "GreenZone Games",
    "version" : 0.001
}
```

You can activate or deactivate a mod by calling `ModsManager.ActivateMod()` or `ModsManager.DeactivateMod()`. You can get a full list of enabled and disabled mods with `ModsManager.modsList`.

Archives
--------

You can find a list of supported archive formats [here]. You can set the expected extension of the archives in the [settings]. The extension of built-in archives *needs* to be `.bytes`. This is because otherwise Unity won't give access to those files.

Paths
-----

By default the built-in mods should be located in [Resources] folder, e.g. `Assets/Resources/Mods/`. The folder for external mods should be in the next to the [Assets] folder. For example:

    -- Assets
       |-- Resources
       |   |-- Mods
       |   |   |-- default.bytes
    -- Mods
       |-- test.zip
    -- ProjectSettings

The external mods folder will be created on first run. The name for the mods folder can be changed in the [settings].

Loading Order
-------------

The built-in mods (mods in the resources folder) are loaded first, this way their files will have the lowest priority and files in external mods will be used if available. If a mod contains a file that also exists in another mod (with the same path), the file will be taken from the mod with the lowest alphabetical name. So for example, if we have mod "Alpha.zip" looking like:

    -- Textures
       |-- Skybox
       |   |-- isle_bk.jpg
       |   |-- isle_dn.jpg
       |   |-- isle_ft.jpg
       |   |-- isle_lf.jpg
       |   |-- isle_rt.jpg
       |   |-- isle_up.jpg
       |-- UI
       |   |-- panel.png
       |-- bricks.png
    -- mod.properties

And mod "Zulu.zip" looking like this:

    -- Textures
       |-- UI
       |   |-- panel.png
    -- mod.properties

And the requested file being "Textures/UI/panel.png", the file will be taken from mod "Zulu.zip".

  [settings]: xref:settings.md
  [here]: https://github.com/adamhathcock/sharpcompress/wiki/Supported-Formats
  [Resources]: http://docs.unity3d.com/ScriptReference/Resources.html
  [Assets]: http://docs.unity3d.com/Manual/SpecialFolders.html