using System.Text.Json;

namespace SharpApi.Helpers.ObjectExtensions.Deserialize
{
    public static partial class Deserialize
    {
        /// <summary>
        /// This method deserializes the JSON to the specific object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="istance"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string istance,JsonSerializerOptions options) where T : class
       {
           return string.IsNullOrEmpty(istance) ? default : JsonSerializer.Deserialize<T>(istance, options);
       }

    }
}
