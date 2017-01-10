using System;

namespace ModEnabler.Serialization
{
    /// <summary>
    /// Base class for any serializers
    /// </summary>
    public abstract class Serializer
    {
        /// <summary>
        /// Serialize an object
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="obj"/>object</typeparam>
        /// <param name="obj">The object to serialize</param>
        /// <returns>Returns the object in string format</returns>
        public abstract string Serialize<T>(T obj);

        /// <summary>
        /// Serialize an object
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="obj"/>object</typeparam>
        /// <param name="obj">The object to serialize</param>
        /// <param name="pretty">Do we want the output in a readable and pretty format</param>
        /// <returns>Returns the object in pretty string format</returns>
        public abstract string Serialize<T>(T obj, bool pretty);

        /// <summary>
        /// Deserialize a string
        /// </summary>
        /// <typeparam name="T">Type to deserialize to</typeparam>
        /// <param name="obj">String to deserialize</param>
        /// <returns>Returns the object</returns>
        public abstract T Deserialize<T>(string obj);

        /// <summary>
        /// Deserialize a string
        /// </summary>
        /// <param name="t">Type to deserialize to</param>
        /// <param name="obj">String to deserialize</param>
        /// <returns>Returns the object</returns>
        public abstract object Deserialize(Type t, string obj);
    }
}