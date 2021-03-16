using System;
using System.Text.Json;
using Newtonsoft.Json;
using SharpApi.Helpers.Model.Enum;

namespace SharpApi.Helpers.ObjectExtensions.Serialization
{
    public static partial class Serialize
    {
        /// <summary>
        /// This method serializes the specific object to JSON
        /// </summary>
        /// <param name="istance"></param>
        /// <param name="serializeContext"></param>
        /// <returns></returns>
        public static string ToJson(this object? istance, object? serializeContext)
        {
            if (istance is null) return string.Empty;

            if (serializeContext == null)
                return istance.ToJson(SerializeApiType.NewtonSoft);

            return serializeContext switch
            {
                JsonSerializerOptions t => istance.ToJson(t),
                JsonSerializerSettings ts => istance.ToJson(ts),
                _ => throw new NotImplementedException()
            };
        }

        #region Private Method
        private static string ToJson(this object? istance, SerializeApiType serializeApiType)
        {
            return (istance is null)
                ? string.Empty
                : serializeApiType == SerializeApiType.NewtonSoft
                    ? JsonConvert.SerializeObject(istance)
                    : System.Text.Json.JsonSerializer.Serialize(istance);

        }
        #endregion
    }
}
