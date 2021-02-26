using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpApi.Helpers.ObjectExtensions
{
    public static class GenericsHelper
    {
        public static bool IsDefault<T>(this T x)
        {
            return EqualityComparer<T>.Default.Equals(x, default);
        }

        public static void ForEach<T>(this IEnumerable<T>? source, Action<T>? action)
        {
            if((source is null) || (action is null)) return;

            foreach (var item in source)
                action(item);
        }

        public static bool Implements<T>(this object x)
        {
            return x.GetType().GetInterfaces().Contains(typeof(T));
        }

        public static bool Implements<T>(this Type t)
        {
            return t.GetInterfaces().Contains(typeof(T));
        }

        public static Type GetType<T>(this IEnumerable<T> list)
        {
            return typeof(T);
        }

        public static bool IsList<T>(this T istance)
        {
            return typeof(T).IsList();
        }

        public static bool IsListOf<T>(this object istance)
        {
            return istance.GetType().IsList() && istance.GetType().GetGenericArguments()[0] == typeof(T);
        }

        public static bool IsList(this Type t)
        {
            return t.IsGenericType &&
                   (t.GetGenericTypeDefinition().IsAssignableFrom(typeof(IList<>)) ||
                    t.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)));
        }

        public static bool IsDictionary(this Type t)
        {
            return t.IsGenericType &&
                   (t.GetGenericTypeDefinition().IsAssignableFrom(typeof(IDictionary<,>)) ||
                    t.GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>)));
        }

        public static T ThrowIf<T>(this T val, Func<T, bool>? predicate, Func<Exception>? exceptionFunc)
        {
            if ((predicate is null) || (exceptionFunc is null)) return val;

            if (predicate(val))
                throw exceptionFunc();
            return val;
        }

        public static TOut InvokeOn<T,TOut>(this T value,Func<T,TOut> func)
        {
            return func.Invoke(value);
        }

        public static bool IsTypeCode<T>(this T istance, TypeCode typeCode)
        {
            return Type.GetTypeCode(istance?.GetType() ?? typeof(T)).Equals(typeCode);
        }
    }
}
