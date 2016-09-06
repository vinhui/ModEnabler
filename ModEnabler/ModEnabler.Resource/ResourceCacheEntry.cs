using System;

namespace ModEnabler.Resource
{
    public class ResourceCacheEntry
    {
        public string name { get; private set; }

        public object value { get; private set; }

        public Mod mod { get; private set; }

        /// <summary>
        /// You should never have to create this manually
        /// </summary>
        internal ResourceCacheEntry(string name, object value, Mod origin)
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
        public T GetValue<T>()
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