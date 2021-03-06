﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModEnabler.Resource
{
    /// <summary>
    /// This class is responsible for caching all the resources that are used
    /// </summary>
    public static class ResourceCache
    {
        private static List<CacheTypeItem> cacheList = new List<CacheTypeItem>();

        /// <summary>
        /// Check if the cache contains a certain type
        /// </summary>
        /// <param name="t">Type to check</param>
        /// <returns>True if it's in the cache, else false</returns>
        private static bool ContainsType(Type t)
        {
            foreach (CacheTypeItem item in cacheList)
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
        public static void Add<T>(string name, T value, Mod mod)
        {
            if (!ContainsType(typeof(T)))
                cacheList.Add(new CacheTypeItem(typeof(T)));

            foreach (var cacheItem in cacheList)
            {
                if (cacheItem.type == typeof(T))
                {
                    cacheItem.entries.Add(new ResourceCacheEntry(name, value, mod));
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
        public static ResourceCacheEntry GetEntry<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            foreach (CacheTypeItem cacheTypeItem in cacheList)
            {
                if (cacheTypeItem.type != typeof(T))
                    continue;

                foreach (ResourceCacheEntry cacheEntry in cacheTypeItem.entries)
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
        /// <param name="mod">The mod to clear the cache for</param>
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
            internal List<ResourceCacheEntry> entries;

            internal CacheTypeItem(Type type)
            {
                this.type = type;
                entries = new List<ResourceCacheEntry>();
            }
        }
    }
}