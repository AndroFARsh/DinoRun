using System;
using Random = UnityEngine.Random;

namespace Infrastructure
{
    public static class ArrayUtils
    {
        public static void InvertForReach<T>(this T[] array, Action<T> func)
        {
            for (var index=array.Length - 1; index >= 0; --index)
                func.Invoke(array[index]);
        }   
        
        public static void ForReach<T>(this T[] array, Action<T> func)
        {
            for (var index=0; index < array.Length; ++index)
                func.Invoke(array[index]);
        }
        
        public static void ForReachWithIndex<T>(this T[] array, Action<int, T> func)
        {
            for (var index=0; index < array.Length; ++index)
                func.Invoke(index, array[index]);
        }

        public static T GetRandom<T>(this T[] array)
        {
            var result = array is { Length: > 0 };
            return result ? array[Random.Range(0, array.Length)] : default;
        }
        
        public static bool TryRandom<T>(this T[] array, out T value)
        {
            var result = array is { Length: > 0 };
            value = result ? array[Random.Range(0, array.Length)] : default;
            return result;
        }

        public static bool TryFirst<T>(this T[] array, out T value)
        {
            var result = array is { Length: > 0 };
            value = result ? array[0] : default;
            return result;
        }

        public static bool TryLast<T>(this T[] array, out T value)
        {
            var result = array is { Length: > 0 };
            value = result ? array[^1] : default;
            return result;
        }

        public static bool TryGet<T>(this T[] array, int index, out T value)
        {
            if (index >= 0 && index < array.Length)
            {
                value = array[index];
                return true;
            }

            value = default;
            return false;
        }

        public static T RandomOrDefault<T>(this T[] array) => TryRandom(array, out var result)
            ? result
            : default;

        public static T FirstOrDefault<T>(this T[] array) => TryFirst(array, out var result)
            ? result
            : default;

        public static T LastOrDefault<T>(this T[] array) => TryLast(array, out var result)
            ? result
            : default;
    }
}