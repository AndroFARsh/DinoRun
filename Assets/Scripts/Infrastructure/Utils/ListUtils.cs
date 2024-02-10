using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Utils
{
    public static class ListUtils
    {
        public static IList<T> ListOf<T>(params T[] elements) => new List<T>(elements);
        
        public static IList<T> EmptyList<T>() => new List<T>();

        public static IList<T> Plus<T>(this IEnumerable<T> list, params T[] elements)
        {
            var newList = new List<T>(list); 
            newList.AddRange(elements);
            return newList;
        } 
        
        public static bool TryPopRandom<T>(this List<T> list, out T value)
        {
            var result = list is { Count: > 0 };
            if (result)
            {
                var index = Random.Range(0, list.Count);
                value = list[index];
                list.RemoveAt(index);
            }
            else
            {
                value = default;
            }
            return result;
        }
        
        public static bool TryRandom<T>(this List<T> list, out T value)
        {
            var result = list is { Count: > 0 };
            value = result ? list[Random.Range(0, list.Count)] : default;
            return result;
        }

        public static bool TryFirst<T>(this List<T> list, out T value)
        {
            var result = list is { Count: > 0 };
            value = result ? list[0] : default;
            return result;
        }

        public static bool TryLast<T>(this List<T> list, out T value)
        {
            var result = list is { Count: > 0 };
            value = result ? list[^1] : default;
            return result;
        }

    }
}