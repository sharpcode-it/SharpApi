using System;
using System.Text.Json;
using Newtonsoft.Json;
using SharpApi.Helpers.Model.Enum;

namespace SharpApi.Helpers.ObjectExtensions.Serialization
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
        private static T? FromJson<T>(this string istance, SerializeApiType serializeApiType = SerializeApiType.NewtonSoft) 
           where T : class
       {
           if (string.IsNullOrEmpty(istance)) return default;

            return serializeApiType == SerializeApiType.NewtonSoft
                ? JsonConvert.DeserializeObject<T>(istance)
                : System.Text.Json.JsonSerializer.Deserialize<T>(istance);
        }

       public static T? FromJson<T>(this string istance, object? serializeContext = null,
           SerializeApiType serializeApiType = SerializeApiType.NewtonSoft)
           where T : class
       {
           if (string.IsNullOrEmpty(istance)) return default;

           if (serializeContext == null)
               return istance.FromJson<T>(serializeApiType);

           return serializeContext switch
           {
               JsonSerializerOptions t => istance.FromJson<T>(t),
               JsonConverter[] tarr => istance.FromJson<T>(tarr),
               JsonSerializerSettings ts => istance.FromJson<T>(ts),
               _ => throw new NotImplementedException()
           };
       }
    }
}
