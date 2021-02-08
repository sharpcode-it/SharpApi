using System.Text.Json;

namespace SharpApi.Helpers.ObjectExtensions.Serialization
{
    public static partial class Serialize
    {
        /// <summary>
        /// This method serializes the specific object to JSON
        /// </summary>
        /// <param name="istance"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private static string ToJson(this object? istance, JsonSerializerOptions options)
        {
            return istance == null ? string.Empty : JsonSerializer.Serialize(istance, options);
        }
    }
}
