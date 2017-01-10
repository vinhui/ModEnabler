using FullSerializer;
using System;

namespace ModEnabler.Serialization
{
    /// <summary>
    /// Json serializer and deserializer. Makes use of https://github.com/jacobdufault/fullserializer
    /// </summary>
    public class JsonSerializer : Serializer
    {
        private readonly fsSerializer _serializer = new fsSerializer();

        /// <summary>
        /// Deserialize a json string to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to deserialize to</typeparam>
        /// <param name="obj">The json object</param>
        /// <returns>Returns the deserialized object</returns>
        public override T Deserialize<T>(string obj)
        {
            return (T)Deserialize(typeof(T), obj);
        }

        /// <summary>
        /// Serialize an object to a json string
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="obj"/>object</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Returns the object in json format</returns>
        public override string Serialize<T>(T obj)
        {
            return Serialize(obj, false);
        }

        /// <summary>
        /// Serialize an object to a json string
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="obj"/>object</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <param name="pretty">Make the output pretty?</param>
        /// <returns>Returns the object in json format</returns>
        public override string Serialize<T>(T obj, bool pretty)
        {
            fsData data = null;
            _serializer.TrySerialize(typeof(T), obj, out data).AssertSuccessWithoutWarnings();

            if (pretty)
                return fsJsonPrinter.PrettyJson(data);
            else
                return fsJsonPrinter.CompressedJson(data);
        }

        /// <summary>
        /// Deserialize a string
        /// </summary>
        /// <param name="t">Type to deserialize to</param>
        /// <param name="obj">String to deserialize</param>
        /// <returns>Returns the object</returns>
        public override object Deserialize(Type t, string obj)
        {
            obj = obj.Trim();
            fsData data = fsJsonParser.Parse(obj);

            object deserialized = null;
            _serializer.TryDeserialize(data, t, ref deserialized).AssertSuccessWithoutWarnings();

            return deserialized;
        }
    }
}