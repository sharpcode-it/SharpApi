using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharpApi.Helpers.ObjectExtensions.Serialization
{
    public static partial class Deserialize
    {
        /// <summary>
        /// Given an array of <see cref="byte"/>[], this method returns the specified object T
        /// </summary>
        /// <param name="istance">Array of byte <see cref="byte"/>[]</param>
        /// <returns></returns>
        public static T FromByteArray<T>(this byte[]? istance)
        {
            if (istance == null) return default;
            if (!typeof(T).IsSerializable) throw new SerializationException($"A {typeof(T)} object was not serializable");


            using var memoryStream = new MemoryStream(istance);
            var binaryFormatter = new BinaryFormatter();
            var tempObject = binaryFormatter.Deserialize(memoryStream);

            return tempObject.GetType() == typeof(T) ? (T) tempObject : default;
        }
    }
}
