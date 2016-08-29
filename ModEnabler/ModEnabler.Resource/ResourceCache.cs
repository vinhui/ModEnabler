using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModEnabler.Resource
{
    /// <summary>
    /// This class is responsible for caching all the resources that are used
    /// </summary>
    internal static class ResourceCache
    {
        private static List<CacheTypeItem> cacheList = new List<CacheTypeItem>();

        /// <summary>
        /// Check if the cache contains a certain type
        /// </summary>
        /// <param name="t">Type to check</param>
        /// <returns>True if it's in the cache, else false</returns>
        private static bool ContainsType(Type t)
        {
            foreach (CacheTypeItem item in ResourceCache.cacheList)
            {
                if (item.type == t)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Add a new item to the cache
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        /// <param name="name">Name of the item</param>
        /// <param name="value">The item itself</param>
        /// <param name="mod">The mod that the item is from</param>
        internal static void Add<T>(string name, T value, Mod mod)
        {
            if (!ResourceCache.ContainsType(typeof(T)))
                ResourceCache.cacheList.Add(new CacheTypeItem(typeof(T)));

            foreach (var cacheItem in ResourceCache.cacheList)
            {
                if (cacheItem.type == typeof(T))
                {
                    cacheItem.entries.Add(new CacheEntry(name, value, mod));
                    return;
                }
            }
        }

        /// <summary>
        /// Get an item from the cache
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        /// <param name="name">Name of the item</param>
        /// <returns>Returns null if it's not in the cache</returns>
        internal static CacheEntry GetEntry<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            foreach (CacheTypeItem cacheTypeItem in ResourceCache.cacheList)
            {
                if (cacheTypeItem.type != typeof(T))
                    continue;

                foreach (CacheEntry cacheEntry in cacheTypeItem.entries)
                {
                    if (cacheEntry.name == name)
                        return cacheEntry;
                }
            }

            return null;
        }

        /// <summary>
        /// Clear the entire cache
        /// </summary>
        public static void Clear()
        {
            if (ModsManager.settings.debugLogging)
                Debug.Log("Clearing resources cache");

            cacheList.Clear();
        }

        /// <summary>
        /// Clear all the items that are in <paramref name="mod"/>
        /// </summary>
        /// <param name="mod">The mode to clear the cache for</param>
        public static void Clear(Mod mod)
        {
            if (mod == null)
                return;

            if (ModsManager.settings.debugLogging)
                Debug.Log("Clearing resource cache for mod " + mod.internalName);

            foreach (CacheTypeItem cacheItem in cacheList)
            {
                for (int i = cacheItem.entries.Count - 1; i >= 0; i--)
                {
                    if (cacheItem.entries[i].mod == mod)
                        cacheItem.entries.Remove(cacheItem.entries[i]);
                }
            }
        }

        private class CacheTypeItem
        {
            internal Type type;
            internal List<CacheEntry> entries;

            internal CacheTypeItem(Type type)
            {
                this.type = type;
                entries = new List<CacheEntry>();
            }
        }
    }

    internal class CacheEntry
    {
        internal string name { get; private set; }

        internal object value { get; private set; }

        internal Mod mod { get; private set; }

        /// <summary>
        /// You should never have to create this manually
        /// </summary>
        internal CacheEntry(string name, object value, Mod origin)
        {
            this.name = name;
            this.value = value;
            mod = origin;
        }

        /// <summary>
        /// Get the value of the entry
        /// </summary>
        /// <typeparam name="T">Type to cast it to</typeparam>
        /// <returns>Returns the value in type <typeparamref name="T"/></returns>
        internal T GetValue<T>()
        {
            if (value != null)
            {
                if (value is T)
                    return (T)value;
                else
                {
                    try
                    {
                        return (T)Convert.ChangeType(value, typeof(T));
                    }
                    catch (InvalidCastException e)
                    {
                        Debug.LogError("Failed to cast object to " + typeof(T));
                        Debug.LogError(e.Message);
                    }
                }
            }

            return default(T);
        }
    }
}