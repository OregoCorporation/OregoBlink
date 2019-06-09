using System;

namespace OregoBlink.util.common.array
{
    public static class OregoArrayUtils
    {
        public static T[] Foreach<T>(this T[] array, Action<T> action)
        {
            foreach (var e in array)
            {
                action?.Invoke(e);
            }

            return array;
        }
    }
}