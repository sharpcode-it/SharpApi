using Newtonsoft.Json;
using SharpApi.Helpers.Model.Enum;

namespace SharpApi.Helpers.ObjectExtensions.Serialize
{
    public static partial class Serialize
    {
        /// <summary>
        /// This method serializes the specific object to JSON
        /// </summary>
        /// <param name="istance"></param>
        /// <param name="serializeApiType"></param>
        /// <returns></returns>
        public static string ToJson(this object istance, SerializeApiType serializeApiType= SerializeApiType.NewtonSoft)
        {
            return istance == null ? string.Empty :
                serializeApiType == SerializeApiType.NewtonSoft 
                    ? JsonConvert.SerializeObject(istance) 
                    : System.Text.Json.JsonSerializer.Serialize(istance);

        }
    }
}
