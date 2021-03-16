using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharpApi.Helpers.ObjectExtensions.Serialization
{
    public static partial class Serialize
    {
        /// <summary>
        /// Given an object, this method returns an Array of <see cref="byte"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this object? value)
        {
            if (value is null) return new byte[0];

            using var memoryStream = new MemoryStream();

            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, value);

            return memoryStream.ToArray();
        }
    }
}
