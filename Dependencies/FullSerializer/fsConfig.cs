using System;
using System.Reflection;

namespace FullSerializer
{
    /// <summary>
    /// Enables some top-level customization of Full Serializer.
    /// </summary>
    internal static class fsConfig
    {
        /// <summary>
        /// The attributes that will force a field or property to be serialized.
        /// </summary>
        internal static Type[] SerializeAttributes = {
#if !NO_UNITY
            typeof(UnityEngine.SerializeField),
#endif
            typeof(fsPropertyAttribute)
        };

        /// <summary>
        /// The attributes that will force a field or property to *not* be serialized.
        /// </summary>
        internal static Type[] IgnoreSerializeAttributes = { typeof(NonSerializedAttribute), typeof(fsIgnoreAttribute) };

        /// <summary>
        /// The default member serialization.
        /// </summary>
        internal static fsMemberSerialization DefaultMemberSerialization
        {
            get
            {
                return _defaultMemberSerialization;
            }
            set
            {
                _defaultMemberSerialization = value;
                fsMetaType.ClearCache();
            }
        }

        private static fsMemberSerialization _defaultMemberSerialization = fsMemberSerialization.Default;

        /// <summary>
        /// Convert a C# field/property name into the key used for the JSON object. For example, you could
        /// force all JSON names to lowercase with:
        ///
        ///    fsConfig.GetJsonNameFromMemberName = (name, info) => name.ToLower();
        ///
        /// This will only be used when the name is not explicitly specified with fsProperty.
        /// </summary>
        internal static Func<string, MemberInfo, string> GetJsonNameFromMemberName = (name, info) => name;

        /// <summary>
        /// Should the default serialization behaviour include non-auto properties?
        /// </summary>
        internal static bool SerializeNonAutoProperties = false;

        /// <summary>
        /// Should the default serialization behaviour include properties with non-internal setters?
        /// </summary>
        internal static bool SerializeNonPublicSetProperties = true;

        /// <summary>
        /// Should deserialization be case sensitive? If this is false and the JSON has multiple members with the
        /// same keys only separated by case, then this results in undefined behavior.
        /// </summary>
        internal static bool IsCaseSensitive = true;

        /// <summary>
        /// If not null, this string format will be used for DateTime instead of the default one.
        /// </summary>
        internal static string CustomDateTimeFormatString = null;

        /// <summary>
        /// Int64 and UInt64 will be serialized and deserialized as string for compatibility
        /// </summary>
        internal static bool Serialize64BitIntegerAsString = false;

        /// <summary>
        /// Enums are serialized using their names by default. Setting this to true will serialize them as integers instead.
        /// </summary>
        internal static bool SerializeEnumsAsInteger = false;
    }
}