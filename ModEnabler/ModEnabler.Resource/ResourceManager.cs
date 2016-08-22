using System;
using UnityEngine;

namespace ModEnabler.Resource
{
    public static partial class ResourceManager
    {
        static ResourceManager()
        {
            // Initialize some stuff on first use

            ModsManager.onModsReloaded.AddListener(() =>
            {
                ResourceCache.Clear();
            });
            ModsManager.onModActivate.AddListener((m) =>
            {
                ResourceCache.Clear();
            });
            ModsManager.onModDeactivate.AddListener((m) =>
            {
                ResourceCache.Clear(m);
            });
        }

        private static T GetResource<T>(string name, string dir, Func<byte[], T> parseBytes, string typename, bool useCache = true)
        {
            if (!dir.EndsWith("/") && !string.IsNullOrEmpty(dir))
                dir += "/";

            CacheEntry cacheEntry = (useCache ? ResourceCache.GetEntry<T>(name) : null);

            if (cacheEntry == null)
            {
                Mod mod;
                byte[] fileBytes = ModsManager.GetFileContents(dir + name, out mod);
                if (fileBytes != null && fileBytes.Length > 0)
                {
                    try
                    {
                        T asset = parseBytes(fileBytes);
                        if (asset != null)
                        {
                            if (useCache)
                                ResourceCache.Add(name, asset, mod);
                            return asset;
                        }
                        else
                            Debug.LogError("Failed to convert '" + name + "' to " + typeof(T));
                    }
                    catch (ArgumentException e)
                    {
                        Debug.LogError("Failed to deserialize '" + name + "'");
                        Debug.LogError(e.Message);
                    }
                }
                else
                    Debug.LogError(typename + " '" + dir + name + "' does not exist or is not loaded");
            }
            else
                return cacheEntry.GetValue<T>();

            return default(T);
        }

        private static string BytesToString(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
                return ModsManager.settings.encoding.GetString(bytes);

            return string.Empty;
        }
    }
}