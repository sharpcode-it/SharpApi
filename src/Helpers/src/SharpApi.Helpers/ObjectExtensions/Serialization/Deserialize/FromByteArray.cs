using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharpApi.Helpers.ObjectExtensions.Serialization
{
    public static partial class Deserialize
    {
        /// <summary>
        /// Given a byte array, this method returns the specified object 
        /// </summary>
        /// <param name="istance">Array of byte</param>
        /// <returns></returns>
        public static T FromByteArray<T>(this byte[]? istance)
        {
            if (istance == null) return default;

            using var memoryStream = new MemoryStream(istance);
            var binaryFormatter = new BinaryFormatter();
            var tempObject = binaryFormatter.Deserialize(memoryStream);

            return tempObject.GetType() == typeof(T) ? (T) tempObject : default;
        }
    }
}
