using Newtonsoft.Json;

namespace SharpApi.Helpers.ObjectExtensions.Deserialize
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
        public static T FromJson<T>(this string istance,JsonSerializerSettings settings ) where T : class
       {
           return string.IsNullOrEmpty(istance) ? default : JsonConvert.DeserializeObject<T>(istance, settings);
       }

    }
}
