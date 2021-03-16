using Newtonsoft.Json;

namespace SharpApi.Helpers.ObjectExtensions.Serialization
{
    public static partial class Serialize
    {
        private static string ToJson(this object? istance, JsonSerializerSettings settings)
        {
            return istance == null ? string.Empty : JsonConvert.SerializeObject(istance, settings);
        }
    }
}
