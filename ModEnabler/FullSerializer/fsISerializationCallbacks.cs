using System;

#if !NO_UNITY

using UnityEngine;

#endif

namespace FullSerializer
{
    /// <summary>
    /// Extend this interface on your type to receive notifications about serialization/deserialization events. If you don't
    /// have access to the type itself, then you can write an fsObjectProcessor instead.
    /// </summary>
    internal interface fsISerializationCallbacks
    {
        /// <summary>
        /// Called before serialization.
        /// </summary>
        void OnBeforeSerialize(Type storageType);

        /// <summary>
        /// Called after serialization.
        /// </summary>
        /// <param name="storageType">The field/property type that is storing the instance.</param>
        /// <param name="data">The data that was serialized.</param>
        void OnAfterSerialize(Type storageType, ref fsData data);

        /// <summary>
        /// Called before deserialization.
        /// </summary>
        /// <param name="storageType">The field/property type that is storing the instance.</param>
        /// <param name="data">The data that will be used for deserialization.</param>
        void OnBeforeDeserialize(Type storageType, ref fsData data);

        /// <summary>
        /// Called after deserialization.
        /// </summary>
        /// <param name="storageType">The field/property type that is storing the instance.</param>
        /// <param name="instance">The type of the instance.</param>
        void OnAfterDeserialize(Type storageType);
    }
}

namespace FullSerializer.Internal
{
    internal class fsSerializationCallbackProcessor : fsObjectProcessor
    {
        internal override bool CanProcess(Type type)
        {
            return typeof(fsISerializationCallbacks).IsAssignableFrom(type);
        }

        internal override void OnBeforeSerialize(Type storageType, object instance)
        {
            ((fsISerializationCallbacks)instance).OnBeforeSerialize(storageType);
        }

        internal override void OnAfterSerialize(Type storageType, object instance, ref fsData data)
        {
            ((fsISerializationCallbacks)instance).OnAfterSerialize(storageType, ref data);
        }

        internal override void OnBeforeDeserializeAfterInstanceCreation(Type storageType, object instance, ref fsData data)
        {
            if (instance is fsISerializationCallbacks == false)
            {
                throw new InvalidCastException("Please ensure the converter for " + storageType + " actually returns an instance of it, not an instance of " + instance.GetType());
            }

            ((fsISerializationCallbacks)instance).OnBeforeDeserialize(storageType, ref data);
        }

        internal override void OnAfterDeserialize(Type storageType, object instance)
        {
            ((fsISerializationCallbacks)instance).OnAfterDeserialize(storageType);
        }
    }

#if !NO_UNITY

    internal class fsSerializationCallbackReceiverProcessor : fsObjectProcessor
    {
        internal override bool CanProcess(Type type)
        {
            return typeof(ISerializationCallbackReceiver).IsAssignableFrom(type);
        }

        internal override void OnBeforeSerialize(Type storageType, object instance)
        {
            ((ISerializationCallbackReceiver)instance).OnBeforeSerialize();
        }

        internal override void OnAfterDeserialize(Type storageType, object instance)
        {
            ((ISerializationCallbackReceiver)instance).OnAfterDeserialize();
        }
    }

#endif
}