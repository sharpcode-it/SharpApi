using Newtonsoft.Json;
using SharpApi.Helpers.Model.Enum;

namespace SharpApi.Helpers.ObjectExtensions.Deserialize
{
    public static partial class Deserialize
    {
       /// <summary>
        /// This method deserializes the JSON to the specific object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="istance"></param>
        /// <param name="serializeApiType"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string istance, SerializeApiType serializeApiType = SerializeApiType.NewtonSoft) where T : class
        {
            return string.IsNullOrEmpty(istance) ? default :
                serializeApiType == SerializeApiType.NewtonSoft
                ? JsonConvert.DeserializeObject<T>(istance)
                : System.Text.Json.JsonSerializer.Deserialize<T>(istance);
        }

    }
}
