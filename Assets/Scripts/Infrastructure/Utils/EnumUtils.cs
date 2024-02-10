using System;

namespace Infrastructure
{
    public static class EnumUtils
    {
        public static string GetName<T>(this T src) where T : struct => Enum.GetName(typeof(T), src);
        
        public static T Next<T>(this T src, bool looped = true) where T : struct => GetValue(src, +1, looped);
        
        public static T Prev<T>(this T src, bool looped = true) where T : struct => GetValue(src, -1, looped);
        
        private static T GetValue<T>(this T src, int offset, bool looped) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            var arr = (T[])Enum.GetValues(src.GetType());
            var index = Array.IndexOf(arr, src) + offset;
            
            if (!looped && (index < 0 || index >= arr.Length)) return src;
                        
            index %= arr.Length;
            if (index < 0)
            {
                index = arr.Length + index;
            }

            return arr[index];            
        }
    }
}