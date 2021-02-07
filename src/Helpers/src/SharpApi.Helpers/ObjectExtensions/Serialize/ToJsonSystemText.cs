using System.Text.Json;

namespace SharpApi.Helpers.ObjectExtensions.Serialize
{
    public static partial class Serialize
    {
        /// <summary>
        /// This method serializes the specific object to JSON
        /// </summary>
        /// <param name="istance"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string ToJson(this object istance, JsonSerializerOptions options)
        {
            return istance == null ? string.Empty : System.Text.Json.JsonSerializer.Serialize(istance, options);
        }

    }
}
