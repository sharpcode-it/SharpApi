using System;
using System.Text.Json;
using Newtonsoft.Json;
using SharpApi.Helpers.Model.Enum;

namespace SharpApi.Helpers.ObjectExtensions.Serialization
{
    public static partial class Deserialize
    {
        /// <returns>Istance of <see cref="object"/> of <see cref="Type"/> T 
        /// <para>Returns default of <typeparamref name="T"/> if <param name="istance"></param>
        /// is <see langword="null"/> or <see langword="string.empty"/></para>
        /// </returns>
        /// <inheritdoc cref="System.Text.Json.JsonSerializer.Deserialize{TValue}(ReadOnlySpan{byte},System.Text.Json.JsonSerializerOptions?)"/>
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

        #region Private Method
        private static T? FromJson<T>(this string istance, SerializeApiType serializeApiType)
            where T : class
        {
            if (string.IsNullOrEmpty(istance)) return default;

            return serializeApiType == SerializeApiType.NewtonSoft
                ? JsonConvert.DeserializeObject<T>(istance)
                : System.Text.Json.JsonSerializer.Deserialize<T>(istance);
        }
        #endregion
    }
}
