using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
    }
}
