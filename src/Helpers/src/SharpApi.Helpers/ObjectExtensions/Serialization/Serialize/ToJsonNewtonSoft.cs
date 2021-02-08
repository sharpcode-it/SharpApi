using Newtonsoft.Json;

namespace SharpApi.Helpers.ObjectExtensions.Serialization
{
    public static partial class Serialize
    {
        /// <summary>
        /// This method serializes the specific object to JSON
        /// </summary>
        /// <param name="istance"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static string ToJson(this object? istance, JsonSerializerSettings settings)
        {
            return istance == null ? string.Empty : JsonConvert.SerializeObject(istance, settings);
        }
    }
}
