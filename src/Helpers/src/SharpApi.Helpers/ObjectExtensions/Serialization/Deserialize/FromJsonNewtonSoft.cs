using Newtonsoft.Json;

namespace SharpApi.Helpers.ObjectExtensions.Serialization
{
    public static partial class Deserialize
    {
        /// <summary>
        /// This method deserializes the JSON to the specific object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="istance"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static T? FromJson<T>(this string istance,JsonSerializerSettings settings) where T : class
       {
           return string.IsNullOrEmpty(istance) ? default : JsonConvert.DeserializeObject<T>(istance, settings);
       }

        /// <summary>
        /// This method deserializes the JSON to the specific object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="istance"></param>
        /// <param name="converters">Converters to use while deserializing.</param>
        /// <returns></returns>
        private static T? FromJson<T>(this string istance, params JsonConverter[] converters) where T : class
        {
            return string.IsNullOrEmpty(istance) ? default : JsonConvert.DeserializeObject<T>(istance, converters);
        }
    }
}
