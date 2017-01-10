using System;
using UnityEngine;

namespace ModEnabler.Resource
{
    public static partial class ResourceManager
    {
        /// <summary>
        /// Load a sprite
        /// </summary>
        /// <param name="name">Full name of the sprite</param>
        /// <returns>Returns null if the file doesn't exist or failed to load</returns>
        public static Sprite LoadSprite(string name, Rect rect, Vector2 pivot, float pixelsPerUnit = 100.0f, uint extrude = 0, SpriteMeshType meshType = SpriteMeshType.Tight, Vector4 border = new Vector4())
        {
            return GetResource(name, ModsManager.settings.texturesDirectory, (bytes) =>
            {
                Texture2D tex = LoadTexture(name, false);

                if (tex != null)
                {
                    if (rect.width > tex.width || rect.width < 0)
                    {
                        Debug.LogWarning("The prefered with exceeds the width of the texture! (texture: '" + name + "'; prefered width: " + rect.width + "; texture width: " + tex.width + ")");
                        Debug.LogWarning("Setting the width of the sprite to the texture width!");
                        rect.width = tex.width;
                    }
                    if (rect.height > tex.height || rect.height < 0)
                    {
                        Debug.LogWarning("The prefered height exceeds the height of the texture! (texture: '" + name + "'; prefered height: " + rect.height + "; texture height: " + tex.height + ")");
                        Debug.LogWarning("Setting the height of the sprite to the texture height!");
                        rect.height = tex.height;
                    }

                    try
                    {
                        return Sprite.Create(tex, rect, pivot, pixelsPerUnit, extrude, meshType, border);
                    }
                    catch (ArgumentException e)
                    {
                        Debug.LogError("Failed to create sprite from texture (name: '" + name + "')");
                        Debug.LogError(e.Message);
                    }
                }

                return null;
            }, "Sprite");
        }
    }
}