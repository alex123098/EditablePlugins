using System;
using System.Collections.Generic;
using System.Reflection;

namespace Editable.Host.Infrastructure
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this T @this)
        {
            yield return @this;
        }
    }

    public static class ReflectionExtensions
    {
        public static bool HasAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetCustomAttribute<T>() != null;
        }
    }
}
